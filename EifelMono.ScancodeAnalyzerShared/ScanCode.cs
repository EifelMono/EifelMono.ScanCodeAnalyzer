using System;
using System.Linq;
using System.Text;
using EifelMono.ScanCodeAnalyzer.ContentParser;

namespace EifelMono.ScanCodeAnalyzer
{
    public static class ScanCode
    {

        #region Analyze
        public static ScanCodeValues Analyze(string scanCode)
        {
            var analyze = new ScanCodeValues
            {
                ScanCode = scanCode
            };

            if (!AnalyzeGS1(analyze))
                if (!AnalyzeIFA(analyze))
                {

                };
            return analyze;
        }

        private static bool AnalyzeGS1(ScanCodeValues analyze)
        {
            var parser = new GS1Parser(analyze.ScanCode, false);
            if (parser.CanParse())
            {
                parser.Parse();
                analyze.ProductCode = parser.ProductCode.Value;
                analyze.ExpiryDate = parser.ExpiryDate.Value;
                analyze.BatchNumber = parser.BatchNumber.Value;
                analyze.SerialNumber = parser.SerialNumber.Value;
                analyze.Parser = parser;
                return true;
            }
            return false;
        }
        private static bool AnalyzeIFA(ScanCodeValues analyze)
        {
            var parser = new IFAParser(analyze.ScanCode, false);
            if (parser.CanParse())
            {
                parser.Parse();
                analyze.ProductCode = parser.ProductCode.Value;
                analyze.ExpiryDate = parser.ExpiryDate.Value;
                analyze.BatchNumber = parser.BatchNumber.Value;
                analyze.SerialNumber = parser.SerialNumber.Value;
                analyze.Parser = parser;
                return true;
            }
            return false;
        }
        #endregion
    }
}
