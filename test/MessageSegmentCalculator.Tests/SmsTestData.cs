using System;


namespace MessageSegmentCalculator.Tests
{
    public static class SmsTestData
    {
        public static List<TestDataItem> Data => new()
    {
        new TestDataItem
        {
            TestDescription = "GSM7 in one segment",
            Body = "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890",
            Encoding = "GSM7",
            Segments = 1,
            MessageSize = 1120,
            TotalSize = 1120,
            Characters = 160,
            UnicodeScalars = 160,
        },
        new TestDataItem
        {
            TestDescription = "GSM7 in two Segments",
            Body = "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901",
            Encoding = "GSM7",
            Segments = 2,
            MessageSize = 1127,
            TotalSize = 1223,
            Characters = 161,
            UnicodeScalars = 161,
        },
        new TestDataItem
        {
            TestDescription = "GSM7 in three Segments",
            Body = "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567",
            Encoding = "GSM7",
            Segments = 3,
            MessageSize = 2149,
            TotalSize = 2293,
            Characters = 307,
            UnicodeScalars = 307,
        },
        new TestDataItem
        {
            TestDescription = "UCS2 message in one segment",
            Body = "😜23456789012345678901234567890123456789012345678901234567890123456789",
            Encoding = "UCS2",
            Segments = 1,
            MessageSize = 1120,
            TotalSize = 1120,
            Characters = 69,
            UnicodeScalars = 69,
        },
        new TestDataItem
        {
            TestDescription = "UCS2 message in two Segments",
            Body = "😜234567890123456789012345678901234567890123456789012345678901234567890",
            Encoding = "UCS2",
            Segments = 2,
            MessageSize = 1136,
            TotalSize = 1232,
            Characters = 70,
            UnicodeScalars = 70,
        },
        new TestDataItem
        {
            TestDescription = "UCS2 message in three Segments",
            Body = "😜2345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234",
            Encoding = "UCS2",
            Segments = 3,
            MessageSize = 2160,
            TotalSize = 2304,
            Characters = 134,
            UnicodeScalars = 134,
        },
        new TestDataItem
        {
            TestDescription = "UCS2 with two bytes extended Characters in one Segments boundary",
            Body = "🇮🇹234567890123456789012345678901234567890123456789012345678901234567",
            Encoding = "UCS2",
            Segments = 1,
            MessageSize = 1120,
            TotalSize = 1120,
            Characters = 67,
            UnicodeScalars = 68,
        },
        new TestDataItem
        {
            TestDescription = "UCS2 with extended Characters in two Segments boundary",
            Body = "🇮🇹2345678901234567890123456789012345678901234567890123456789012345678",
            Encoding = "UCS2",
            Segments = 2,
            MessageSize = 1136,
            TotalSize = 1232,
            Characters = 68,
            UnicodeScalars = 69,
        },
        new TestDataItem
        {
            TestDescription = "UCS2 with four bytes extended Characters in one Segments boundary",
            Body = "🏳️‍🌈2345678901234567890123456789012345678901234567890123456789012345",
            Encoding = "UCS2",
            Segments = 1,
            MessageSize = 1120,
            TotalSize = 1120,
            Characters = 65,
            UnicodeScalars = 68,
        },
        new TestDataItem
        {
            TestDescription = "UCS2 with four bytes extended Characters in two Segments boundary",
            Body = "🏳️‍🌈23456789012345678901234567890123456789012345678901234567890123456",
            Encoding = "UCS2",
            Segments = 2,
            MessageSize = 1136,
            TotalSize = 1232,
            Characters = 66,
            UnicodeScalars = 69,
        },
    };
    }
}

