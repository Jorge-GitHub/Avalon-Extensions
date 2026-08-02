using Avalon.Base.Extension.Types;

namespace Avalon.Base.Extension.System.IO;

/// <summary>
/// File extension methods.
/// </summary>
public static class FileExtensions
{
    /// <summary>
    /// Number of bytes inspected when deciding whether a file is binary.
    /// </summary>
    public const int BinarySampleSize = 8000;

    private static readonly string[] SizeUnits =
        ["bytes", "KB", "MB", "GB", "TB", "PB"];

    /// <summary>
    /// Determine whether the file holds binary rather than text content.
    /// </summary>
    /// <param name="filePath">
    /// Path to the file to inspect.
    /// </param>
    /// <returns>
    /// True when the sampled bytes contain a null, which text encodings do not
    /// produce. False when the file is missing or unreadable.
    /// </returns>
    public static bool IsBinary(this string filePath)
    {
        if (filePath.IsNullOrEmpty() || !File.Exists(filePath))
        {
            return false;
        }

        try
        {
            using FileStream stream = File.OpenRead(filePath);
            byte[] sample = new byte[BinarySampleSize];
            int read = stream.Read(sample, 0, sample.Length);

            return Array.IndexOf(sample, (byte)0, 0, read) >= 0;
        }
        catch (Exception exception)
            when (exception is IOException or UnauthorizedAccessException)
        {
            return false;
        }
    }

    /// <summary>
    /// Determine whether the path sits inside the folder.
    /// </summary>
    /// <param name="path">
    /// Path to test. May be a file or a folder, and need not exist.
    /// </param>
    /// <param name="folderPath">
    /// Folder the path is expected to sit inside.
    /// </param>
    /// <returns>
    /// True when the path is the folder itself or anything beneath it.
    /// </returns>
    /// <remarks>
    /// Both sides are fully qualified first, so <c>..</c> segments cannot walk
    /// out of the folder and still report as inside it.
    /// </remarks>
    public static bool IsInsideFolder(this string path, string folderPath)
    {
        if (path.IsNullOrEmpty() || folderPath.IsNullOrEmpty())
        {
            return false;
        }

        try
        {
            string absolutePath = Path.GetFullPath(path).TrimEnd(
                Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            string absoluteFolderPath = Path.GetFullPath(folderPath).TrimEnd(
                Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

            if (absolutePath.Equals(absoluteFolderPath,
                StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            // The separator matters: without it "C:\Data" would report as
            // inside "C:\DataArchive".
            return absolutePath.StartsWith(
                absoluteFolderPath + Path.DirectorySeparatorChar,
                StringComparison.OrdinalIgnoreCase);
        }
        catch (Exception exception)
            when (exception is ArgumentException or NotSupportedException
                or PathTooLongException)
        {
            return false;
        }
    }

    /// <summary>
    /// Create the folder that holds the file when it does not exist yet.
    /// </summary>
    /// <param name="filePath">
    /// Path to the file whose parent folder is needed.
    /// </param>
    /// <returns>
    /// The parent folder path, or an empty string when the file path has none.
    /// </returns>
    public static string EnsureParentDirectory(this string filePath)
    {
        if (filePath.IsNullOrEmpty())
        {
            return string.Empty;
        }

        string? folderPath = Path.GetDirectoryName(Path.GetFullPath(filePath));

        if (folderPath.IsNullOrEmpty())
        {
            return string.Empty;
        }

        Directory.CreateDirectory(folderPath!);

        return folderPath!;
    }

    /// <summary>
    /// Read a range of lines from the file.
    /// </summary>
    /// <param name="filePath">
    /// Path to the file to read.
    /// </param>
    /// <param name="offset">
    /// Zero based line to start at. Values below zero start at the beginning.
    /// </param>
    /// <param name="limit">
    /// Maximum number of lines to return. Values of zero or less return none.
    /// </param>
    /// <returns>
    /// The requested lines, streamed rather than loading the whole file.
    /// </returns>
    public static IEnumerable<string> ReadLines(this string filePath,
        int offset, int limit)
    {
        if (filePath.IsNullOrEmpty() || limit <= 0)
        {
            return [];
        }

        return File.ReadLines(filePath).Skip(offset < 0 ? 0 : offset).Take(limit);
    }

    /// <summary>
    /// Convert a byte count into text a person can read.
    /// </summary>
    /// <param name="bytes">
    /// Number of bytes.
    /// </param>
    /// <returns>
    /// The size with a unit, for example <c>1.4 MB</c>. Whole bytes are shown
    /// without a decimal.
    /// </returns>
    public static string ToFileSizeText(this long bytes)
    {
        if (bytes < 0)
        {
            return $"-{(-bytes).ToFileSizeText()}";
        }

        int unit = 0;
        double size = bytes;

        while (size >= 1024 && unit < SizeUnits.Length - 1)
        {
            size /= 1024;
            unit++;
        }

        return unit == 0
            ? $"{bytes} {SizeUnits[unit]}"
            : $"{size:0.#} {SizeUnits[unit]}";
    }
}
