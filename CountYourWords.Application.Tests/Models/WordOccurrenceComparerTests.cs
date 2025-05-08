using CountYourWords.Application.Models;

namespace CountYourWords.Application.Tests.Models;

public class WordOccurrenceComparerTests
{
    [Theory]
    [InlineData("apple", "banana")]
    [InlineData(null, "apple")]
    public void Compare_ShouldReturnNegativeOne_WhenXIsBeforeY(string? xValue, string yValue)
    {
        // Arrange
        var comparer = new WordOccurrenceComparer();
        var x = xValue == null ? null : new WordOccurrence(xValue, 1);
        var y = new WordOccurrence(yValue, 1);

        // Act
        int result = comparer.Compare(x, y);

        // Assert
        Assert.True(result < 0);
    }

    [Theory]
    [InlineData("banana", "apple")]
    [InlineData("apple", null)]
    public void Compare_ShouldReturnPositiveOne_WhenXIsAfterY(string xValue, string? yValue)
    {
        // Arrange
        var comparer = new WordOccurrenceComparer();
        var x =  new WordOccurrence(xValue, 1);
        var y = yValue == null ? null : new WordOccurrence(yValue, 1);

        // Act
        int result = comparer.Compare(x, y);

        // Assert
        Assert.True(result > 0);
    }

    [Theory]
    [InlineData("apple", "apple")]
    [InlineData("app","apple")]
    [InlineData(null, null)]
    public void Compare_ShouldReturnZero_WhenXAndYAreSame(string? xValue, string? yValue)
    {
        // Arrange
        var comparer = new WordOccurrenceComparer();
        var x = xValue == null ? null : new WordOccurrence("apple", 1);
        var y = yValue == null ? null :new WordOccurrence("apple", 1);

        // Act
        int result = comparer.Compare(x, y);

        // Assert
        Assert.Equal(0, result);
    }
}