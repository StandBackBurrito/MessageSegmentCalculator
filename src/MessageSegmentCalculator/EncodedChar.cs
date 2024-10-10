namespace MessageSegmentCalculator;

/// <summary>
/// Utility class to represent a character in a given encoding.
/// </summary>
public class EncodedChar : ISegmentElement
{
    public bool IsReservedChar => false;
    public bool IsUserDataHeader => false;

    /// <summary>
    /// Raw character (grapheme) as passed in the constructor
    /// </summary>
    public string Raw { get; private set; }

    /// <summary>
    /// Array of 8 bits number rapresenting the encoded character
    /// </summary>
    public byte[] CodeUnits { get; private set; }

    /// <summary>
    /// True if the character is a GSM7 one
    /// </summary>
    public bool IsGSM7 { get; private set; }

    /// <summary>
    /// Encoding to use for this char
    /// </summary>
    public SmsEncoding Encoding { get; private set; } = SmsEncoding.GSM7;

    /// <summary>
    /// Create a new EncodedChar
    /// </summary>
    /// <param name="char">The character to encode</param>
    /// <param name="encoding">The encoding to use</param>

    public EncodedChar(string @char, SmsEncoding encoding)
    {
        Raw = @char;
        Encoding = encoding;

        IsGSM7 = !string.IsNullOrWhiteSpace(@char)
          && @char.Length == 1
          && UnicodeToGsm.Map.ContainsKey(@char[0]);

        if (IsGSM7)
        {
            CodeUnits = UnicodeToGsm.Map[@char[0]];
        }
        else
        {
            CodeUnits = new byte[@char.Length];
            for (int i = 0; i < @char.Length; i++)
            {
                CodeUnits[i] = (byte)@char[i];
            }
        }
    }

    public int CodeUnitSizeInBits()
    {
        return Encoding switch
        {
            SmsEncoding.GSM7 => 7,
            SmsEncoding.UCS2 => 8,
            _ => throw new NotImplementedException(),
        };
    }

    public int SizeInBits()
    {
        if (Encoding == SmsEncoding.UCS2 && IsGSM7)
        {
            // GSM characters are always using 16 bits in UCS-2 encoding
            return 16;
        }

        var bitsPerUnits = Encoding == SmsEncoding.GSM7 ? 7 : 16;
        return bitsPerUnits * CodeUnits.Length;
    }
}
