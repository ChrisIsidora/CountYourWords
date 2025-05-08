using CountYourWords.Application.Services;

namespace CountYourWords.Application.Tests.Services;

public class DocumentProcessorTests
{
    [Theory]
    [InlineData("Hello world hello World")] // Different Cases
    [InlineData("Hello world hello 123 World")] // Include Numbers
    [InlineData("Hello world hello 123 World !")] // Include Symbols
    public void GetProcessedDocument_ShouldReturnCorrectWordCountsAndOrder(string content)
    {
        // Arrange
        var documentProcessor = new DocumentProcessor();

        // Act
        var processedDocument = documentProcessor.GetProcessedDocument(content);

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
    }

    [Fact]
    public void GetProcessedDocument_ShouldHandleEmptyContent()
    {
        // Arrange
        var documentProcessor = new DocumentProcessor();
        var content = "";

        // Act
        var processedDocument = documentProcessor.GetProcessedDocument(content);

        // Assert
        Assert.NotNull(processedDocument);
        Assert.Empty(processedDocument.WordOccurrences);
        Assert.Equal(0, processedDocument.TotalWordCount);
    }
}