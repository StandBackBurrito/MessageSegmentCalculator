namespace MessageSegmentCalculator.Tests;
public class GraphemeSplitterTest
{

    [Fact]
    public void SplitGraphemes_SimpleString_ReturnsCorrectGraphemes()
    {
        // Arrange
        string input = "hello";
        string[] expected = ["h", "e", "l", "l", "o"];

        // Act
        string[] result = GraphemeSplitter.SplitGraphemes(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void SplitGraphemes_EmptyString_ReturnsEmptyArray()
    {
        // Arrange
        string input = "";
        string[] expected = [];

        // Act
        string[] result = GraphemeSplitter.SplitGraphemes(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void SplitGraphemes_StringWithEmoji_ReturnsCorrectGraphemes()
    {
        // Arrange
        string input = "hello👋";
        string[] expected = ["h", "e", "l", "l", "o", "👋"];

        // Act
        string[] result = GraphemeSplitter.SplitGraphemes(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void SplitGraphemes_StringWithAccentedCharacters_ReturnsCorrectGraphemes()
    {
        // Arrange
        string input = "héllo";
        string[] expected = ["h", "é", "l", "l", "o"];

        // Act
        string[] result = GraphemeSplitter.SplitGraphemes(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void SplitGraphemes_StringWithSurrogatePairs_ReturnsCorrectGraphemes()
    {
        // Arrange
        string input = "𐍈hello";
        string[] expected = ["𐍈", "h", "e", "l", "l", "o"];

        // Act
        string[] result = GraphemeSplitter.SplitGraphemes(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void SplitGraphemes_StringWithNewline_ReturnsCorrectGraphemes()
    {
        // Arrange
        string input = "hello\r\nworld";
        string[] expected = { "h", "e", "l", "l", "o", "\r\n", "w", "o", "r", "l", "d" };

        // Act
        string[] result = GraphemeSplitter.SplitGraphemes(input);

        // Assert
        Assert.Equal(expected, result);
    }

}