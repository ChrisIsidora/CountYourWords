namespace CountYourWords.Application.Interfaces;

public interface IDocumentProvider
{
    Task<string> GetContentAsStringAsync(string filePath);
}