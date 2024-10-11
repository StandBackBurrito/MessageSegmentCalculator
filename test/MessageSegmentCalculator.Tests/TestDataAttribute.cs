using System;
using System.Reflection;
using Xunit.Sdk;


namespace MessageSegmentCalculator.Tests
{
    public class TestDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            return SmsTestData.Data.Select(item => new object[] { item });
        }
    }
}

