namespace MusicCollectorConsole.Utilities;

public static class FileHelper
{
    public static bool IsValidMusicFile(string filePath)
    {
        if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            return false;
            
        var extension = Path.GetExtension(filePath).ToLowerInvariant();
        return extension is ".mp3" or ".flac" or ".wav" or ".m4a" or ".ogg";
    }
    
    public static string GetSafeFileName(string fileName)
    {
        return Path.GetInvalidFileNameChars()
            .Aggregate(fileName, (current, c) => current.Replace(c, '_'));
    }
}
