namespace MessageSegmentCalculator;

public interface ISegmentElement
{
    public bool IsReservedChar { get; }
    public bool IsUserDataHeader { get; }
    string Raw { get; }

    int SizeInBits();
    int CodeUnitSizeInBits();
}
