using CountYourWords.Application.Interfaces;

namespace CountYourWords.Application.Services;

public class DocumentProvider : IDocumentProvider
{
    public async Task<string> GetContentAsStringAsync(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
        }

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"The file '{filePath}' does not exist.", filePath);
        }

        return await File.ReadAllTextAsync(filePath);
    }
}