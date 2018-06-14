using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Data;

namespace EifelMono.ScanCodeAnalyzer.ContentParser
{
    public class IdentifierDateConverter : IdentifierTextConverter
    {
        public string Format { get; set; }
        public bool DayCanBeNull { get; set; }
        public IdentifierDateConverter(string format = "yyMMdd", bool dayCanBeNull = true)
        {
            Format = format;
            DayCanBeNull = dayCanBeNull;
        }
        public override (bool ok, object value) TextToValue(string text)
        {
            if (string.IsNullOrEmpty(Format))
                return (false, text);
            if (string.IsNullOrEmpty(text))
                return (false, text);
            try
            {
                var useText = text;
                var dayIsNull = false;
                if (DayCanBeNull)
                {
                    var check = CheckNullDay(text);
                    dayIsNull = check.DayIsNull;
                    useText = check.UseText;
                }
                var date = DateTime.ParseExact(useText, Format, CultureInfo.InvariantCulture);
                if (dayIsNull)
                    date = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
                return (true, date);
            }
            catch
            {
                return (false, text);
            }
        }

        (bool DayIsNull, string UseText) CheckNullDay(string text)
        {
            var useText = "";
            int? d1 = null;
            int? d2 = null;
            char d1c = '\x00';
            char d2c = '\x00';

            char vc;
            char fc;
            for (int i = 0; i < text.Length; i++)
            {
                vc = text[i];
                if (i < Format.Length)
                {
                    fc = Format[i];
                    if (fc == 'd')
                    {
                        if (d1 == null)
                        {
                            d1 = i;
                            d1c = vc;
                        }
                        else
                            if (d2 == null)
                            {
                                d2 = i;
                                d2c = vc;
                            }
                    }
                }
                useText += vc;
            }
            if (d1 != null && d2 != null && d1c == '0' && d2c == '0')
            {
                char[] array = useText.ToCharArray();
                array[(int)d2] = '1';
                useText = new string(array);
                return (true, useText);
            }
            return (false, useText);
        }
    }
}
