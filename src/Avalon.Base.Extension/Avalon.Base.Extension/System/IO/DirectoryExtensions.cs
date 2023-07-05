namespace Avalon.Base.Extension.System.IO;

/// <summary>
/// System IO extension methods.
/// </summary>
public static class DirectoryExtensions
{
    /// <summary>
    /// Empty directory content.
    /// </summary>
    /// <param name="directory">
    /// Directory to empty.
    /// </param>
    public static void Empty(this DirectoryInfo directory)
    {
        foreach (FileInfo file in directory.GetFiles())
        {
            file.Delete();
        }
        foreach (DirectoryInfo subDirectory in directory.GetDirectories())
        {
            subDirectory.Delete(true);
        }
    }

    /// <summary>
    /// Empty folder.
    /// </summary>
    /// <param name="directoryPath">
    /// Path to the directory to empty.
    /// </param>
    public static void EmptyFolder(this string directoryPath)
    {
        if (Directory.Exists(directoryPath))
        {
            new DirectoryInfo(directoryPath).Empty();
        }
    }
}