using CountYourWords.Application.Models;
using CountYourWords.Application.Services;

namespace CountYourWords.Application.Tests.Services
{
    public class ListProcessorTests
    {
        private readonly ListProcessor _listProcessor;
        private readonly WordOccurrenceComparer _wordOccurrenceComparer;

        public ListProcessorTests()
        {
            _listProcessor = new ListProcessor();
            _wordOccurrenceComparer = new WordOccurrenceComparer();
        }

        [Fact]
        public void SortUsingComparer_NullList_ThrowsArgumentNullException()
        {
            // Arrange
            List<WordOccurrence> nullList = null;

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => 
                _listProcessor.SortUsingComparer(nullList, _wordOccurrenceComparer));
            Assert.Equal("items", exception.ParamName);
        }

        [Fact]
        public void SortUsingComparer_NullComparer_ThrowsArgumentNullException()
        {
            // Arrange
            var list = new List<WordOccurrence> { new WordOccurrence("test", 1) };

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => 
                _listProcessor.SortUsingComparer(list, null));
            Assert.Equal("comparer", exception.ParamName);
        }

        [Fact]
        public void SortUsingComparer_MultipleWordOccurrences_SortsAsIntended()
        {
            // Arrange
            var originalList = new List<WordOccurrence>
            {
                new("fox", 3),
                new("the", 4),
                new("big", 2),
                new("brown", 2),
                new("lazy", 2),
                new("dog", 2),
                new("jumped", 2),
                new("over", 2)
            };

            // Act
            var result = _listProcessor.SortUsingComparer(originalList, _wordOccurrenceComparer);

            // Assert
            Assert.NotSame(originalList, result);
            Assert.Collection(result,
                item => Assert.Equal("big", item.Word),
                item => Assert.Equal("brown", item.Word),
                item => Assert.Equal("dog", item.Word),
                item => Assert.Equal("fox", item.Word),
                item => Assert.Equal("jumped", item.Word),
                item => Assert.Equal("lazy", item.Word),
                item => Assert.Equal("over", item.Word),
                item => Assert.Equal("the", item.Word)
            );
        }
    }
}