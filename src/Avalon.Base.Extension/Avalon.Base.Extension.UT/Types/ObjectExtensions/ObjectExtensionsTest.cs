using Avalon.Base.Extension.Types;

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

/// <summary>
/// Demo class for testing purposes.
/// </summary>
internal class User
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Description { get; set; }
    public string Password { get; set; }
}

/// <summary>
/// Demo DTO class for testing purposes.
/// </summary>
internal class UserDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Description { get; set; }
}