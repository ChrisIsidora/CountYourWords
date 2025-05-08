using CountYourWords.Application.Models;

namespace CountYourWords.Application.Tests.Models;

public class WordOccurrenceComparerTests
{
    [Fact]
    public void Compare_ShouldReturnNegativeOne_WhenCurrentIsBeforeOther()
    {
        // Arrange
        var comparer = new WordOccurrenceComparer();
        var current = new WordOccurrence("apple", 1);
        var other = new WordOccurrence("banana", 1);

        // Act
        int result = comparer.Compare(current, other);

        // Assert
        Assert.True(result < 0);
    }

    [Fact]
    public void Compare_ShouldReturnPositiveOne_WhenCurrentIsAfterOther()
    {
        // Arrange
        var comparer = new WordOccurrenceComparer();
        var current = new WordOccurrence("banana", 1);
        var other = new WordOccurrence("apple", 1);

        // Act
        int result = comparer.Compare(current, other);

        // Assert
        Assert.True(result > 0);
    }

    [Theory]
    [InlineData("apple", "apple")]
    [InlineData("app", "apple")]
    public void Compare_ShouldReturnZero_WhenCurrentAndOtherAreSame_OrAlreadyInOrder(string currentWord, string otherWord)
    {
        // Arrange
        var comparer = new WordOccurrenceComparer();
        var current = new WordOccurrence(currentWord, 1);
        var other = new WordOccurrence(otherWord, 1);

        // Act
        int result = comparer.Compare(current, other);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void Compare_ShouldReturnNegativeOne_WhenCurrentIsNull()
    {
        // Arrange
        var comparer = new WordOccurrenceComparer();
        WordOccurrence? current = null;
        var other = new WordOccurrence("apple", 1);

        // Act
        int result = comparer.Compare(current, other);

        // Assert
        Assert.True(result < 0);
    }

    [Fact]
    public void Compare_ShouldReturnPositiveOne_WhenOtherIsNull()
    {
        // Arrange
        var comparer = new WordOccurrenceComparer();
        var current = new WordOccurrence("apple", 1);
        WordOccurrence? other = null;

        // Act
        int result = comparer.Compare(current, other);

        // Assert
        Assert.True(result > 0);
    }
}