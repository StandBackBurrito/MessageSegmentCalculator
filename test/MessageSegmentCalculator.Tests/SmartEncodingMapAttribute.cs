using System;
using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;


namespace MessageSegmentCalculator.Tests
{
    public class SmartEncodingMapAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            return SmartEncoding.SmartEncodingMap.Select(item => new object[] { item.Key, item.Value });
        }
    }
}

