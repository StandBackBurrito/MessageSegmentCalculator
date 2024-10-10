
using FluentAssertions;

namespace MessageSegmentCalculator.Tests;

public class SegmentedMessageTest
{

  [Fact]
  public void GSM7SegmentAnalysis_CheckUserDataHeader()
  {
    // Arrange
    var testMessage =
      "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567";

    // Act
    var segmentedMessage = new SegmentedMessage(testMessage);

    // Assert
    for (var segmentIndex = 0; segmentIndex <= 2; segmentIndex++)
    {
      for (var index = 0; index < 6; index++)
      {
        segmentedMessage.Segments[segmentIndex][index].IsUserDataHeader.Should().BeTrue();
      }
    }
  }

  [Fact]
  public void GSM7SegmentAnalysis_LastSegmentHasSingleCharacter()
  {
    // Arrange
    var testMessage =
      "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567";

    // Act
    var segmentedMessage = new SegmentedMessage(testMessage);

    // Assert
    segmentedMessage.Segments[2].Length.Should().Be(7);
    segmentedMessage.Segments[2][6].Raw.Should().Be("7");
  }

  [Fact]
  public void UCS2SegmentAnalysis_CheckUserDataHeader()
  {
    var testMessage =
      "😜2345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234";

    var segmentedMessage = new SegmentedMessage(testMessage);

      for (var segmentIndex = 0; segmentIndex <= 2; segmentIndex++)
      {
        for (var index = 0; index < 6; index++)
        {
          segmentedMessage.Segments[segmentIndex][index].IsUserDataHeader.Should().BeTrue();
        }
      }
  }

  [Fact]
  public void UCS2SegmentAnalysis_LastSegmentHasSingleCharacter()
  {
    var testMessage =
      "😜2345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234";

    var segmentedMessage = new SegmentedMessage(testMessage);

    segmentedMessage.Segments[2].Length.Should().Be(7);
    segmentedMessage.Segments[2][6].Raw.Should().Be("4");
  }
}