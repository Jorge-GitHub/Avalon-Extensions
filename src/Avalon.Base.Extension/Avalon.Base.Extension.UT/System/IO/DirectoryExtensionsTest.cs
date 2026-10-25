using Avalon.Base.Extension.System.IO;

namespace Avalon.Base.Extension.UT.System.IO;

[TestClass]
public class DirectoryExtensionsTest
{
    private string folder = string.Empty;

    [TestInitialize]
    public void CreateFolder()
    {
        this.folder = Path.Combine(
            Path.GetTempPath(),
            "Avalon.Base.Extension.UT",
            Guid.NewGuid().ToString("N"));

        Directory.CreateDirectory(this.folder);
    }

    [TestCleanup]
    public void RemoveFolder()
    {
        if (Directory.Exists(this.folder))
        {
            Directory.Delete(this.folder, recursive: true);
        }
    }

    [TestMethod]
    public void CopyTo_CopiesFilesAndSubDirectories()
    {
        string source = this.CreateTree();
        string destination = Path.Combine(this.folder, "destination");

        int copied = new DirectoryInfo(source).CopyTo(destination);

        Assert.AreEqual(2, copied);
        Assert.AreEqual("top",
            File.ReadAllText(Path.Combine(destination, "top.txt")));
        Assert.AreEqual("deep",
            File.ReadAllText(Path.Combine(destination, "nested", "deep.txt")));
    }

    [TestMethod]
    public void CopyTo_WithoutOverwrite_ThrowsOnAnExistingFile()
    {
        string source = this.CreateTree();
        string destination = Path.Combine(this.folder, "destination");

        Directory.CreateDirectory(destination);
        File.WriteAllText(Path.Combine(destination, "top.txt"), "existing");

        Assert.ThrowsExactly<IOException>(
            () => new DirectoryInfo(source).CopyTo(destination));
    }

    [TestMethod]
    public void CopyTo_WithOverwrite_ReplacesAnExistingFile()
    {
        string source = this.CreateTree();
        string destination = Path.Combine(this.folder, "destination");

        Directory.CreateDirectory(destination);
        File.WriteAllText(Path.Combine(destination, "top.txt"), "existing");

        new DirectoryInfo(source).CopyTo(destination, overwrite: true);

        Assert.AreEqual("top",
            File.ReadAllText(Path.Combine(destination, "top.txt")));
    }

    [TestMethod]
    public void CopyTo_CreatesTheDestinationWhenMissing()
    {
        string source = this.CreateTree();
        string destination = Path.Combine(this.folder, "a", "b", "c");

        new DirectoryInfo(source).CopyTo(destination);

        Assert.IsTrue(Directory.Exists(destination));
    }

    private string CreateTree()
    {
        string source = Path.Combine(this.folder, "source");

        Directory.CreateDirectory(Path.Combine(source, "nested"));
        File.WriteAllText(Path.Combine(source, "top.txt"), "top");
        File.WriteAllText(Path.Combine(source, "nested", "deep.txt"), "deep");

        return source;
    }
}
