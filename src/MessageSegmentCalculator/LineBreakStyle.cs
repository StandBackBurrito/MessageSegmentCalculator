namespace MessageSegmentCalculator;

/// <summary>
/// Line break style used in the message
/// </summary>
public enum LineBreakStyle
{
  /// <summary>
  /// Line feed
  /// </summary>
  LF,

  /// <summary>
  /// Carriage return + Line feed
  /// </summary>
  CRLF,

  /// <summary>
  /// Mixed line feed and carriage return + line feed
  /// </summary>
  LFCRLF,

  /// <summary>
  /// No line breaks
  /// </summary>
  NONE,
}
