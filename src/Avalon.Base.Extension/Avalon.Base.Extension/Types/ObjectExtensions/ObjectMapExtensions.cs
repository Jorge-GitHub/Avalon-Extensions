using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Avalon.Base.Extension.Types.ObjectExtensions;

/// <summary>
/// Object mapping extension methods.
/// </summary>
public static class ObjectMapExtensions
{
    private static readonly ConcurrentDictionary<(Type SourceType, Type TargetType), Delegate> MapCache = new();

    private static readonly JsonSerializerOptions MapJsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    /// <summary>
    /// Map an object to its DTO version using JSON serialization.
    /// </summary>
    /// <typeparam name="T">
    /// Object to return.
    /// </typeparam>
    /// <param name="objectToMap">
    /// Object to map.
    /// </param>
    /// <returns>
    /// Object in its DTO version.
    /// </returns>
    public static T? MapUsingJson<T>(this object? objectToMap)
    {
        return objectToMap.Map<T>(MapJsonOptions);
    }

    /// <summary>
    /// Map an object to its DTO version using JSON serialization and the specified serializer options.
    /// </summary>
    /// <typeparam name="T">
    /// Object to return.
    /// </typeparam>
    /// <param name="objectToMap">
    /// Object to map.
    /// </param>
    /// <param name="options">
    /// Serializer options.
    /// </param>
    /// <returns>
    /// Object in its DTO version.
    /// </returns>
    public static T? Map<T>(this object? objectToMap,
        JsonSerializerOptions options)
    {
        if (objectToMap is not null)
        {
            return JsonSerializer.Deserialize<T>(
                objectToMap.ToJson(options), options);
        }

        return default;
    }

    /// <summary>
    /// Map an object to its DTO version using a cached compiled property mapper.
    /// </summary>
    /// <typeparam name="T">
    /// Object to return.
    /// </typeparam>
    /// <param name="objectToMap">
    /// Object to map.
    /// </param>
    /// <returns>
    /// Object in its DTO version.
    /// </returns>
    public static T? Map<T>(this object? objectToMap)
    {
        if (objectToMap is not null)
        {
            var mapper = (Func<object, T>)MapCache.GetOrAdd(
                (objectToMap.GetType(), typeof(T)),
                static key => CreateMapper<T>(key.SourceType, key.TargetType));

            return mapper(objectToMap);
        }

        return default;
    }

    /// <summary>
    /// Create a compiled mapper function for the specified source and target types.
    /// </summary>
    /// <typeparam name="T">
    /// Object to return.
    /// </typeparam>
    /// <param name="sourceType">
    /// Source object type.
    /// </param>
    /// <param name="targetType">
    /// Target object type.
    /// </param>
    /// <returns>
    /// Compiled mapper function.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the target type is a reference type without a public parameterless constructor.
    /// </exception>
    private static Func<object, T> CreateMapper<T>(Type sourceType, Type targetType)
    {
        ParameterExpression sourceParameter = Expression.Parameter(typeof(object), "source");

        if (targetType.IsAssignableFrom(sourceType))
        {
            UnaryExpression convertedSource = Expression.Convert(sourceParameter, targetType);

            return Expression.Lambda<Func<object, T>>(
                convertedSource,
                sourceParameter).Compile();
        }

        ConstructorInfo? targetConstructor = targetType.GetConstructor(Type.EmptyTypes);
        if (targetConstructor is null && !targetType.IsValueType)
        {
            throw new InvalidOperationException(
                $"Type '{targetType.FullName}' must have a public parameterless constructor to be mapped.");
        }

        UnaryExpression typedSource = Expression.Convert(sourceParameter, sourceType);
        NewExpression target = targetConstructor is null
            ? Expression.New(targetType)
            : Expression.New(targetConstructor);

        MemberBinding[] bindings = CreatePropertyBindings(sourceType, targetType, typedSource);
        MemberInitExpression body = Expression.MemberInit(target, bindings);

        return Expression.Lambda<Func<object, T>>(
            body,
            sourceParameter).Compile();
    }

    /// <summary>
    /// Create member bindings for compatible source and target properties.
    /// </summary>
    /// <param name="sourceType">
    /// Source object type.
    /// </param>
    /// <param name="targetType">
    /// Target object type.
    /// </param>
    /// <param name="typedSource">
    /// Expression that represents the source object converted to its runtime type.
    /// </param>
    /// <returns>
    /// Member bindings for all compatible mapped properties.
    /// </returns>
    private static MemberBinding[] CreatePropertyBindings(
        Type sourceType,
        Type targetType,
        Expression typedSource)
    {
        Dictionary<string, PropertyInfo> sourceProperties = sourceType
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(property =>
                property.CanRead &&
                property.GetMethod?.IsPublic == true &&
                property.GetIndexParameters().Length == 0)
            .ToDictionary(
                property => property.Name,
                StringComparer.OrdinalIgnoreCase);

        return targetType
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(property =>
                property.CanWrite &&
                property.SetMethod?.IsPublic == true &&
                property.GetIndexParameters().Length == 0)
            .Where(property =>
                sourceProperties.TryGetValue(property.Name, out PropertyInfo? sourceProperty) &&
                property.PropertyType.IsAssignableFrom(sourceProperty.PropertyType))
            .Select(property =>
            {
                PropertyInfo sourceProperty = sourceProperties[property.Name];
                MemberExpression sourceValue = Expression.Property(typedSource, sourceProperty);
                Expression value = sourceValue.Type == property.PropertyType
                    ? sourceValue
                    : Expression.Convert(sourceValue, property.PropertyType);

                return Expression.Bind(property, value);
            })
            .ToArray();
    }
}
