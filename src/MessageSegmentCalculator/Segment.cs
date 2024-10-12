using System.Data;

namespace MessageSegmentCalculator;

/// <summary>
/// A modified List of ISegmentElement representing one segment and add some helper functions
/// /// </summary>
public class Segment
{
    /// <summary>
    /// Max size of a SMS is 140 octets -> 140 * 8bits = 1120 bits
    /// </summary>
    const int MaxBitsInSegment = 1120;

    /// <summary>
    /// List of ISegmentElement representing one segment
    /// </summary>
    /// <returns>List of ISegmentElement representing one segment</returns>
    private readonly List<ISegmentElement> _elements = [];

    /// <summary>
    /// True if the segment has Twilio reserved bits
    /// </summary>
    /// <returns>True if the segment has Twilio reserved bits</returns>
    public bool HasTwilioReservedBits { get; private set; }

    /// <summary>
    /// True if the segment has a User Data Header
    /// </summary>
    /// <returns>True if the segment has a User Data Header</returns>
    public bool HasUserDataHeader { get; private set; }

    /// <summary>
    /// Create a new Segment
    /// </summary>
    /// <param name="withUserDataHeader">True if the segment has a User Data Header</param>
    /// <returns>Array of ISegmentElement that were removed to make space for the User Data Header</returns>
    public Segment(bool withUserDataHeader = false)
    {
        HasTwilioReservedBits = withUserDataHeader;
        HasUserDataHeader = withUserDataHeader;

        if (withUserDataHeader)
        {
            for (int i = 0; i < 6; i++)
            {
                _elements.Add(new UserDataHeader());
            }
        }
    }

    /// <summary>
    /// Size in bits *including* User Data Header (if present)
    /// </summary>
    /// <returns>Size in bits *including* User Data Header (if present)</returns>
    public int SizeInBits()
    {
        //return this.reduce((accumulator: number, encodedChar: Segment) => accumulator + encodedChar.sizeInBits(), 0);
        var size = _elements.Select(x => x.SizeInBits()).Sum();
        return size;
    }

    /// <summary>
    /// Size in bits *excluding* User Data Header (if present)
    /// </summary>
    /// <returns>Size in bits *excluding* User Data Header (if present)</returns>
    public int MessageSizeInBits()
    {
        var size = _elements
                .Where(x => x is not UserDataHeader)
                .Select(x => x.SizeInBits())
                .Sum();

        return size;
    }

    /// <summary>
    /// Return the number of bits available in the segment
    /// </summary>
    /// <returns>The number of bits available in the segment</returns>
    public int FreeSizeInBits()
    {
        return MaxBitsInSegment - SizeInBits();
    }

    /// <summary>
    /// Add a User Data Header to the segment
    /// </summary>
    /// <returns>Array of ISegmentElement that were removed to make space for the User Data Header</returns>
    public ISegmentElement[] AddHeader()
    {
        List<ISegmentElement> leftOverChar = [];

        if (HasUserDataHeader)
        {
            return [];
        }

        HasTwilioReservedBits = true;
        HasUserDataHeader = false;

        for (int i = 0; i < 6; i++)
        {
            _elements.Insert(0, new UserDataHeader());
        }

        // Remove characters
        while (FreeSizeInBits() < 0)
        {
            var lastIndex = _elements.Count - 1;
            leftOverChar.Insert(0, _elements[lastIndex]);
            _elements.RemoveAt(lastIndex);
        }

        leftOverChar.Reverse();
        return [.. leftOverChar];
    }

    /// <summary>
    /// Add an ISegmentElement to the segment
    /// </summary>
    public void Add(ISegmentElement encodedChar) => _elements.Add(encodedChar);

    /// <summary>
    /// Return the ISegmentElement at the given index
    /// </summary>
    /// <param name="index">The index of the ISegmentElement</param>
    /// <returns>The ISegmentElement at the given index</returns>
    public ISegmentElement this[int index]
    {
        get => _elements[index];
        set => _elements[index] = value;
    }

    /// <summary>
    /// Return the number of ISegmentElement in the segment
    /// </summary>
    /// <returns>The number of ISegmentElement in the segment</returns>
    public int Length => _elements.Count;

    /// <summary>
    /// Return the number of ISegmentElement in the segment
    /// </summary>
    /// <returns>The number of ISegmentElement in the segment</returns>
    public int Count => _elements.Count;
}