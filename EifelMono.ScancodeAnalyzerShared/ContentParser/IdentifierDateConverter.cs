using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EifelMono.ScanCodeAnalyzer.ContentParser
{
    public class IdentifierDateConverter : IdentifierTextConverter
    {
        public string Format { get; set; }
        public IdentifierDateConverter(string format= "yyMMdd")
        {
            Format = format;
        }
        public override (bool Ok, object Value) TextToValue(string text)
        {
            if (string.IsNullOrEmpty(Format))
                return (false, text);
            if (string.IsNullOrEmpty(text))
                return (false, text);
            try
            {
                return (true, DateTime.ParseExact(text, Format, CultureInfo.InvariantCulture));
            }
            catch
            {
                return (false, text);
            }
        }
    }
}
