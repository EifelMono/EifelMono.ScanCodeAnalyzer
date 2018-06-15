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
                if (DateTime.TryParseExact(useText, Format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                {
                    if (dayIsNull)
                        date = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
                    return (true, date);
                }
                else
                    return (false, text);
            }
            catch
            {
                return (false, text);
            }
        }

        (bool DayIsNull, string UseText) CheckNullDay(string text)
        {
            var useText = "";
            var posdd = Format.IndexOf("dd", StringComparison.Ordinal);
            if (posdd>= 0)
            {
                useText = text;
                useText= useText.Remove(posdd, 2);
                useText= useText.Insert(posdd, "01");
            }
            return (false, useText);
        }
    }
}
