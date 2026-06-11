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

    public static string ResolveFolderPath(this string folder)
    {
        if (string.IsNullOrWhiteSpace(folder))
        {
            return AppContext.BaseDirectory;
        }

        if (Path.IsPathFullyQualified(folder))
        {
            return folder;
        }

        return Path.GetFullPath(folder, AppContext.BaseDirectory);
    }

    public static void DeleteEmptyParentDirectory(this string filePath, string rootPath)
    {
        string? folderPath = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrWhiteSpace(folderPath))
        {
            string absoluteRootPath = Path.GetFullPath(rootPath).TrimEnd(
                Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            string absoluteFolderPath = Path.GetFullPath(folderPath).TrimEnd(
                Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

            if (!absoluteFolderPath.Equals(absoluteRootPath,
                StringComparison.OrdinalIgnoreCase) &&
                Directory.Exists(absoluteFolderPath) &&
                !Directory.EnumerateFileSystemEntries(absoluteFolderPath).Any())
            {
                Directory.Delete(absoluteFolderPath);
            }
        }
    }
}