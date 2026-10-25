using Avalon.Base.Extension.Types.StringExtensions;

namespace Avalon.Base.Extension.UT.Types.StringExtensions;

[TestClass]
public class StringEncryptionTest
{
    [TestMethod]
    public void EncryptWithKeyTest()
    {
        string value = "HelloWorld";
        string key = "e3c71f4a9b2846d5a1c0f3e8b7d2a961";
        string encryptedValue = "lCMl+fq4zxVvaD1sYgi7bw==";

        string encryptedResult = value.Encrypt(key);

        Assert.IsTrue(encryptedResult.Equals(encryptedValue));
    }


    [TestMethod]
    public void DecryptWithKeyTest()
    {
        string value = "lCMl+fq4zxVvaD1sYgi7bw==";
        string key = "e3c71f4a9b2846d5a1c0f3e8b7d2a961";
        string decryptedValue = "HelloWorld";

        string decryptedResult = value.Decrypt(key);

        Assert.IsTrue(decryptedValue.Equals(decryptedValue));
    }
}
