namespace MessageSegmentCalculator;

/// <summary>
/// Represent a User Data Header https://en.wikipedia.org/wiki/User_Data_Header
/// Twilio messages reserve 6 of this per segment in a concatenated message
/// </summary>
public class UserDataHeader : ISegmentElement
{
    /// <summary>
    /// True if the character is a reserved char
    /// </summary>
    /// <remarks>
    /// Reserved characters are used for message headers and are not part of the message body.
    /// </remarks>
    public bool IsReservedChar => true;

    /// <summary>
    /// True if the character is a user data header
    /// </summary>
    /// <remarks>
    /// User data headers are used to link mulitple segments of a message together.
    /// </remarks>
    public bool IsUserDataHeader => true;

    /// <summary>
    /// Raw character (grapheme) as passed in the constructor
    /// </summary>
    public string Raw => "";

    /// <summary>
    /// Return the number of bits used by a code unit
    /// </summary>
    /// <returns>The number of bits used by a code unit</returns>
    public int CodeUnitSizeInBits() => 8;

    /// <summary>
    /// Return the number of bits used by the character
    /// </summary>
    /// <returns>The number of bits used by the character</returns>
    public int SizeInBits() => 8;
}
