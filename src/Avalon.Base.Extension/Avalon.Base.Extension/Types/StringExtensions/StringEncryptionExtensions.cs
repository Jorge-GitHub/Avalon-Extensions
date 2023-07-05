using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Avalon.Base.Extension.Types.StringExtensions;

/// <summary>
/// String encryption extensions.
/// </summary>
public static class StringEncryptionExtensions
{
    /// <summary>
    /// Encrypts the string.
    /// </summary>
    /// <param name="value">
    /// String to be encrypted.
    /// </param>
    /// <returns>
    /// Encrypted string.
    /// </returns>
    public static string Encrypt(this string value)
    {
        if (value.IsNotNullOrEmpty())
        {
            Byte[] b = ASCIIEncoding.ASCII.GetBytes(value);

            return Convert.ToBase64String(b);
        }

        return string.Empty;
    }

    /// <summary>
    /// Decrypts the string.
    /// </summary>
    /// <param name="value">
    /// String to be decrypted.
    /// </param>
    /// <returns>
    /// Decrypted string.
    /// </returns>
    public static string Decrypt(this string value)
    {
        if (value.IsNotNullOrEmpty())
        {
            Byte[] b = Convert.FromBase64String(value);

            return ASCIIEncoding.ASCII.GetString(b);
        }

        return string.Empty;
    }

    /// <summary>
    /// Encrypt text.
    /// </summary>
    /// <param name="text">
    /// Text to encrypt.
    /// </param>
    /// <param name="key">
    /// Password phrase.
    /// </param>
    /// <remarks>
    /// The key length cannot be greater than 32 characters.
    /// </remarks>
    /// <returns>
    /// Encrypted text.
    /// </returns>
    public static string Encrypt(this string text, string key)
    {
        if (key.Length > 32)
        {
            throw new Exception("The key length cannot be greater than 32 characters.");
        }
        byte[] iv = new byte[16];
        byte[] array;

        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = iv;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(
                    (Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                    {
                        streamWriter.Write(text);
                    }

                    array = memoryStream.ToArray();
                }
            }
        }

        return Convert.ToBase64String(array);
    }

    /// <summary>
    /// Decrypt text.
    /// </summary>
    /// <param name="text">
    /// Text to decrypt.
    /// </param>
    /// <param name="key">
    /// Password phrase.
    /// </param>
    /// <remarks>
    /// The key cannot be more than 32 characters long. 
    /// </remarks>
    /// <returns>
    /// Decrypted text.
    /// </returns>
    public static string Decrypt(this string text, string key)
    {
        if (key.Length > 32)
        {
            throw new Exception("The key length cannot be greater than 32 characters.");
        }

        byte[] iv = new byte[16];
        byte[] buffer = Convert.FromBase64String(text);

        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream(buffer))
            {
                using (CryptoStream cryptoStream = new CryptoStream(
                    (Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }
    }
}