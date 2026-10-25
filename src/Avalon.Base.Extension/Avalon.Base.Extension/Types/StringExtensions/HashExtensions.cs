using System.Security.Cryptography;
using System.Text;

namespace Avalon.Base.Extension.Types;

/// <summary>
/// Hash extensions.
/// Other types of hash for the future: MD5, SHA384, SHA512Of.
/// </summary>
public static class HashExtensions
{
    /// <summary>
    /// Create a Hash Sha1. 
    /// </summary>
    /// <param name="value">
    /// Value to hash.
    /// </param>
    /// <returns>
    /// Value to hashed.
    /// </returns>
    public static string ToSHA1(this string value)
    {
        return value.ToSHA1(Encoding.UTF8);
    }
    /// <summary>
    /// Create a Hash Sha1. 
    /// </summary>
    /// <param name="value">
    /// Value to hash.
    /// </param>
    /// <param name="encode">
    /// Optional: Encoding type.
    /// </param>
    /// <returns>
    /// Value to hashed.
    /// </returns>
    public static string ToSHA1(this string value, Encoding? encode = null)
    {
        if (encode is null)
        {
            encode = Encoding.UTF8;
        }
        byte[] buffer = encode.GetBytes(value);
        using SHA1 cryptoTransformSHA1 = SHA1.Create();

        return BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");
    }

    public static string CreateHash(this string value)
    {
        return value.CreateHash(Encoding.UTF8);
    }

    public static string CreateHash(this string value,
        Encoding encode)
    {
        if (encode is null)
        {
            encode = Encoding.UTF8;
        }

        byte[] bytes = encode.GetBytes(value);
        byte[] hashBytes = SHA256.HashData(bytes);

        return Convert.ToHexString(hashBytes).ToLowerInvariant();
    }
}
