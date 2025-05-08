using CountYourWords.Application.Interfaces;
using Microsoft.Extensions.FileProviders;

namespace CountYourWords.Application.Services;

public class DocumentProvider(IFileProvider fileProvider) : IDocumentProvider
{
    private readonly IFileProvider _fileProvider = fileProvider ?? throw new ArgumentNullException(nameof(fileProvider));

    public async Task<string> GetContentAsStringAsync(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
        }
        
        var fileInfo = _fileProvider.GetFileInfo(filePath);

        if (!fileInfo.Exists)
        {
            throw new FileNotFoundException($"The file '{filePath}' does not exist.", filePath);
        }
        
        if (fileInfo.IsDirectory)
        {
            throw new ArgumentException($"The path '{filePath}' is not a file.", filePath);
        }
        
        await using var stream = fileInfo.CreateReadStream();
        using StreamReader reader = new(stream);
        return await reader.ReadToEndAsync();
    }
}