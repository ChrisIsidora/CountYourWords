using CountYourWords.Application.Models;

namespace CountYourWords.Application.Interfaces;

public interface IDocumentProcessor
{
    ProcessedDocument GetProcessedDocument(string documentContent);
}