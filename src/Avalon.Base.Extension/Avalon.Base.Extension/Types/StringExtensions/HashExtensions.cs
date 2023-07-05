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
        return value.ToSHA1(null);
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
    public static string ToSHA1(this string value, Encoding encode)
    {
        if (encode == null)
        {
            encode = Encoding.Default;
        }
        byte[] buffer = encode.GetBytes(value);
        SHA1CryptoServiceProvider cryptoTransformSHA1 = new SHA1CryptoServiceProvider();

        return BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");
    }
}