using EifelMono.ScanCodeAnalyzer;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace TestSamples
{
    public class TestSample
    {
	public TestSample([CallerMemberName]string name = "")
	{
	    Name = name; 
	}
	public string Name { get; set; }
	// input
	public string ScanCode { get; set; }
        public ScanCodeType ScanCodeType { get; set; } = ScanCodeType.NONE;
    // output
        public string ProductNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string SerialNumber { get; set; }
        public string BatchNumber { get; set; }
    }
}
