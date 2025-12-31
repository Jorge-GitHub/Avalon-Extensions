namespace Avalon.Base.Extension.Collections;

public static class DictionaryExtensions
{
    public static void AddRange<TKey, TValue>(
    this Dictionary<TKey, TValue[]> target,
    Dictionary<TKey, TValue[]> source,
    bool updateValue = true,
    bool throwExceptionOnDuplicate = false)
    {
        if (target == null)
        {
            throw new ArgumentNullException(nameof(target));
        }
        if (source?.Count > 0)
        {
            foreach (var element in source)
            {
                if (element.Key is not null)
                {
                    if (target.ContainsKey(element.Key))
                    {
                        if (throwExceptionOnDuplicate)
                        {
                            throw new InvalidOperationException($"Duplicate key found on target dictionary. Key: {element.Key}");
                        }
                        if (updateValue)
                        {
                            target[element.Key] = element.Value;
                        }
                    }
                    else
                    {
                        target.Add(element.Key, element.Value);
                    }
                }
            }
        }
    }
}
