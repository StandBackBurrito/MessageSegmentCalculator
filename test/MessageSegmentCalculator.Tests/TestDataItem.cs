using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;


namespace MessageSegmentCalculator.Tests;

public class TestDataItem
{
    public required string TestDescription { get; set; }
    public required string Body { get; set; }
    public required string Encoding { get; set; }
    public int Segments { get; set; }
    public int MessageSize { get; set; }
    public int TotalSize { get; set; }
    public int Characters { get; set; }
    public int UnicodeScalars { get; set; }
}

