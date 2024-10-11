using FluentAssertions;

namespace MessageSegmentCalculator.Tests;

public class SegmentedMessageTests
{
    [Fact]
    public void GetNonGsmCharacters_ReturnsExpectedCharacters()
    {
        // Arrange
        var testMessage = "más";
        var segmentedMessage = new SegmentedMessage(testMessage);

        // Act
        var result = segmentedMessage.GetNonGsmCharacters();

        // Assert
        var expected = new[] { "á" };
        result.Should().BeEquivalentTo(expected);
    }
}