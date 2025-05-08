using CountYourWords.Application.Interfaces;

namespace CountYourWords.Application.Services;

public class ListProcessor : IListProcessor
{
    public List<T> SortUsingComparer<T>(List<T> items, IComparer<T> comparer)
    {
        if (items == null)
        {
            throw new ArgumentNullException(nameof(items));
        }

        if (comparer == null)
        {
            throw new ArgumentNullException(nameof(comparer));
        }

        var result = new List<T>(items);
        
        if (result.Count <= 1)
        {
            return result;
        }

        for (int i = 1; i < result.Count; i++)
        {
            var current = result[i];
            int j = i - 1;

            // Move elements greater than current to one position ahead
            while (j >= 0 && comparer.Compare(result[j], current) > 0)
            {
                result[j + 1] = result[j];
                j--;
            }

            // Place current in its correct position
            result[j + 1] = current;
        }

        return result;
    }
}