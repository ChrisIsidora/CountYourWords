using CountYourWords.Application.Services;

namespace CountYourWords.Application.Tests.Services;

public class ScrambleServiceTests
{
    [Theory]
    [InlineData("hello")]
    [InlineData("HELLO")]
    [InlineData("HeLlo")]
    public void Scramble_ShouldReturnScrambledString(string input)
    {
        // Arrange
        var expectedOutput = "oLlEh";
        
        // Act
        var output = new ScrambleService().Scramble(input);

        // Assert
        Assert.Equal(expectedOutput, output);
    }
}