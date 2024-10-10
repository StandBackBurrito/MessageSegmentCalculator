using System.Globalization;

namespace MessageSegmentCalculator;
public class GraphemeSplitter()
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
}