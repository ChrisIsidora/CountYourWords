using CountYourWords.Application.Interfaces;
using CountYourWords.Application.Models;

namespace CountYourWords.Console.Services;

public class CountYourWordsWorkerService(IDocumentProvider documentProvider, IDocumentProcessor documentProcessor)
{
    private readonly IDocumentProvider _documentProvider = documentProvider ?? throw new ArgumentNullException(nameof(documentProvider));
    private readonly IDocumentProcessor _documentProcessor = documentProcessor ?? throw new ArgumentNullException(nameof(documentProcessor));

    public async Task ProcessDocumentAsync(string documentPath)
    {
        System.Console.WriteLine($"Reading Document {documentPath}");
        var documentContent = await _documentProvider.GetContentAsStringAsync(documentPath);

        System.Console.WriteLine($"Processing Document {documentPath}");
        var processedDocument = _documentProcessor.GetProcessedDocument(documentContent);

        System.Console.WriteLine($"Document {documentPath} processed successfully.");
        DisplayProcessedDocument(processedDocument);
    }

    private static void DisplayProcessedDocument(ProcessedDocument processedDocument)
    {
        System.Console.WriteLine($"Total Word Count: {processedDocument.TotalWordCount}");
        foreach (var wordOccurrence in processedDocument.WordOccurrences)
        {
            System.Console.WriteLine($"{wordOccurrence.Word} {wordOccurrence.Occurrences}");
        }
    }
}