using System.Collections.Generic;

namespace CountYourWords.Application.Models;

public class WordOccurrenceComparer : IComparer<WordOccurrence>
{
    public int Compare(WordOccurrence? x, WordOccurrence? y)
    {
        // Handle Null
        if (x == null)
        {
            return y == null ? 0 : -1;
        }

        if (y == null)
        {
            return 1;
        }

        // Handle Max Comparison
        var maxComparisonLength = x.Word.Length > y.Word.Length ? y.Word.Length : x.Word.Length;

        for (var i = 0; i < maxComparisonLength; i++)
        {
            var currentCharValue = Convert.ToInt32(x.Word[i]);
            var otherCharValue = Convert.ToInt32(y.Word[i]);

            if (currentCharValue == otherCharValue)
            {
                continue;
            }

            return currentCharValue < otherCharValue ? -1 : 1;
        }

        return 0;
    }
}