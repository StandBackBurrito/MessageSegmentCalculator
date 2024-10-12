namespace MessageSegmentCalculator;

/// <summary>
/// Interface for a segment element
/// </summary>
public interface ISegmentElement
{
    /// <summary>
    /// True if the character is a reserved char
    /// </summary>
    /// <remarks>
    /// Reserved characters are used for message headers and are not part of the message body.
    /// </remarks>
    public bool IsReservedChar { get; }

    /// <summary>
    /// True if the character is a user data header
    /// </summary>
    /// <remarks>
    /// User data headers are used to link mulitple segments of a message together.
    /// </remarks>
    public bool IsUserDataHeader { get; }

    /// <summary>
    /// Raw character (grapheme) as passed in the constructor
    /// </summary>
    string Raw { get; }

    /// <summary>
    /// Return the number of bits used by the character
    /// </summary>
    /// <returns>The number of bits used by the character</returns>
    int SizeInBits();

    /// <summary>
    /// Return the number of bits used by a code unit
    /// </summary>
    /// <returns>The number of bits used by a code unit</returns>
    int CodeUnitSizeInBits();
}
