using EifelMono.ScanCodeAnalyzer;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestSamples
{
    public class IFASamples: CoreSamples
    {
	public static TestSample Sample1 { get; set; } = new TestSample {
	    ScanCode = @"[)>\x1e06\x1d9N111234567842\x1d1T1A234B5\x1dD151231\x1DS1234567890123456\x1e\x04",
	    ScanCodeType = ScanCodeType.IFA,

	    ProductNumber = "111234567842",
	    ExpiryDate = new DateTime(2015, 12, 31),
	    SerialNumber = "1234567890123456",
	    BatchNumber = "1A234B5"
	};

	public static TestSample Sample1UpperCase { get; set; } = new TestSample {
	    ScanCode = @"[)>\x1E06\x1D9N111234567842\x1D1T1A234B5\x1DD151231\x1DS1234567890123456\x1E\x04",
	    ScanCodeType = ScanCodeType.IFA,

	    ProductNumber = "111234567842",
	    ExpiryDate = new DateTime(2015, 12, 31),
	    SerialNumber = "1234567890123456",
	    BatchNumber = "1A234B5"
	};


	public static TestSample Sample2 { get; set; } = new TestSample {
	    ScanCode = @"[)>\x1E06\x1D9N110130041992\x1D1T171156\x1DD221130\x1DSF4E1F7429C4B\x1E\x04",
	    ScanCodeType = ScanCodeType.IFA,

	    ProductNumber = "",
	    ExpiryDate = new DateTime(2020, 10, 30),
	    SerialNumber = "",
	    BatchNumber = ""
	};

	public static TestSample Sample3 { get; set; } = new TestSample {
	    ScanCode = @"[)>\x1E06\x1D9N110258796689\x1D1T789\x1DD201031\x1DS2A3306FB57DD\x1E\x04",
	    ScanCodeType = ScanCodeType.IFA,

	    ProductNumber = "",
	    ExpiryDate = new DateTime(2020, 10, 30),
	    SerialNumber = "",
	    BatchNumber = ""
	};

	public static TestSample Sample4 { get; set; } = new TestSample {
	    ScanCode = @"[)>\x1e06\x1d9N110461290653\x1d1T171011\x1dD201031\x1dS67CBE405FBC5\x1e\x04",
	    ScanCodeType = ScanCodeType.IFA,

	    ProductNumber = "110461290653",
	    ExpiryDate = new DateTime(2020, 10, 30),
	    SerialNumber = "67CBE405FBC5",
	    BatchNumber = "171011"
	};


	public static TestSample Sample5 { get; set; } = new TestSample {
	    ScanCode = @"[)>\x1E06\x1D9N110210785465\x1D1T171122.1\x1DD221130\x1DSFC4248E48DFC\x1E\x04",
	    ScanCodeType = ScanCodeType.IFA,

	    ProductNumber = "",
	    ExpiryDate = new DateTime(2020, 10, 30),
	    SerialNumber = "",
	    BatchNumber = ""
	};

        public static TestSample Sample6DayIsNull { get; set; } = new TestSample
        {
            ScanCode = @"[)>\x1E06\x1D9N110210785465\x1D1T171122.1\x1DD221100\x1DSFC4248E48DFC\x1E\x04",
            ScanCodeType = ScanCodeType.IFA,

            ProductNumber = "",
            ExpiryDate = new DateTime(2020, 10, 30),
            SerialNumber = "",
            BatchNumber = ""
        };

        public static TestSample Sample7 { get; set; } = new TestSample
        {
            ScanCode = @"[)>\x1E06\x1D9N110210785465\x1D1T171122.1\x1DD221100\x1DSFC4248E48DFC\x1E\x04",
            ScanCodeType = ScanCodeType.IFA,

            ProductNumber = "",
            ExpiryDate = new DateTime(2020, 10, 30),
            SerialNumber = "",
            BatchNumber = ""
        };
    }
}
