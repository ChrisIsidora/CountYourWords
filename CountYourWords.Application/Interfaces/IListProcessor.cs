namespace CountYourWords.Application.Interfaces;

public interface IListProcessor
{
    List<T> SortUsingComparer<T>(List<T> items, IComparer<T> comparer);
}