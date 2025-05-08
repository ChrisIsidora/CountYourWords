namespace CountYourWords.Application.Models;

public class WordOccurrence(string word, int occurrences)
{
    public string Word { get; set; } = word;
    public int Occurrences { get; set; } = occurrences;
}