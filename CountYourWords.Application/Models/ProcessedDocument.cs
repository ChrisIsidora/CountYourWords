namespace CountYourWords.Application.Models;

public class ProcessedDocument(IEnumerable<WordOccurrence> wordOccurrences)
{
    public IEnumerable<WordOccurrence> WordOccurrences { get; } = wordOccurrences;
    
    public int TotalWordCount => WordOccurrences.Sum(wc => wc.Occurrences);
}