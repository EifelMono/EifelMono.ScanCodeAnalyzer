using System;
using System.Collections.Generic;
using System.Text;

namespace TestSamples
{
    public static class GS1Samples
    {
        public readonly TestSample TestSampleGS1 = new TestSample
        {
            ScanCode = @"0104150123456782101A234B5\x1d17151231211234567890123456",
	    ScanCodeType = ScanCodeType.GS1,

            ProductNumber = "04150123456782",
            ExpiryDate = new DateTime(2015, 12, 31),
            SerialNumber = "1234567890123456",
            BatchNumber = "1A234B5"
        };

        public readonly TestSample TestSampleGs1UpperCase = new TestSample
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
