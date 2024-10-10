namespace MessageSegmentCalculator;

/// <summary>
/// Represent a User Data Header https://en.wikipedia.org/wiki/User_Data_Header
/// Twilio messages reserve 6 of this per segment in a concatenated message
/// </summary>
public class UserDataHeader : ISegmentElement
{
    public bool IsReservedChar { get; private set; } = true;
    public bool IsUserDataHeader { get; private set; } = true;

    public int CodeUnitSizeInBits() => 8;
    public int SizeInBits() => 8;
}
