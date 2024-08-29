using Avalon.Base.Extension.Types;
using Avalon.Base.Extension.UT.Objects;

namespace Avalon.Base.Extension.UT.Types.ObjectExtensions;

/// <summary>
/// String tests.
/// </summary>
[TestClass]
public class ObjectExtensionsTest
{
    [TestMethod]
    public void TestToJSON()
    {
        User test = new User()
        {
            Name = "Test",
            Description = "Simple Test"
        };
        string json = test.ToJSON();
        Assert.IsTrue(json.IsNotNullOrEmpty());
    }

    [TestMethod]
    public void TestDTOAndMapping()
    {
        User user = new User()
        {
            Id = "1",
            Name = "Jorge",
            LastName = "Gonzalez",
            Description = "User tester",
            Password = "Password1"
        };
        UserDTO dto = user.ToDTO<UserDTO>();

        Assert.IsTrue(dto.Name.IsNotNullOrEmpty());
    }
}