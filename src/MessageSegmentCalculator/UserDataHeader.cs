namespace MessageSegmentCalculator;

/// <summary>
/// Represent a User Data Header https://en.wikipedia.org/wiki/User_Data_Header
/// Twilio messages reserve 6 of this per segment in a concatenated message
/// </summary>
public class UserDataHeader : ISegmentElement
{
    public bool IsReservedChar => true;
    public bool IsUserDataHeader => true;
    public string Raw => "";
    public int CodeUnitSizeInBits() => 8;
    public int SizeInBits() => 8;
}
