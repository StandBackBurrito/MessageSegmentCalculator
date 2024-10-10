using MessageSegmentCalculator;

/// <summary>
/// A segmented message
/// </summary>
/// <remarks>
/// This class is used to calculate the number of segments a message will be split into
/// and the encoding to use for each segment.
/// </remarks>
public class SegmentedMessage
{
    /// <summary>
    /// Encoding set in the varructor for the message. Allowed values: 'GSM-7', 'UCS-2', 'auto'.
    /// </summary>
    private SmsEncoding _declaredEncoding;

    /// <summary>
    /// Calculated encoding for the message. It can be 'GSM-7' or 'UCS-2'
    /// </summary>
    private SmsEncoding _encoding;

    /// <summary>
    /// Array of segment(s) the message have been segmented into
    /// </summary>
    public Segment[] Segments { get; private set; }

    /// <summary>
    /// Graphemes (array of strings) the message have been split into
    /// </summary>
    public string[] Graphemes { get; private set; }

    /// <summary>
    /// Number of Unicode Scalars (i.e. unicode pairs) the message is made of
    /// Some characters (e.g. extended emoji) can be made of more than one unicode pair
    /// </summary>
    public int NumberOfUnicodeScalars { get; private set; }

    /// <summary>
    /// Number of characters in the message. Each character count as 1 except for
    /// the characters in the GSM extension character set.
    /// </summary>
    public int NumberOfCharacters { get; private set; }

    /// <summary>
    /// Array of encoded characters composing the message
    /// </summary>
    public EncodedChar[] EncodedChars { get; private set; }

    /// <summary>
    /// Line break style used in the message
    /// </summary>
    public LineBreakStyle LineBreakStyle { get; private set; }

    /// <summary>
    /// warnings message line break style
    /// </summary>
    public string[] Warnings { get; private set; }

    /// <summary>
    /// Create a new segmented message from a string
    /// </summary>
    /// <param name="message">Body of the message</param>
    /// <param name="encoding">Optional: encoding. It can be 'GSM-7', 'UCS-2', 'auto'. Default value: 'auto'</param>
    /// <param name="smartEncoding">Optional: whether or not Twilio's [Smart Encoding](https://www.twilio.com/docs/messaging/services#smart-encoding) is emulated. Default value: false</param>
    /// <exception cref="Error">Encoding not supported. Valid values for encoding are GSM-7, UCS-2, auto</exception>
    /// <exception cref="Error">The string provided is incompatible with GSM-7 encoding</exception>
    public SegmentedMessage(string message, SmsEncoding encoding = SmsEncoding.AUTO, bool smartEncoding = false)
    {
        _declaredEncoding = encoding;

        if (smartEncoding)
        {
            message = SmartEncoding.Encode(message);
        }

        var initialGraphmems = GraphemeSplitter.SplitGraphemes(message);
        NumberOfUnicodeScalars = initialGraphmems.Length;

        Graphemes = initialGraphmems
            .SelectMany(x => x == "\r\n" ? ["\r", "\n"] : new[] { x })
            .ToArray();

        var hasAnyUCSCharacters = HasAnyUCSCharacters(Graphemes);

        if (_declaredEncoding == SmsEncoding.AUTO)
        {
            _encoding = hasAnyUCSCharacters
                ? SmsEncoding.UCS2
                : SmsEncoding.GSM7;
        }
        else
        {
            if (_declaredEncoding == SmsEncoding.GSM7 && hasAnyUCSCharacters)
            {
                throw new InvalidDataException("The string provided is incompatible with GSM-7 encoding");
            }

            _encoding = _declaredEncoding;
        }

        EncodedChars = EncodeChars(Graphemes);
        NumberOfCharacters = _encoding == SmsEncoding.UCS2
            ? Graphemes.Length
            : CountCodeUnits(EncodedChars);

        Segments = BuildSegments(EncodedChars);
        LineBreakStyle = DetectLineBreakStyle(message);
        Warnings = CheckForWarnings();
    }

    /// <summary>
    /// Check if the message has any non-GSM7 characters
    /// </summary>
    /// <param name="graphemes">Array of graphemes representing the message</param>
    /// <returns>True if there are non-GSM-7 characters</returns>
    /// <remarks>
    /// This method is used to determine if the message should be encoded as GSM-7 or UCS-2.
    /// </remarks>
    private static bool HasAnyUCSCharacters(string[] graphemes)
    {
        var result = false;

        foreach (var grapheme in graphemes)
        {
            if (grapheme.Length >= 2 || (grapheme.Length == 1 && !UnicodeToGsm.Map.ContainsKey(grapheme[0])))
            {
                result = true;
                break;
            }
        }

        return result;
    }

    /**
     * Internal method used to build message's segment(s)
     *
     * @param {object[]} encodedChars Array of EncodedChar
     * @returns {object[]} Array of Segment
     * @private
     */

    private Segment[] BuildSegments(EncodedChar[] encodedChars)
    {
        List<Segment> segments = [];
        segments.Add(new Segment());

        var currentSegment = segments[0];

        foreach (var encodedChar in encodedChars)
        {
            if (currentSegment.FreeSizeInBits() < encodedChar.SizeInBits())
            {
                segments.Add(new Segment(true));
                currentSegment = segments[segments.Count - 1];
                var previousSegment = segments[segments.Count - 2];

                if (!previousSegment.HasUserDataHeader)
                {
                    var removedChars = previousSegment.AddHeader();

                    foreach (var @char in removedChars)
                    {
                        currentSegment.Add(@char);
                    }
                }
            }

            currentSegment.Add(encodedChar);
        }

        return [.. segments];
    }

    /// <summary>
    /// Return the encoding of the message segment
    /// </summary>
    /// <returns>Encoding for the message segment(s)</returns>
    public string GetEncodingName() => _encoding.ToString();

    /// <summary>
    /// Create an array of EncodedChar from an array of graphemes
    /// </summary>
    /// <param name="graphemes">Array of graphemes representing the message</param>
    /// <returns>Array of EncodedChar</returns>
    /// <remarks>
    /// This method is used to encode the message body.
    /// </remarks>
    private EncodedChar[] EncodeChars(string[] graphemes)
    {
        List<EncodedChar> encodedChars = [];

        foreach (var grapheme in graphemes)
        {
            encodedChars.Add(new EncodedChar(grapheme, _encoding));
        }

        return [.. encodedChars];
    }

    /// <summary>
    /// Count the total number of code units of the message
    /// </summary>
    /// <param name="encodedChars">Array of EncodedChar</param>
    /// <returns>The total number of code units</returns>
    /// <remarks>
    /// This method is used to calculate the number of characters in the message.
    /// </remarks>
    private int CountCodeUnits(EncodedChar[] encodedChars)
        => encodedChars.Select(x => x.CodeUnits.Length).Sum();

    /// <summary>
    /// Return the total size of the message in bits
    /// </summary>
    /// <returns>Total size of the message in bits</returns>
    public int GetTotalSize()
        => Segments.Select(x => x.SizeInBits()).Sum();

    /// <summary>
    /// Return the total size of the message in bits (excluding User Data Header if present)
    /// </summary>
    /// <returns>Total size of the message in bits (excluding User Data Header if present)</returns>
    public int GetMessageSize()
        => Segments.Select(x => x.MessageSizeInBits()).Sum();

    /// <summary>
    /// Return the number of segments the message has been split into
    /// </summary>
    public int GetSegmentsCount() => Segments.Length;

    /// <summary>
    /// Return the non-GSM-7 characters in the message body
    /// </summary>
    /// <returns>Array of characters representing the non-GSM-7 characters in the message body</returns>
    public string[] GetNonGsmCharacters()
    {
        return EncodedChars
            .Where(encodedChar => !encodedChar.IsGSM7)
            .Select(encodedChar => encodedChar.Raw)
            .ToArray();
    }

    /// <summary>
    /// Detect the line break style used in the message
    /// </summary>
    /// <param name="message">Message body</param>
    /// <returns>Line break style name LF or CRLF</returns>
    public LineBreakStyle DetectLineBreakStyle(string message)
    {
        var hasWindowsStyle = message.Contains("\r\n");
        var hasUnixStyle = message.Contains('\n');

        var mixedStyle = hasWindowsStyle && hasUnixStyle;
        var noBreakLine = !hasWindowsStyle && !hasUnixStyle;

        if (noBreakLine)
        {
            return LineBreakStyle.NONE;
        }
        if (mixedStyle)
        {
            return LineBreakStyle.LFCRLF;
        }

        return hasUnixStyle ? LineBreakStyle.LF : LineBreakStyle.CRLF;
    }

     /// <summary>
     /// Check the line break styled used in the message
     /// </summary>
     /// <returns>Array of warnings</returns>
    public string[] CheckForWarnings()
    {
        List<string> warnings = [];
        if (LineBreakStyle == LineBreakStyle.LFCRLF || LineBreakStyle == LineBreakStyle.CRLF)
        {
            warnings.Add(
              "The message has line breaks, the web page utility only supports LF style. If you insert a CRLF it will be converted to LF."
            );
        }

        return [.. warnings];
    }
}
