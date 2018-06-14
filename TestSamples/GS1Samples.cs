using EifelMono.ScanCodeAnalyzer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TestSamples
{
    public class GS1Samples : CoreSamples
    {
	public static TestSample Sample1 { get; set; } = new TestSample
        {
            ScanCode = @"0104150123456782101A234B5\x1d17151231211234567890123456",
            ScanCodeType = ScanCodeType.GS1,

            ProductNumber = "04150123456782",
            ExpiryDate = new DateTime(2015, 12, 31),
            SerialNumber = "1234567890123456",
            BatchNumber = "1A234B5"
        };

        public static  TestSample Sample1UpperCase { get; set; } =
        new TestSample
        {
            ScanCode = @"0104150123456782101A234B5\x1D17151231211234567890123456",
            ScanCodeType = ScanCodeType.GS1,

            ProductNumber = "04150123456782",
            ExpiryDate = new DateTime(2015, 12, 31),
            SerialNumber = "1234567890123456",
            BatchNumber = "1A234B5"
        };
    }
}
