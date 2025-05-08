using System.Text.RegularExpressions;
using CountYourWords.Application.Interfaces;
using CountYourWords.Application.Models;

namespace CountYourWords.Application.Services;

public class DocumentProcessor : IDocumentProcessor
{
    private const string WordPattern = @"([a-zA-Z]+)";
    
    public ProcessedDocument GetProcessedDocument(string documentContent)
    {
        var result = ExtractWords(documentContent)
            .GroupBy(w => w)
            .Select(g => new WordOccurrence(g.Key, g.Count()))
            .ToList();
        
        result.Sort(new WordOccurrenceComparer());
        
        return new ProcessedDocument(result);
    }
    
    private static IEnumerable<string> ExtractWords(string content)
    {
        var matches = Regex.Matches(content, WordPattern);
        return matches.Select(match => match.Value.ToLowerInvariant());
    }
}