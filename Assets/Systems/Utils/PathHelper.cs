using System.IO;

/// <summary>
/// A helper class for working with file paths.
/// </summary>
public static class PathHelper
{

    /// <summary>
    /// Gets the file name without extension from the specified file path.
    /// </summary>
    /// <param name="path">The path of the file.</param>
    /// <returns>The file name without extension, or an empty string if the file does not exist.</returns>
    public static string GetFileNameFromPath(string path)
    {
        if (!File.Exists(path)) return string.Empty;
        return Path.GetFileNameWithoutExtension(path);
    }
}
