namespace MessageSegmentCalculator;

/// <summary>
/// Encoding to use for the message
/// </summary>
public enum SmsEncoding
{
  /// <summary>
  /// GSM 7-bit encoding
  /// </summary>
  GSM7,

  /// <summary>
  /// UCS-2 encoding
  /// </summary>
  UCS2,

  /// <summary>
  /// Auto-detect encoding
  /// </summary>
  Auto,
}