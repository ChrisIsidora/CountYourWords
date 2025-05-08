using System.Text.RegularExpressions;
using CountYourWords.Application.Interfaces;
using CountYourWords.Application.Models;

namespace CountYourWords.Application.Services;

public class DocumentProcessor(IListProcessor listProcessor) : IDocumentProcessor
{
    private readonly IListProcessor _listProcessor =
        listProcessor ?? throw new ArgumentNullException(nameof(listProcessor));

    private const string WordPattern = @"([a-zA-Z]+)";

    public ProcessedDocument GetProcessedDocument(string documentContent)
    {
        var sortedList = _listProcessor.SortUsingComparer(
            ExtractWords(documentContent)
                .GroupBy(w => w)
                .Select(g => new WordOccurrence(g.Key, g.Count()))
                .ToList(), new WordOccurrenceComparer());

        return new ProcessedDocument(sortedList);
    }

    private static IEnumerable<string> ExtractWords(string content)
    {
        var matches = Regex.Matches(content, WordPattern);
        return matches.Select(match => match.Value.ToLowerInvariant());
    }
}