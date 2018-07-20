using EifelMono.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EifelMono.ScanCodeAnalyzer.ContentParser
{
    public class IFAParser : Parser
    {
        #region Constructors
        public IFAParser() : this("", false)
        {
        }
        public IFAParser(string scanCode, bool parse = true) : base(scanCode, parse)
        {
        }
        #endregion

        #region Control
        public static string FieldSeperator = Escape(0x1d);
        public static string RecordSeperator = Escape(0x1e);
        public static string FrameStart = $"[)>{RecordSeperator}06{FieldSeperator}";
        public static string FrameEnd = RecordSeperator + Escape(0x04);

        public override bool CanParse()
        {
            if (!base.CanParse())
                return false;
            return ScanCode.StartsWith(FrameStart) && ScanCode.EndsWith(FrameEnd) ? true : false;
        }
        protected override string ScanCodeWithoutFrame()
        {
            var scanCode = base.ScanCodeWithoutFrame();
            if (ScanCode.StartsWith(FrameStart))
                scanCode = ScanCode.Skip(FrameStart.Length);
            if (scanCode.EndsWith(FrameEnd))
                scanCode = scanCode.Take(scanCode.Length - FrameEnd.Length);
            return scanCode;
        }

        public override string IdentifiertsToScanCode() =>
            FrameStart + base.IdentifiertsToScanCode().IfEndsWithRemoveIt(FieldSeperator) + FrameEnd;
        #endregion

        #region Identifiers
        protected Identifier<string> Id9N { get; private set; } = new Identifier<string>("Product Code", "9N", FieldSeperator);
        public Identifier<string> ProductCode { get => Id9N; }
        protected Identifier<string> IdS { get; private set; } = new Identifier<string>("Serial number", "S", FieldSeperator);
        public Identifier<string> SerialNumber { get => IdS; }
        protected Identifier<string> Id1T { get; private set; } = new Identifier<string>("Batch number", "1T", FieldSeperator);
        public Identifier<string> BatchNumber { get => Id1T; }
        protected Identifier<DateTime> IdD { get; private set; } = new Identifier<DateTime>("Expiry date", "D", FieldSeperator, new IdentifierDateConverter("yyMMdd"));
        public Identifier<DateTime> ExpiryDate { get => IdD; }
        protected Identifier<DateTime> Id16D { get; private set; } = new Identifier<DateTime>("ManufactureDate date", "16D", FieldSeperator, new IdentifierDateConverter("yyyyMMdd"));
        public Identifier<DateTime> ManufactureDate { get => Id16D; }
        protected MultiIdentifier<string> Id8P { get; private set; } = new MultiIdentifier<string>("Country Specific Product Code's", "8P", 20, FieldSeperator);
        public MultiIdentifier<string> CountrySpecificProductCodes { get => Id8P; }

        #endregion
    }
}
