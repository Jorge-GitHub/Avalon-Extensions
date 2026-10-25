using Avalon.Base.Extension.System.IO;

namespace Avalon.Base.Extension.UT.System.IO;

[TestClass]
public class FileExtensionsTest
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
    public void IsBinary_WithTextFile_ReturnsFalse()
    {
        string path = this.CreateFile("notes.txt", "hello\r\nworld");

        Assert.IsFalse(path.IsBinary());
    }

    [TestMethod]
    public void IsBinary_WithNullBytes_ReturnsTrue()
    {
        string path = Path.Combine(this.folder, "image.bin");
        File.WriteAllBytes(path, [0x50, 0x4E, 0x47, 0x00, 0x1A, 0x0A]);

        Assert.IsTrue(path.IsBinary());
    }

    [TestMethod]
    public void IsBinary_WithMissingFile_ReturnsFalse()
    {
        Assert.IsFalse(Path.Combine(this.folder, "gone.txt").IsBinary());
    }

    [TestMethod]
    public void IsBinary_WithEmptyPath_ReturnsFalse()
    {
        Assert.IsFalse(string.Empty.IsBinary());
    }

    [TestMethod]
    public void IsInsideFolder_WithChildPath_ReturnsTrue()
    {
        string child = Path.Combine(this.folder, "sub", "file.txt");

        Assert.IsTrue(child.IsInsideFolder(this.folder));
    }

    [TestMethod]
    public void IsInsideFolder_WithTheFolderItself_ReturnsTrue()
    {
        Assert.IsTrue(this.folder.IsInsideFolder(this.folder));
    }

    [TestMethod]
    public void IsInsideFolder_WithTrailingSeparator_ReturnsTrue()
    {
        string child = Path.Combine(this.folder, "file.txt");

        Assert.IsTrue(child.IsInsideFolder(
            this.folder + Path.DirectorySeparatorChar));
    }

    [TestMethod]
    public void IsInsideFolder_WithSiblingSharingAPrefix_ReturnsFalse()
    {
        // "C:\DataArchive" must not count as inside "C:\Data".
        Assert.IsFalse($"{this.folder}Archive\\file.txt"
            .IsInsideFolder(this.folder));
    }

    [TestMethod]
    public void IsInsideFolder_WithParentTraversal_ReturnsFalse()
    {
        string escaping = Path.Combine(this.folder, "..", "..", "elsewhere.txt");

        Assert.IsFalse(escaping.IsInsideFolder(this.folder));
    }

    [TestMethod]
    public void IsInsideFolder_WithEmptyValues_ReturnsFalse()
    {
        Assert.IsFalse(string.Empty.IsInsideFolder(this.folder));
        Assert.IsFalse(this.folder.IsInsideFolder(string.Empty));
    }

    [TestMethod]
    public void EnsureParentDirectory_WithMissingFolder_CreatesIt()
    {
        string path = Path.Combine(this.folder, "a", "b", "file.txt");

        string created = path.EnsureParentDirectory();

        Assert.IsTrue(Directory.Exists(created));
        Assert.AreEqual(Path.GetDirectoryName(Path.GetFullPath(path)), created);
    }

    [TestMethod]
    public void EnsureParentDirectory_WithExistingFolder_LeavesItAlone()
    {
        string path = this.CreateFile("kept.txt", "keep me");

        path.EnsureParentDirectory();

        Assert.AreEqual("keep me", File.ReadAllText(path));
    }

    [TestMethod]
    public void ReadLines_ReturnsTheRequestedRange()
    {
        string path = this.CreateFile("lines.txt",
            string.Join(Environment.NewLine, ["one", "two", "three", "four"]));

        string[] lines = [.. path.ReadLines(1, 2)];

        CollectionAssert.AreEqual(new[] { "two", "three" }, lines);
    }

    [TestMethod]
    public void ReadLines_PastTheEnd_ReturnsNothing()
    {
        string path = this.CreateFile("short.txt", "only");

        Assert.IsEmpty(path.ReadLines(10, 5));
    }

    [TestMethod]
    public void ReadLines_WithoutALimit_ReturnsNothing()
    {
        string path = this.CreateFile("any.txt", "one");

        Assert.IsEmpty(path.ReadLines(0, 0));
    }

    [TestMethod]
    public void ReadLines_WithNegativeOffset_StartsAtTheBeginning()
    {
        string path = this.CreateFile("lines.txt",
            string.Join(Environment.NewLine, ["one", "two"]));

        string[] lines = [.. path.ReadLines(-5, 1)];

        CollectionAssert.AreEqual(new[] { "one" }, lines);
    }

    [TestMethod]
    public void ToFileSizeText_UnderAKilobyte_ShowsWholeBytes()
    {
        Assert.AreEqual("0 bytes", 0L.ToFileSizeText());
        Assert.AreEqual("512 bytes", 512L.ToFileSizeText());
    }

    [TestMethod]
    public void ToFileSizeText_ScalesToTheUnit()
    {
        Assert.AreEqual("1 KB", 1024L.ToFileSizeText());
        Assert.AreEqual("1.4 MB", 1468006L.ToFileSizeText());
        Assert.AreEqual("1 GB", (1024L * 1024 * 1024).ToFileSizeText());
    }

    [TestMethod]
    public void ToFileSizeText_WithNegativeValue_KeepsTheSign()
    {
        Assert.AreEqual("-1 KB", (-1024L).ToFileSizeText());
    }

    private string CreateFile(string name, string content)
    {
        string path = Path.Combine(this.folder, name);
        File.WriteAllText(path, content);

        return path;
    }
}
