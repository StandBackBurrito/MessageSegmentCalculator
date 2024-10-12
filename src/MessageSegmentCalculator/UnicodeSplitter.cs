using System.Globalization;

namespace MessageSegmentCalculator;

/// <summary>
/// Utility class to split a string into its graphemes or scalars
/// </summary>
/// <remarks>
/// This class is used to split a string into its graphemes or scalars.
/// </remarks>
/// <example>
/// <code>
/// var graphemes = UnicodeSplitter.SplitGraphemes("Hello, World!");
/// var scalars = UnicodeSplitter.SplitUnicodeScalers("Hello, World!");
/// </code>
/// </example>
public class UnicodeSplitter
{
    /// <summary>
    /// Split a string into its graphemes
    /// </summary>
    /// <param name="source">The string to split</param>
    /// <returns>An array of graphemes</returns>
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

    /// <summary>
    /// Split a string into its scalars
    /// </summary>
    /// <param name="source">The string to split</param>
    /// <returns>An array of scalars</returns>
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