using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EifelMono.ScanCodeAnalyzer.ContentParser
{
    public class GS1Parser : Parser
    {
        #region Constructors
        public GS1Parser() : this("", false)
        {
        }
        public GS1Parser(string scanCode, bool parse= true) : base(scanCode, parse)
        {
        }
        #endregion

        #region Control
        public static string GroupSeperator = Escape(0x1d);
        public static string FrameStart = "]d2";
        public override bool CanParse()
        {
            if (!base.CanParse())
                return false;
            if (ScanCode.StartsWith(FrameStart))
                return true;
            return ScanCode.StartsWith(AI01.Id) && ScanCode.Length >= 16 ? true : false;
        }
        protected override string ScanCodeWithoutFrame()
        {
            var scanCode = base.ScanCodeWithoutFrame();
            if (scanCode.StartsWith(FrameStart))
                scanCode = scanCode.Skip(FrameStart.Length);
            return scanCode;
        }

        public override string IdentifiertsToScanCode()
        {
            return FrameStart + base.IdentifiertsToScanCode();
        }
        #endregion

        #region Identifiers
        protected Identifier<string> AI01 { get; private set; } = new Identifier<string>("Product code", "01", 14);
        public Identifier<string> ProductCode{ get => AI01; }

        protected Identifier<string> AI10 { get; private set; } = new Identifier<string>("Batch number", "10", 20, GroupSeperator);
        public Identifier<string> BatchNumber { get => AI10; }

        protected Identifier<DateTime> AI17 { get; private set; } = new Identifier<DateTime>("Expiry Date", "17", 6, new IdentifierDateConverter("yyMMdd"));
        public Identifier<DateTime> ExpiryDate { get => AI17; }

        protected Identifier<string> AI21 { get; private set; } = new Identifier<string>("Serial number", "21", 20, GroupSeperator);
        public Identifier<string> SerialNumber { get => AI21; }

        public Identifier<string> AI710 { get; private set; } = new Identifier<string>("Country Product code 1", "710", 20, GroupSeperator);
        public Identifier<string> AI711 { get; private set; } = new Identifier<string>("Country Product code 2", "711", 20, GroupSeperator);

        #endregion
    }
}
