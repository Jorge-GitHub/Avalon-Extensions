using Avalon.Base.Extension.Types;
using Avalon.Base.Extension.Types.StringExtensions;

namespace Avalon.Base.Extension.UT.Types.StringExtensions;

/// <summary>
/// String tests.
/// </summary>
[TestClass]
public class StringTest
{
    [TestMethod]
    public void TestSplitByBlocks()
    {
        string value = "Hello World";
        List<string> values = value.SplitByBlocks(2);
        int lenght = value.SplitByBlocks(2).Count;
        Assert.AreEqual(6, lenght);
        lenght = string.Empty.SplitByBlocks(2).Count;
        Assert.AreEqual(0, lenght);
        lenght = value.SplitByBlocks(4).Count;
        Assert.AreEqual(3, lenght);
    }

    [TestMethod]
    public void TestNotEquals()
    {
        string test1 = null;
        string test2 = null;
        Assert.IsFalse(test1.NotEquals(test2, StringComparison.OrdinalIgnoreCase));
        test1 = string.Empty;
        test2 = string.Empty;
        Assert.IsFalse(test1.NotEquals(test2, StringComparison.OrdinalIgnoreCase));
        test1 = "Hello World";
        Assert.IsTrue(test1.NotEquals(test2, StringComparison.OrdinalIgnoreCase));
        test2 = "Hello World";
        Assert.IsFalse(test1.NotEquals(test2, StringComparison.OrdinalIgnoreCase));
        test2 = "Hola Mundo";
        Assert.IsTrue(test1.NotEquals(test2, StringComparison.OrdinalIgnoreCase));
    }

    [TestMethod]
    public void TestEmail()
    {
        string[] validEmails =
        {
            "jamesbond@gmail.com",
            "jane@about.me",
            "jane@archive.history",
            "person@example.technology",
            "first.last+tag@sub.example.solutions"
        };

        foreach (string email in validEmails)
        {
            Assert.IsTrue(email.IsAValidEmail(), $"{email} should be valid.");
        }
    }

    [TestMethod]
    public void TestNotValidEmail()
    {
        string?[] invalidEmails =
        {
            null,
            string.Empty,
            " ",
            "jamesbond",
            "a@b",
            "name@example",
            "name@example.com ",
            " name@example.com",
            "name@@example.com",
            "name@example..com",
            "first last@example.com"
        };

        Assert.IsFalse("jamesbond@gmail.com".IsNotAValidEmail());

        foreach (string? email in invalidEmails)
        {
            Assert.IsFalse(email.IsAValidEmail(), $"{email} should be invalid.");
            Assert.IsTrue(email.IsNotAValidEmail(), $"{email} should not be valid.");
        }
    }

    /// <summary>
    /// Test RemoveBetweenTags extension.
    /// </summary>
    [TestMethod]
    public void TestRemoveBetweenTags()
    {
        string testValue = "*|FirstText|*Content To Remove*|SecondText|*";
        testValue = testValue.RemoveBetweenTags("*|FirstText|*", "*|SecondText|*");
        Assert.IsFalse(testValue.Contains("Content To Remove"));
        Assert.IsTrue(testValue.Contains("*|FirstText|**|SecondText|*"));
    }

    /// <summary>
    /// Test Sanitize extension.
    /// </summary>
    [TestMethod]
    public void TestSanitize()
    {
        string value = "Hello@";
        string cleanValue = value.Sanitize();
        Assert.IsTrue(cleanValue.Equals("Hello"));
    }
}
