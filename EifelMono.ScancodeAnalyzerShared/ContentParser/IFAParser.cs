using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EifelMono.ScanCodeAnalyzer.ContentParser
{
    public class IFAParser : Parser
    {
        public IFAParser() : this("")
        {
        }
        public IFAParser(string scanCode) : base(scanCode)
        {
        }

        #region Characters
        public static string FieldSeperator = @"\x1D";
        public static string RecordSeperator = @"\x1E";
        // Symbolic Identifier

        public static string Start = $"[)>{RecordSeperator}06{FieldSeperator}";
        public static string End = @"\x04";
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

        public Identifier<string> DI9N { get; private set; } = new Identifier<string>("ProductNumber", "9N", 20, FieldSeperator);
        public Identifier<string> ProductNumber { get => DI9N; }
        public Identifier<string> DI1T { get; private set; } = new Identifier<string>("BatchNumber", "1T", 20, FieldSeperator);
        public Identifier<string> BatchNumber { get => DI1T; }
        public Identifier<DateTime> DID { get; private set; } = new Identifier<DateTime>("ExpiryDate", "D", 20, FieldSeperator, new IdentifierDateConverter("yyMMdd"));
        public Identifier<DateTime> ExpiryDate { get => DID; }

        public Identifier<string> DIS { get; private set; } = new Identifier<string>("SerialNumber", "S", 20, RecordSeperator);
        public Identifier<string> SerialNumber { get => DIS; }
        #endregion

        public override void Parse()
        {
            base.Parse();
        }
    }
}
