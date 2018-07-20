using System;
using System.Collections.Generic;
using System.Text;

namespace EifelMono.ScanCodeAnalyzer.ContentParser
{
    public class IdentifierTextConverter
    {
        public virtual (bool ok, object value) TextToValue(string text)
        {
            return ( true, text);
        }
    }
}
