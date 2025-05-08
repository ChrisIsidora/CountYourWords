using CountYourWords.Application.Interfaces;
using CountYourWords.Application.Models;
using CountYourWords.Application.Services;
using Moq;

namespace CountYourWords.Application.Tests.Services;

public class ScrambledDocumentProcessorDecoratorTests
{
    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenDependenciesAreNull()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            new ScrambledDocumentProcessorDecorator(null, new Mock<IScrambleService>().Object));
        Assert.Throws<ArgumentNullException>(() =>
            new ScrambledDocumentProcessorDecorator(new Mock<IDocumentProcessor>().Object, null));
    }

    [Fact]
    public void GetProcessedDocument_ShouldScrambleWords()
    {
        // Arrange
        var mockDocumentProcessor = new Mock<IDocumentProcessor>();
        var mockScrambleService = new Mock<IScrambleService>();

        var documentContent = "hello world";
        var processedDocument = new ProcessedDocument(new List<WordOccurrence>
        {
            new WordOccurrence("hello", 1),
            new WordOccurrence("world", 1)
        });

        mockDocumentProcessor.Setup(x => x.GetProcessedDocument(documentContent)).Returns(processedDocument)
            .Verifiable();
        mockScrambleService.Setup(x => x.Scramble("hello")).Returns("olleh").Verifiable();
        mockScrambleService.Setup(x => x.Scramble("world")).Returns("dlrow").Verifiable();

        var decorator =
            new ScrambledDocumentProcessorDecorator(mockDocumentProcessor.Object, mockScrambleService.Object);

        // Act
        var result = decorator.GetProcessedDocument(documentContent);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("olleh", result.WordOccurrences.First().Word);
        Assert.Equal("dlrow", result.WordOccurrences.Last().Word);

        mockDocumentProcessor.Verify();
        mockScrambleService.Verify();
    }

    [Fact]
    public void GetProcessedDocument_ShouldThrowArgumentException_WhenContentIsNullOrEmpty()
    {
        // Arrange
        var mockDocumentProcessor = new Mock<IDocumentProcessor>();
        var mockScrambleService = new Mock<IScrambleService>();
        var decorator =
            new ScrambledDocumentProcessorDecorator(mockDocumentProcessor.Object, mockScrambleService.Object);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => decorator.GetProcessedDocument(string.Empty));
        Assert.Throws<ArgumentException>(() => decorator.GetProcessedDocument(" "));

        mockDocumentProcessor.VerifyNoOtherCalls();
        mockScrambleService.VerifyNoOtherCalls();
    }
}