using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EifelMono.ScanCodeAnalyzer.ContentParser
{
    public class GS1Parser : Parser
    {
        public GS1Parser() : this("")
        {
        }
        public GS1Parser(string scanCode) : base(scanCode)
        {
        }

        #region Characters
        // Group Group-Separator
        // 1d == 29
        public static string GroupSeperator = @"\x1D";
        // Symbolic Identifier
        public static string Start = "]d2";

        #endregion
        public override bool CanParse()
        {
            if (!base.CanParse())
                return false;
            if (ScanCode.StartsWith(Start))
                return true;
            if (ScanCode.StartsWith(AI01.Id) && ScanCode.Length >= 16)
                return true;
            return false;
        }

        public override string ScanCodeWithoutFrame()
        {
            var scanCode = base.ScanCodeWithoutFrame();
            if (scanCode.StartsWith(Start))
                scanCode = scanCode.Skip(Start.Length);
            return scanCode;
        }

        #region Identifiers
        public Identifier<string> AI01 { get; private set; } = new Identifier<string>("ProductNumber1", "01", 14);
        public Identifier<string> ProductNumber1 { get => AI01; }
        public Identifier<string> AI02 { get; private set; } = new Identifier<string>("ProductNumber2", "02", 14);
        public Identifier<string> ProductNumber2 { get => AI02; }

        public Identifier<string> AI10 { get; private set; } = new Identifier<string>("BatchNumber", "10", 20, GroupSeperator);
        public Identifier<string> BatchNumber { get => AI10; }

        public Identifier<DateTime> AI17 { get; private set; } = new Identifier<DateTime>("ExpiryDate", "17", 6, new IdentifierDateConverter("yyMMdd"));
        public Identifier<DateTime> ExpiryDate { get => AI17; }

        public Identifier<string> AI21 { get; private set; } = new Identifier<string>("SerialNumber", "21", 20, GroupSeperator);
        public Identifier<string> SerialNumber { get => AI21; }


        #endregion

        public override void Parse()
        {
            base.Parse();
        }
    }
}
