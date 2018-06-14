using System;
using System.Collections.Generic;
using System.Text;

namespace EifelMono.ScancodeAnalyzerSamplesShared
{
    public class TestSample
    {
	// input
	public string ScanCode { get; set; }
        public ScanCodeType ScanCodeType { get; set; } = ScanCodeType.None;
        // output
        public string ProductCode { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string SerialNumber { get; set; }
        public string BatchNumber { get; set; }
    }
}
