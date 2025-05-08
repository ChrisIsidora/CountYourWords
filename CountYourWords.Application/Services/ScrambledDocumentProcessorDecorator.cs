using CountYourWords.Application.Interfaces;
using CountYourWords.Application.Models;

namespace CountYourWords.Application.Services;

public class ScrambledDocumentProcessorDecorator(
    IDocumentProcessor documentProcessor,
    IScrambleService scrambleService)
    : IDocumentProcessor
{
    private readonly IDocumentProcessor _documentProcessor = documentProcessor ?? throw new ArgumentNullException(nameof(documentProcessor));
    private readonly IScrambleService _scrambleService = scrambleService ?? throw new ArgumentNullException(nameof(scrambleService));

    public ProcessedDocument GetProcessedDocument(string documentContent)
    {
        if (string.IsNullOrWhiteSpace(documentContent))
        {
            throw new ArgumentException("Document content cannot be null or empty.", nameof(documentContent));
        }
        
        var processedDocument =  _documentProcessor.GetProcessedDocument(documentContent);
        
        foreach (var wordOccurrence in processedDocument.WordOccurrences)
        {
            wordOccurrence.Word = _scrambleService.Scramble(wordOccurrence.Word);
        }
        
        return processedDocument;
    }
}