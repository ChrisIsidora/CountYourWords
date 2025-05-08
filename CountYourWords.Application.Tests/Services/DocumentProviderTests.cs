using CountYourWords.Application.Services;
using Microsoft.Extensions.FileProviders;
using Moq;

namespace CountYourWords.Application.Tests.Services;

public class DocumentProviderTests
{
    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenFileProviderIsNull()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new DocumentProvider(null));
    }
    
    [Fact]
    public async Task GetContentAsStringAsync_ShouldReturnContent_WhenFileExists()
    {
        // Arrange
        var mockFileProvider = new Mock<IFileProvider>();
        var mockFileInfo = new Mock<IFileInfo>();

        mockFileInfo.Setup(x => x.Exists).Returns(true);
        mockFileInfo.Setup(x => x.IsDirectory).Returns(false);
        mockFileInfo.Setup(x => x.CreateReadStream()).Returns(new MemoryStream(System.Text.Encoding.UTF8.GetBytes("test content")));
        mockFileProvider.Setup(x => x.GetFileInfo("input.txt")).Returns(mockFileInfo.Object).Verifiable();

        var documentProvider = new DocumentProvider(mockFileProvider.Object);

        // Act
        var content = await documentProvider.GetContentAsStringAsync("input.txt");

        // Assert
        Assert.Equal("test content", content);
        mockFileProvider.Verify();
    }

    [Fact]
    public async Task GetContentAsStringAsync_ShouldThrowArgumentException_WhenFilePathIsNullOrEmpty()
    {
        // Arrange
        var mockFileProvider = new Mock<IFileProvider>();
        var documentProvider = new DocumentProvider(mockFileProvider.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => documentProvider.GetContentAsStringAsync(string.Empty));
        await Assert.ThrowsAsync<ArgumentException>(() => documentProvider.GetContentAsStringAsync(" "));

        mockFileProvider.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task GetContentAsStringAsync_ShouldThrowFileNotFoundException_WhenFileDoesNotExist()
    {
        // Arrange
        var mockFileProvider = new Mock<IFileProvider>();
        var mockFileInfo = new Mock<IFileInfo>();
        mockFileInfo.Setup(x => x.Exists).Returns(false).Verifiable();
        mockFileProvider.Setup(x => x.GetFileInfo("input.txt")).Returns(mockFileInfo.Object).Verifiable();

        var documentProvider = new DocumentProvider(mockFileProvider.Object);

        // Act & Assert
        await Assert.ThrowsAsync<FileNotFoundException>(() => documentProvider.GetContentAsStringAsync("input.txt"));
        mockFileProvider.Verify();
    }

    [Fact]
    public async Task GetContentAsStringAsync_ShouldThrowArgumentException_WhenPathIsDirectory()
    {
        // Arrange
        var mockFileProvider = new Mock<IFileProvider>();
        var mockFileInfo = new Mock<IFileInfo>();

        mockFileInfo.Setup(x => x.Exists).Returns(true).Verifiable();
        mockFileInfo.Setup(x => x.IsDirectory).Returns(true).Verifiable();
        mockFileProvider.Setup(x => x.GetFileInfo("input.txt")).Returns(mockFileInfo.Object).Verifiable();

        var documentProvider = new DocumentProvider(mockFileProvider.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => documentProvider.GetContentAsStringAsync("input.txt"));
        mockFileProvider.Verify(); 
    }
}