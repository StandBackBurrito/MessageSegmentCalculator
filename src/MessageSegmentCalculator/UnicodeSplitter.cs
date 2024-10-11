using System.Globalization;

namespace MessageSegmentCalculator;
public class UnicodeSplitter()
{
    public static string[] SplitGraphemes(string source)
    {
        var elementEnumerator = StringInfo.GetTextElementEnumerator(source);
        var graphmemes = new List<string>();

        while (elementEnumerator.MoveNext())
        {
            graphmemes.Add(elementEnumerator.GetTextElement());
        }

        return [.. graphmemes];
    }

    public static string[] SplitUnicodeScalers(string source)
    {
        var scalars = new List<string>();

        foreach (var scalar in source.EnumerateRunes())
        {
            scalars.Add(scalar.ToString());
        }

        return [.. scalars];
    }
}