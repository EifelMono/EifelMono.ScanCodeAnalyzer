using System;
using System.Linq;
using System.Text;
using EifelMono.ScanCodeAnalyzer.ContentParser;

namespace EifelMono.ScanCodeAnalyzer {
    public static class ScanCode {

	#region Analyze
	public static ScanCodeValues Analyze(string scanCode)
	{
	    var analyze = new ScanCodeValues {
		ScanCode = scanCode
	    };

	    if (!AnalyzeGS1(analyze))
		if (!AnalyzeIFA(analyze)) {

		};
	    return analyze;
	}

	private static bool AnalyzeGS1(ScanCodeValues analyze)
	{
	    var parser = new GS1Parser(analyze.ScanCode);
	    if (parser.CanParse()) {
		parser.Parse();
		analyze.ProductCode = string.IsNullOrEmpty(parser.ProductNumber1.Value)? parser.ProductNumber2.Value: parser.ProductNumber1.Value;
		analyze.ExpiryDate = parser.ExpiryDate.Value;
		analyze.BatchNumber = parser.BatchNumber.Value;
		analyze.SerialNumber = parser.SerialNumber.Value;
		return true;
	    }
	    return false;
	}
	private static bool AnalyzeIFA(ScanCodeValues analyze)
	{
	    var parser = new IFAParser(analyze.ScanCode);
	    if (parser.CanParse()) {
		parser.Parse();
		analyze.ProductCode = parser.ProductNumber.Value;
		analyze.ExpiryDate = parser.ExpiryDate.Value;
		analyze.BatchNumber = parser.BatchNumber.Value;
		analyze.SerialNumber = parser.SerialNumber.Value;
		return true;
	    }
	    return false;
	}
	#endregion
    }
}
