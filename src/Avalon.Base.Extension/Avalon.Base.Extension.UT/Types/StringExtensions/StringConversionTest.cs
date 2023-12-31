using Avalon.Base.Extension.Types;
using Avalon.Base.Extension.UT.Objects;

namespace Avalon.Base.Extension.UT.Types.StringExtensions;

/// <summary>
/// String tests.
/// </summary>
[TestClass]
public class StringConversionTest
{
    /// <summary>
    /// Test to object extension.
    /// </summary>
    [TestMethod]
    public void TestToObject()
    {
        string testValue = "{\"Id\":\"1\",\"Name\":\"Jorge\",\"LastName\":\"Gonzalez\",\"Description\":\"User tester\",\"Password\":\"Password1\"}";
        User user = testValue.ToObject<User>();
        Assert.IsTrue(user.Name.IsNotNullOrEmpty());
    }
}