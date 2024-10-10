using System.Data;

namespace MessageSegmentCalculator;

/**
 * Segment Class
 * A modified array representing one segment and add some helper functions
 */

/// <summary>
/// A modified array representing one segment and add some helper functions
/// </summary>
public class Segment
{
    /// <summary>
    /// Max size of a SMS is 140 octets -> 140 * 8bits = 1120 bits
    /// </summary>
    const int MaxBitsInSegment = 1120;
    private readonly List<ISegmentElement> _elements = [];
    public bool HasTwilioReservedBits { get; private set; }
    public bool HasUserDataHeader { get; private set; }

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

    // Size in bits *excluding* User Data Header (if present)
    public int MessageSizeInBits()
    {
        var size = _elements
                .Where(x => x is not UserDataHeader)
                .Select(x => x.SizeInBits())
                .Sum();

        return size;
    }


    public int FreeSizeInBits()
    {
        return MaxBitsInSegment - SizeInBits();
    }

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

    public void Add(ISegmentElement encodedChar) => _elements.Add(encodedChar);
}