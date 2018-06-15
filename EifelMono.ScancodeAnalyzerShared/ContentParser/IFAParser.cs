using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EifelMono.ScanCodeAnalyzer.ContentParser {
    public class IFAParser : Parser {
	public IFAParser() : this("")
	{
	}
	public IFAParser(string scanCode) : base(scanCode)
	{
	}

	#region Characters
	public static string FieldSeperator = Escape(0x1d);
	public static string RecordSeperator = Escape(0x1e);
	// Symbolic Identifier

	public static string Start = $"[)>{RecordSeperator}06{FieldSeperator}";
	public static string End = Escape(4);
	#endregion

	public override bool CanParse()
	{
#pragma warning disable IDE0046 // Convert to conditional expression
	    if (!base.CanParse())
		return false;
	    return ScanCode.StartsWith(Start) && ScanCode.EndsWith(End) ? true : false;
#pragma warning restore IDE0046 // Convert to conditional expression
	}

	public override string ScanCodeWithoutFrame()
	{
	    var scanCode = base.ScanCodeWithoutFrame();
	    if (ScanCode.StartsWith(Start))
		scanCode = ScanCode.Skip(Start.Length);
	    if (scanCode.EndsWith(End))
		scanCode = scanCode.Take(scanCode.Length - End.Length);
	    return scanCode;
	}

	#region Identifiers

	public Identifier<string> DI9N { get; private set; } = new Identifier<string>("Product Code", "9N", 20, FieldSeperator);
	public Identifier<string> ProductCode { get => DI9N; }
	public Identifier<string> DIS { get; private set; } = new Identifier<string>("Serial number", "S", 20, RecordSeperator);
	public Identifier<string> SerialNumber { get => DIS; }
	public Identifier<string> DI1T { get; private set; } = new Identifier<string>("Batch number", "1T", 20, FieldSeperator);
	public Identifier<string> BatchNumber { get => DI1T; }
	public Identifier<DateTime> DID { get; private set; } = new Identifier<DateTime>("Expiry date", "D", 20, FieldSeperator, new IdentifierDateConverter("yyMMdd"));
	public Identifier<DateTime> ExpiryDate { get => DID; }

	public IdentifierList<string> DI8P { get; private set; } = new IdentifierList<string>("Country Specific Product Code's", "8P", 20, FieldSeperator);
	public IdentifierList<string> CountrySpecificProductCodes { get => DI8P; }

	#endregion

	public override void Parse()
	{
	    base.Parse();
	}
    }
}
