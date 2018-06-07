using System;
using System.Linq;
using System.Text;
using EifelMono.ScanCodeAnalyzer.ContentParser;

namespace EifelMono.ScanCodeAnalyzer {
    public static class ScanCode {
	#region Escape
	public static string Escape(byte[] scanCode)
	{
	    var result = new StringBuilder();
	    foreach (var b in scanCode) {
		if (b < 0x20)
		    result.Append($@"\x{b:X2}");
		else
		    result.Append(Convert.ToChar(b));
	    }
	    return result.ToString();
	}

	public static string Escape(char[] scanCode)
	{
	    return Escape(scanCode.Select(c => (byte)c).ToArray<byte>());
	}
	public static string Escape(string scanCode)
	{
	    return Escape(Encoding.ASCII.GetBytes(scanCode));
	}
	#endregion

	#region UnEscape
	public static byte[] UnEscapeAsByte(string scanCode)
	{
	    throw new NotImplementedException();
	}

	public static char[] UnEscapeAsChar(string scanCode)
	{
	    throw new NotImplementedException();
	}

	public static string UnEscapeAsString(string scanCode)
	{
	    throw new NotImplementedException();
	}
	#endregion

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
