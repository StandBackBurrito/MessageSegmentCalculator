using MessageSegmentCalculator;
using FluentAssertions;
using Xunit;
using System.Linq;

namespace MessageSegmentCalculator.Tests;

public class IndexTests
{
    public static readonly List<object[]> GSM7EscapeChars =
    [
        ['|'],
        ['^'],
        ['€'],
        ['{'],
        ['}'],
        ['['],
        [']'],
        ['~'],
        ['\\'],
    ];

    [Theory]
    [SmartEncodingMap]
    public void SmartEncoding_WithSmartEncodingEnabled_MapsAllChars(string key, string value)
    {
        var segmentedMessage = new SegmentedMessage(key, SmsEncoding.Auto, true);
        String.Join("", segmentedMessage.Graphemes).Should().Be(value);
    }

    [Theory]
    [SmartEncodingMap]
    public void SmartEncoding_WithSmartEncodingDisabled_DoesNotModifyChars(string key, string value)
    {
        var segmentedMessage = new SegmentedMessage(key, SmsEncoding.Auto, false);
        String.Join("", segmentedMessage.Graphemes).Should().Be(key);
    }

    [Fact]
    public void SmartEncoding_ReplaceAllSmartEncodingCharsAtOnce()
    {
        var testString = string.Join("", SmartEncoding.SmartEncodingMap.Keys);
        var expected = string.Join("", SmartEncoding.SmartEncodingMap.Values);
        var segmentedMessage = new SegmentedMessage(testString, SmsEncoding.Auto, true);
        var result = String.Join("", segmentedMessage.Graphemes);
        result.Should().Be(expected);
    }

    [Theory]
    [TestData]
    public void BasicTests(TestDataItem testMessage)
    {
        var segmentedMessage = new SegmentedMessage(testMessage.Body);
        segmentedMessage.EncodingName.Should().Be(testMessage.Encoding);
        segmentedMessage.Segments.Length.Should().Be(testMessage.Segments);
        segmentedMessage.SegmentsCount.Should().Be(testMessage.Segments);
        segmentedMessage.MessageSize.Should().Be(testMessage.MessageSize);
        segmentedMessage.TotalSize.Should().Be(testMessage.TotalSize);
        segmentedMessage.NumberOfUnicodeScalars.Should().Be(testMessage.UnicodeScalars);
        segmentedMessage.NumberOfCharacters.Should().Be(testMessage.Characters);
    }

    [Theory]
    [MemberData(nameof(GSM7EscapeChars))]
    public void GSM7EscapeCharacters_OneSegment(char escapeChar)
    {
        var testMessage = $"{escapeChar}12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678";
        var segmentedMessage = new SegmentedMessage(testMessage);
        segmentedMessage.EncodingName.Should().Be("GSM7");
        segmentedMessage.Segments.Length.Should().Be(1);
        segmentedMessage.SegmentsCount.Should().Be(1);
        segmentedMessage.MessageSize.Should().Be(1120);
        segmentedMessage.TotalSize.Should().Be(1120);
    }

    [Theory]
    [MemberData(nameof(GSM7EscapeChars))]
    public void GSM7EscapeCharacters_TwoSegments(char escapeChar)
    {
        var testMessage = $"{escapeChar}123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789";
        var segmentedMessage = new SegmentedMessage(testMessage);
        segmentedMessage.EncodingName.Should().Be("GSM7");
        segmentedMessage.Segments.Length.Should().Be(2);
        segmentedMessage.SegmentsCount.Should().Be(2);
        segmentedMessage.MessageSize.Should().Be(1127);
        segmentedMessage.TotalSize.Should().Be(1223);
    }

    [Theory]
    [InlineData('Á'), InlineData('Ú'), InlineData('ú'), InlineData('ç'), InlineData('í'), InlineData('Í'), InlineData('ó'), InlineData('Ó')]
    public void OneGraphemeUCS2Characters_OneSegment(char character)
    {
        var testMessage = new string(character, 70);
        var segmentedMessage = new SegmentedMessage(testMessage);
        segmentedMessage.SegmentsCount.Should().Be(1);
        segmentedMessage.EncodedChars.All(c => !c.IsGSM7).Should().BeTrue();
    }

    [Theory]
    [InlineData('Á'), InlineData('Ú'), InlineData('ú'), InlineData('ç'), InlineData('í'), InlineData('Í'), InlineData('ó'), InlineData('Ó')]
    public void OneGraphemeUCS2Characters_TwoSegments(char character)
    {
        var testMessage = new string(character, 71);
        var segmentedMessage = new SegmentedMessage(testMessage);
        segmentedMessage.SegmentsCount.Should().Be(2);
        segmentedMessage.EncodedChars.All(c => !c.IsGSM7).Should().BeTrue();
    }

    [Fact]
    public void SpecialTests_UCS2MessageWithSpecialGSMCharacters_OneSegment()
    {
        var testMessage = "😀]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]";
        var segmentedMessage = new SegmentedMessage(testMessage);
        segmentedMessage.SegmentsCount.Should().Be(1);
    }

    [Fact]
    public void SpecialTests_UCS2MessageWithSpecialGSMCharacters_TwoSegments()
    {
        var testMessage = "😀]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]";
        var segmentedMessage = new SegmentedMessage(testMessage);
        segmentedMessage.SegmentsCount.Should().Be(2);
    }

    [Fact]
    public void LineBreakStylesTests_MessageWithCRLFLineBreakStyle()
    {
        var testMessage = "\rabcde\r\n123";
        var segmentedMessage = new SegmentedMessage(testMessage);
        segmentedMessage.NumberOfCharacters.Should().Be(11);
    }

    [Fact]
    public void LineBreakStylesTests_MessageWithLFLineBreakStyle()
    {
        var testMessage = "\nabcde\n\n123\n";
        var segmentedMessage = new SegmentedMessage(testMessage);
        segmentedMessage.NumberOfCharacters.Should().Be(12);
    }

    [Fact]
    public void LineBreakStylesTests_TripleAccentsCharacters_UnicodeTest()
    {
        var testMessage = "é́́";
        var segmentedMessage = new SegmentedMessage(testMessage);
        segmentedMessage.NumberOfCharacters.Should().Be(1);
        segmentedMessage.NumberOfUnicodeScalars.Should().Be(4);
    }

    [Fact]
    public void LineBreakStylesTests_TripleAccentsCharacters_OneSegmentTest()
    {
        var testMessage = "é́́aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
        var segmentedMessage = new SegmentedMessage(testMessage);
        segmentedMessage.SegmentsCount.Should().Be(1);
    }

    [Fact]
    public void LineBreakStylesTests_TripleAccentsCharacters_TwoSegmentsTest()
    {
        var testMessage = "é́́aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
        var segmentedMessage = new SegmentedMessage(testMessage);
        segmentedMessage.SegmentsCount.Should().Be(2);
    }
}