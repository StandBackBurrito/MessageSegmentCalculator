
using System.Globalization;
using System.Text;

namespace MessageSegmentCalculator;

public class SmartEncoding
{
    public static string Encode(string message)
    {
        var sb = new StringBuilder();

        var charEnumerator = StringInfo.GetTextElementEnumerator(message);
        while (charEnumerator.MoveNext())
        {
            var currentChar = charEnumerator.GetTextElement();
            var newChar = SmartEncodingMap.TryGetValue(currentChar, out var replacement)
                            ? replacement
                            : currentChar;

            sb.Append(newChar);
        }

        return sb.ToString();
    }

    public static readonly Dictionary<string, string> SmartEncodingMap = new()
    {
        { "\u00ab", "\"" }, // LEFT-POINTING DOUBLE ANGLE QUOTATION MARK
        { "\u00bb", "\"" }, // RIGHT-POINTING DOUBLE ANGLE QUOTATION MARK
        { "\u201c", "\"" }, // LEFT DOUBLE QUOTATION MARK
        { "\u201d", "\"" }, // RIGHT DOUBLE QUOTATION MARK
        { "\u02ba", "\"" }, // MODIFIER LETTER DOUBLE PRIME
        { "\u02ee", "\"" }, // MODIFIER LETTER DOUBLE APOSTROPHE
        { "\u201f", "\"" }, // DOUBLE HIGH-REVERSED-9 QUOTATION MARK
        { "\u201E", "\"" }, // DOUBLE LOW-9 QUOTATION MARK
        { "\u275d", "\"" }, // HEAVY DOUBLE TURNED COMMA QUOTATION MARK ORNAMENT
        { "\u275e", "\"" }, // HEAVY DOUBLE COMMA QUOTATION MARK ORNAMENT
        { "\u301d", "\"" }, // REVERSED DOUBLE PRIME QUOTATION MARK
        { "\u301e", "\"" }, // DOUBLE PRIME QUOTATION MARK
        { "\uff02", "\"" }, // FULLWIDTH QUOTATION MARK
        { "\u2018", "'" }, // LEFT SINGLE QUOTATION MARK
        { "\u2019", "'" }, // RIGHT SINGLE QUOTATION MARK
        { "\u02BB", "'" }, // MODIFIER LETTER TURNED COMMA
        { "\u02c8", "'" }, // MODIFIER LETTER VERTICAL LINE
        { "\u02bc", "'" }, // MODIFIER LETTER APOSTROPHE
        { "\u02bd", "'" }, // MODIFIER LETTER REVERSED COMMA
        { "\u02b9", "'" }, // MODIFIER LETTER PRIME
        { "\u201b", "'" }, // SINGLE HIGH-REVERSED-9 QUOTATION MARK
        { "\uff07", "'" }, // FULLWIDTH APOSTROPHE
        { "\u00b4", "'" }, // ACUTE ACCENT
        { "\u02ca", "'" }, // MODIFIER LETTER ACUTE ACCENT
        { "\u0060", "'" }, // GRAVE ACCENT
        { "\u02cb", "'" }, // MODIFIER LETTER GRAVE ACCENT
        { "\u275b", "'" }, // HEAVY SINGLE TURNED COMMA QUOTATION MARK ORNAMENT
        { "\u275c", "'" }, // HEAVY SINGLE COMMA QUOTATION MARK ORNAMENT
        { "\u0313", "'" }, // COMBINING COMMA ABOVE
        { "\u0314", "'" }, // COMBINING REVERSED COMMA ABOVE
        { "\ufe10", "'" }, // PRESENTATION FORM FOR VERTICAL COMMA
        { "\ufe11", "'" }, // PRESENTATION FORM FOR VERTICAL IDEOGRAPHIC COMMA
        { "\u00F7", "/" }, // DIVISION SIGN
        { "\u00bc", "1/4" }, // VULGAR FRACTION ONE QUARTER
        { "\u00bd", "1/2" }, // VULGAR FRACTION ONE HALF
        { "\u00be", "3/4" }, // VULGAR FRACTION THREE QUARTERS
        { "\u29f8", "/" }, // BIG SOLIDUS
        { "\u0337", "/" }, // COMBINING SHORT SOLIDUS OVERLAY
        { "\u0338", "/" }, // COMBINING LONG SOLIDUS OVERLAY
        { "\u2044", "/" }, // FRACTION SLASH
        { "\u2215", "/" }, // DIVISION SLASH
        { "\uff0f", "/" }, // FULLWIDTH SOLIDUS
        { "\u29f9", "\\" }, // BIG REVERSE SOLIDUS
        { "\u29f5", "\\" }, // REVERSE SOLIDUS OPERATOR
        { "\u20e5", "\\" }, // COMBINING REVERSE SOLIDUS OVERLAY
        { "\ufe68", "\\" }, // SMALL REVERSE SOLIDUS
        { "\uff3c", "\\" }, // FULLWIDTH REVERSE SOLIDUS
        { "\u0332", "_" }, // COMBINING LOW LINE
        { "\uff3f", "_" }, // FULLWIDTH LOW LINE
        { "\u20d2", "|" }, // COMBINING LONG VERTICAL LINE OVERLAY
        { "\u20d3", "|" }, // COMBINING SHORT VERTICAL LINE OVERLAY
        { "\u2223", "|" }, // DIVIDES
        { "\uff5c", "|" }, // FULLWIDTH VERTICAL LINE
        { "\u23b8", "|" }, // LEFT VERTICAL BOX LINE
        { "\u23b9", "|" }, // RIGHT VERTICAL BOX LINE
        { "\u23d0", "|" }, // VERTICAL LINE EXTENSION
        { "\u239c", "|" }, // LEFT PARENTHESIS EXTENSION
        { "\u239f", "|" }, // RIGHT PARENTHESIS EXTENSION
        { "\u23bc", "-" }, // HORIZONTAL SCAN LINE-7
        { "\u23bd", "-" }, // HORIZONTAL SCAN LINE-9
        { "\u2015", "-" }, // HORIZONTAL BAR
        { "\ufe63", "-" }, // SMALL HYPHEN-MINUS
        { "\uff0d", "-" }, // FULLWIDTH HYPHEN-MINUS
        { "\u2010", "-" }, // HYPHEN
        { "\u2043", "-" }, // HYPHEN BULLET
        { "\ufe6b", "@" }, // SMALL COMMERCIAL AT
        { "\uff20", "@" }, // FULLWIDTH COMMERCIAL AT
        { "\ufe69", "$" }, // SMALL DOLLAR SIGN
        { "\uff04", "$" }, // FULLWIDTH DOLLAR SIGN
        { "\u01c3", "!" }, // LATIN LETTER RETROFLEX CLICK
        { "\ufe15", "!" }, // PRESENTATION FORM FOR VERTICAL EXLAMATION MARK
        { "\ufe57", "!" }, // SMALL EXCLAMATION MARK
        { "\uff01", "!" }, // FULLWIDTH EXCLAMATION MARK
        { "\ufe5f", "#" }, // SMALL NUMBER SIGN
        { "\uff03", "#" }, // FULLWIDTH NUMBER SIGN
        { "\ufe6a", "%" }, // SMALL PERCENT SIGN
        { "\uff05", "%" }, // FULLWIDTH PERCENT SIGN
        { "\ufe60", "&" }, // SMALL AMPERSAND
        { "\uff06", "&" }, // FULLWIDTH AMPERSAND
        { "\u201a", "," }, // SINGLE LOW-9 QUOTATION MARK
        { "\u0326", "," }, // COMBINING COMMA BELOW
        { "\ufe50", "," }, // SMALL COMMA
        { "\ufe51", "," }, // SMALL IDEOGRAPHIC COMMA
        { "\uff0c", "," }, // FULLWIDTH COMMA
        { "\uff64", "," }, // HALFWIDTH IDEOGRAPHIC COMMA
        { "\u2768", "(" }, // MEDIUM LEFT PARENTHESIS ORNAMENT
        { "\u276a", "(" }, // MEDIUM FLATTENED LEFT PARENTHESIS ORNAMENT
        { "\ufe59", "(" }, // SMALL LEFT PARENTHESIS
        { "\uff08", "(" }, // FULLWIDTH LEFT PARENTHESIS
        { "\u27ee", "(" }, // MATHEMATICAL LEFT FLATTENED PARENTHESIS
        { "\u2985", "(" }, // LEFT WHITE PARENTHESIS
        { "\u2769", ")" }, // MEDIUM RIGHT PARENTHESIS ORNAMENT
        { "\u276b", ")" }, // MEDIUM FLATTENED RIGHT PARENTHESIS ORNAMENT
        { "\ufe5a", ")" }, // SMALL RIGHT PARENTHESIS
        { "\uff09", ")" }, // FULLWIDTH RIGHT PARENTHESIS
        { "\u27ef", ")" }, // MATHEMATICAL RIGHT FLATTENED PARENTHESIS
        { "\u2986", ")" }, // RIGHT WHITE PARENTHESIS
        { "\u204e", "*" }, // LOW ASTERISK
        { "\u2217", "*" }, // ASTERISK OPERATOR
        { "\u229B", "*" }, // CIRCLED ASTERISK OPERATOR
        { "\u2722", "*" }, // FOUR TEARDROP-SPOKED ASTERISK
        { "\u2723", "*" }, // FOUR BALLOON-SPOKED ASTERISK
        { "\u2724", "*" }, // HEAVY FOUR BALLOON-SPOKED ASTERISK
        { "\u2725", "*" }, // FOUR CLUB-SPOKED ASTERISK
        { "\u2731", "*" }, // HEAVY ASTERISK
        { "\u2732", "*" }, // OPEN CENTRE ASTERISK
        { "\u2733", "*" }, // EIGHT SPOKED ASTERISK
        { "\u273a", "*" }, // SIXTEEN POINTED ASTERISK
        { "\u273b", "*" }, // TEARDROP-SPOKED ASTERISK
        { "\u273c", "*" }, // OPEN CENTRE TEARDROP-SPOKED ASTERISK
        { "\u273d", "*" }, // HEAVY TEARDROP-SPOKED ASTERISK
        { "\u2743", "*" }, // HEAVY TEARDROP-SPOKED PINWHEEL ASTERISK
        { "\u2749", "*" }, // BALLOON-SPOKED ASTERISK
        { "\u274a", "*" }, // EIGHT TEARDROP-SPOKED PROPELLER ASTERISK
        { "\u274b", "*" }, // HEAVY EIGHT TEARDROP-SPOKED PROPELLER ASTERISK
        { "\u29c6", "*" }, // SQUARED ASTERISK
        { "\ufe61", "*" }, // SMALL ASTERISK
        { "\uff0a", "*" }, // FULLWIDTH ASTERISK
        { "\u02d6", "+" }, // MODIFIER LETTER PLUS SIGN
        { "\ufe62", "+" }, // SMALL PLUS SIGN
        { "\uff0b", "+" }, // FULLWIDTH PLUS SIGN
        { "\u3002", "." }, // IDEOGRAPHIC FULL STOP
        { "\ufe52", "." }, // SMALL FULL STOP
        { "\uff0e", "." }, // FULLWIDTH FULL STOP
        { "\uff61", "." }, // HALFWIDTH IDEOGRAPHIC FULL STOP
        { "\uff10", "0" }, // FULLWIDTH DIGIT ZERO
        { "\uff11", "1" }, // FULLWIDTH DIGIT ONE
        { "\uff12", "2" }, // FULLWIDTH DIGIT TWO
        { "\uff13", "3" }, // FULLWIDTH DIGIT THREE
        { "\uff14", "4" }, // FULLWIDTH DIGIT FOUR
        { "\uff15", "5" }, // FULLWIDTH DIGIT FIVE
        { "\uff16", "6" }, // FULLWIDTH DIGIT SIX
        { "\uff17", "7" }, // FULLWIDTH DIGIT SEVEN
        { "\uff18", "8" }, // FULLWIDTH DIGIT EIGHT
        { "\uff19", "9" }, // FULLWIDTH DIGIT NINE
        { "\u02d0", ":" }, // MODIFIER LETTER TRIANGULAR COLON
        { "\u02f8", ":" }, // MODIFIER LETTER RAISED COLON
        { "\u2982", ":" }, // Z NOTATION TYPE COLON
        { "\ua789", ":" }, // MODIFIER LETTER COLON
        { "\ufe13", ":" }, // PRESENTATION FORM FOR VERTICAL COLON
        { "\uff1a", ":" }, // FULLWIDTH COLON
        { "\u204f", ";" }, // REVERSED SEMICOLON
        { "\ufe14", ";" }, // PRESENTATION FORM FOR VERTICAL SEMICOLON
        { "\ufe54", ";" }, // SMALL SEMICOLON
        { "\uff1b", ";" }, // FULLWIDTH SEMICOLON
        { "\ufe64", "<" }, // SMALL LESS-THAN SIGN
        { "\uff1c", "<" }, // FULLWIDTH LESS-THAN SIGN
        { "\u0347", "=" }, // COMBINING EQUALS SIGN BELOW
        { "\ua78a", "=" }, // MODIFIER LETTER SHORT EQUALS SIGN
        { "\ufe66", "=" }, // SMALL EQUALS SIGN
        { "\uff1d", "=" }, // FULLWIDTH EQUALS SIGN
        { "\ufe65", ">" }, // SMALL GREATER-THAN SIGN
        { "\uff1e", ">" }, // FULLWIDTH GREATER-THAN SIGN
        { "\ufe16", "?" }, // PRESENTATION FORM FOR VERTICAL QUESTION MARK
        { "\ufe56", "?" }, // SMALL QUESTION MARK
        { "\uff1f", "?" }, // FULLWIDTH QUESTION MARK
        { "\uff21", "A" }, // FULLWIDTH LATIN CAPITAL LETTER A
        { "\u1d00", "A" }, // LATIN LETTER SMALL CAPITAL A
        { "\uff22", "B" }, // FULLWIDTH LATIN CAPITAL LETTER B
        { "\u0299", "B" }, // LATIN LETTER SMALL CAPITAL B
        { "\uff23", "C" }, // FULLWIDTH LATIN CAPITAL LETTER C
        { "\u1d04", "C" }, // LATIN LETTER SMALL CAPITAL C
        { "\uff24", "D" }, // FULLWIDTH LATIN CAPITAL LETTER D
        { "\u1d05", "D" }, // LATIN LETTER SMALL CAPITAL D
        { "\uff25", "E" }, // FULLWIDTH LATIN CAPITAL LETTER E
        { "\u1d07", "E" }, // LATIN LETTER SMALL CAPITAL E
        { "\uff26", "F" }, // FULLWIDTH LATIN CAPITAL LETTER F
        { "\ua730", "F" }, // LATIN LETTER SMALL CAPITAL F
        { "\uff27", "G" }, // FULLWIDTH LATIN CAPITAL LETTER G
        { "\u0262", "G" }, // LATIN LETTER SMALL CAPITAL G
        { "\uff28", "H" }, // FULLWIDTH LATIN CAPITAL LETTER H
        { "\u029c", "H" }, // LATIN LETTER SMALL CAPITAL H
        { "\uff29", "I" }, // FULLWIDTH LATIN CAPITAL LETTER I
        { "\u026a", "I" }, // LATIN LETTER SMALL CAPITAL I
        { "\uff2a", "J" }, // FULLWIDTH LATIN CAPITAL LETTER J
        { "\u1d0a", "J" }, // LATIN LETTER SMALL CAPITAL J
        { "\uff2b", "K" }, // FULLWIDTH LATIN CAPITAL LETTER K
        { "\u1d0b", "K" }, // LATIN LETTER SMALL CAPITAL K
        { "\uff2c", "L" }, // FULLWIDTH LATIN CAPITAL LETTER L
        { "\u029f", "L" }, // LATIN LETTER SMALL CAPITAL L
        { "\uff2d", "M" }, // FULLWIDTH LATIN CAPITAL LETTER M
        { "\u1d0d", "M" }, // LATIN LETTER SMALL CAPITAL M
        { "\uff2e", "N" }, // FULLWIDTH LATIN CAPITAL LETTER N
        { "\u0274", "N" }, // LATIN LETTER SMALL CAPITAL N
        { "\uff2f", "O" }, // FULLWIDTH LATIN CAPITAL LETTER O
        { "\u1d0f", "O" }, // LATIN LETTER SMALL CAPITAL O
        { "\uff30", "P" }, // FULLWIDTH LATIN CAPITAL LETTER P
        { "\u1d18", "P" }, // LATIN LETTER SMALL CAPITAL P
        { "\uff31", "Q" }, // FULLWIDTH LATIN CAPITAL LETTER Q
        { "\uff32", "R" }, // FULLWIDTH LATIN CAPITAL LETTER R
        { "\u0280", "R" }, // LATIN LETTER SMALL CAPITAL R
        { "\uff33", "S" }, // FULLWIDTH LATIN CAPITAL LETTER S
        { "\ua731", "S" }, // LATIN LETTER SMALL CAPITAL S
        { "\uff34", "T" }, // FULLWIDTH LATIN CAPITAL LETTER T
        { "\u1d1b", "T" }, // LATIN LETTER SMALL CAPITAL T
        { "\uff35", "U" }, // FULLWIDTH LATIN CAPITAL LETTER U
        { "\u1d1c", "U" }, // LATIN LETTER SMALL CAPITAL U
        { "\uff36", "V" }, // FULLWIDTH LATIN CAPITAL LETTER V
        { "\u1d20", "V" }, // LATIN LETTER SMALL CAPITAL V
        { "\uff37", "W" }, // FULLWIDTH LATIN CAPITAL LETTER W
        { "\u1d21", "W" }, // LATIN LETTER SMALL CAPITAL W
        { "\uff38", "X" }, // FULLWIDTH LATIN CAPITAL LETTER X
        { "\uff39", "Y" }, // FULLWIDTH LATIN CAPITAL LETTER Y
        { "\u028f", "Y" }, // LATIN LETTER SMALL CAPITAL Y
        { "\uff3a", "Z" }, // FULLWIDTH LATIN CAPITAL LETTER Z
        { "\u1d22", "Z" }, // LATIN LETTER SMALL CAPITAL Z
        { "\u02c6", "^" }, // MODIFIER LETTER CIRCUMFLEX ACCENT
        { "\u0302", "^" }, // COMBINING CIRCUMFLEX ACCENT
        { "\uff3e", "^" }, // FULLWIDTH CIRCUMFLEX ACCENT
        { "\u1dcd", "^" }, // COMBINING DOUBLE CIRCUMFLEX ABOVE
        { "\u2774", "{" }, // MEDIUM LEFT CURLY BRACKET ORNAMENT
        { "\ufe5b", "{" }, // SMALL LEFT CURLY BRACKET
        { "\uff5b", "{" }, // FULLWIDTH LEFT CURLY BRACKET
        { "\u2775", "}" }, // MEDIUM RIGHT CURLY BRACKET ORNAMENT
        { "\ufe5c", "}" }, // SMALL RIGHT CURLY BRACKET
        { "\uff5d", "}" }, // FULLWIDTH RIGHT CURLY BRACKET
        { "\uff3b", "[" }, // FULLWIDTH LEFT SQUARE BRACKET
        { "\uff3d", "]" }, // FULLWIDTH RIGHT SQUARE BRACKET
        { "\u02dc", "~" }, // SMALL TILDE
        { "\u02f7", "~" }, // MODIFIER LETTER LOW TILDE
        { "\u0303", "~" }, // COMBINING TILDE
        { "\u0330", "~" }, // COMBINING TILDE BELOW
        { "\u0334", "~" }, // COMBINING TILDE OVERLAY
        { "\u223c", "~" }, // TILDE OPERATOR
        { "\uff5e", "~" }, // FULLWIDTH TILDE
        { "\u00a0", "  " }, // NO-BREAK SPACE
        { "\u2000", "  " }, // EN QUAD
        { "\u2002", "  " }, // EN SPACE
        { "\u2003", "  " }, // EM SPACE
        { "\u2004", "  " }, // THREE-PER-EM SPACE
        { "\u2005", "  " }, // FOUR-PER-EM SPACE
        { "\u2006", "  " }, // SIX-PER-EM SPACE
        { "\u2007", "  " }, // FIGURE SPACE
        { "\u2008", "  " }, // PUNCTUATION SPACE
        { "\u2009", "  " }, // THIN SPACE
        { "\u200a", "  " }, // HAIR SPACE
        { "\u202f", "  " }, // NARROW NO-BREAK SPACE
        { "\u205f", "  " }, // MEDIUM MATHEMATICAL SPACE
        { "\u3000", "  " }, // IDEOGRAHPIC SPACE
        { "\u008d", "  " }, // REVERSE LINE FEED (standard LF looks like \n, this looks like a space)
        { "\u009f", "  " }, // <control>
        { "\u0080", "  " }, // C1 CONTROL CODES
        { "\u0090", "  " }, // DEVICE CONTROL STRING
        { "\u009b", "  " }, // CONTROL SEQUENCE INTRODUCER
        { "\u0010", "" }, // ESCAPE, DATA LINK (not visible)
        { "\u0009", "       " }, // TAB (7 spaces based on print statement in Python interpreter)
        { "\u0000", "" }, // NULL
        { "\u0003", "" }, // END OF TEXT
        { "\u0004", "" }, // END OF TRANSMISSION
        { "\u0017", "" }, // END OF TRANSMISSION BLOCK
        { "\u0019", "" }, // END OF MEDIUM
        { "\u0011", "" }, // DEVICE CONTROL ONE
        { "\u0012", "" }, // DEVICE CONTROL TWO
        { "\u0013", "" }, // DEVICE CONTROL THREE
        { "\u0014", "" }, // DEVICE CONTROL FOUR
        { "\u2060", "" }, // WORD JOINER
        { "\u2017", "_" }, // DOUBLE LOW LINE
        { "\u2014", "-" }, // EM DASH
        { "\u2013", "-" }, // EN DASH
        { "\u2039", ">" }, // Single left-pointing angle quotation mark
        { "\u203A", "<" }, // Single right-pointing angle quotation mark
        { "\u203C", "!!" }, // Double exclamation mark
        { "\u2028", " " }, // Whitespace: Line Separator
        { "\u2029", " " }, // Whitespace: Paragraph Separator
        { "\u2026", "..." }, // Whitespace: Narrow No-Break Space
        { "\u2001", " " }, // Whitespace: Medium Mathematical Space
        { "\u200b", "" }, // ZERO WIDTH SPACE
        { "\u3001", "," }, // IDEOGRAPHIC COMMA
        { "\uFEFF", "" }, // ZERO WIDTH NO-BREAK SPACE
        { "\u2022", "-" }, // Bullet
    };
}

