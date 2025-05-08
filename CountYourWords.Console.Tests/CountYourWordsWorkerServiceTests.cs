using CountYourWords.Application.Interfaces;
using CountYourWords.Application.Models;
using CountYourWords.Console.Services;
using Moq;

namespace CountYourWords.Console.Tests;

public class CountYourWordsWorkerServiceTests
{
    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenDependenciesAreNull()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() => new CountYourWordsWorkerService(null, new Mock<IDocumentProcessor>().Object));
        Assert.Throws<ArgumentNullException>(() => new CountYourWordsWorkerService(new Mock<IDocumentProvider>().Object, null));
    }

    [Fact]
    public async Task ProcessDocumentAsync_ShouldProcessAndDisplayDocument()
    {
        // Arrange
        var mockDocumentProvider = new Mock<IDocumentProvider>();
        var mockDocumentProcessor = new Mock<IDocumentProcessor>();

        var documentPath = "input.txt";
        var documentContent = "hello world is it me your looking for";
        var processedDocument = new ProcessedDocument(new List<WordOccurrence>
        {
            new WordOccurrence("hello", 1),
            new WordOccurrence("world", 1),
            new WordOccurrence("is", 1),
            new WordOccurrence("it", 1),
            new WordOccurrence("me", 1),
            new WordOccurrence("your", 1),
            new WordOccurrence("looking", 1),
            new WordOccurrence("for", 1)
        });

        mockDocumentProvider
            .Setup(provider => provider.GetContentAsStringAsync(documentPath))
            .ReturnsAsync(documentContent);

        mockDocumentProcessor
            .Setup(processor => processor.GetProcessedDocument(documentContent))
            .Returns(processedDocument);

        var service = new CountYourWordsWorkerService(mockDocumentProvider.Object, mockDocumentProcessor.Object);

        // Act
        await service.ProcessDocumentAsync(documentPath);

        // Assert
        mockDocumentProvider.Verify(provider => provider.GetContentAsStringAsync(documentPath), Times.Once);
        mockDocumentProcessor.Verify(processor => processor.GetProcessedDocument(documentContent), Times.Once);
    }
}