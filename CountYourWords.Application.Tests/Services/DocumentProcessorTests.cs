using CountYourWords.Application.Interfaces;
using CountYourWords.Application.Models;
using CountYourWords.Application.Services;
using Moq;

namespace CountYourWords.Application.Tests.Services;

public class DocumentProcessorTests
{
    private readonly Mock<IListProcessor> _mockListProcessor;
    private readonly DocumentProcessor _documentProcessor;

    public DocumentProcessorTests()
    {
        _mockListProcessor = new Mock<IListProcessor>();
        _documentProcessor = new DocumentProcessor(_mockListProcessor.Object);
    }

    [Theory]
    [InlineData("Hello world hello World")] // Different Cases
    [InlineData("Hello world hello 123 World")] // Include Numbers
    [InlineData("Hello world hello 123 World !")] // Include Symbols
    public void GetProcessedDocument_ShouldReturnCorrectWordCountsAndOrder(string content)
    {
        // Arrange
        var expectedWordOccurrences = new List<WordOccurrence>
        {
            new("hello", 2),
            new("world", 2)
        };

        _mockListProcessor.Setup(x => x.SortUsingComparer(
                It.IsAny<List<WordOccurrence>>(), 
                It.IsAny<IComparer<WordOccurrence>>()))
            .Returns(expectedWordOccurrences);

        // Act
        var processedDocument = _documentProcessor.GetProcessedDocument(content);

        // Assert
        Assert.NotNull(processedDocument);
        Assert.Equal(4, processedDocument.TotalWordCount);
        Assert.Collection(
            processedDocument.WordOccurrences,
            first =>
            {
                Assert.Equal("hello", first.Word);
                Assert.Equal(2, first.Occurrences);
            },
            second =>
            {
                Assert.Equal("world", second.Word);
                Assert.Equal(2, second.Occurrences);
            }
        );

        _mockListProcessor.Verify(x => x.SortUsingComparer(
            It.Is<List<WordOccurrence>>(list => 
                list.Count == 2 &&
                list.Any(w => w.Word == "hello" && w.Occurrences == 2) &&
                list.Any(w => w.Word == "world" && w.Occurrences == 2)),
            It.IsAny<IComparer<WordOccurrence>>()), Times.Once);
    }

    [Fact]
    public void GetProcessedDocument_ShouldHandleEmptyContent()
    {
        // Arrange
        var content = "";
        var emptyList = new List<WordOccurrence>();

        _mockListProcessor.Setup(x => x.SortUsingComparer(
                It.IsAny<List<WordOccurrence>>(), 
                It.IsAny<IComparer<WordOccurrence>>()))
            .Returns(emptyList);

        // Act
        var processedDocument = _documentProcessor.GetProcessedDocument(content);

        // Assert
        Assert.NotNull(processedDocument);
        Assert.Empty(processedDocument.WordOccurrences);
        Assert.Equal(0, processedDocument.TotalWordCount);
        
        _mockListProcessor.Verify(x => x.SortUsingComparer(
            It.Is<List<WordOccurrence>>(list => list.Count == 0),
            It.IsAny<IComparer<WordOccurrence>>()), Times.Once);
    }
}