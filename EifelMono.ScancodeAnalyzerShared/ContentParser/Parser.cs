using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EifelMono.ScanCodeAnalyzer.ContentParser
{
    public class Parser
    {
        public Parser() : this("")
        {
        }


        public Parser(string scanCode)
        {
            OrgScanCode = scanCode;
            ScanCode = NormalizeEscape(scanCode.Trim());
            Reset();
        }

        #region Escape and more...

        public static string Escape(byte value)
        {
            return $@"\x{value:x2}";
        }
        public static string Escape(byte[] scanCode)
        {
            var result = new StringBuilder();
            foreach (var b in scanCode)
            {
                if (b < 0x20)
                    result.Append(Escape(b));
                else
                    result.Append(Convert.ToChar(b));
            }
            return result.ToString();
        }
        public static string Escape(char[] scanCode)
        {
            return Escape(scanCode.Select(c => (byte)c).ToArray<byte>());
        }
        public static string Escape(string scanCode)
        {
            return Escape(Encoding.ASCII.GetBytes(scanCode));
        }
        public static byte[] UpEscape(string scanCode)
        {
            List<byte> result = new List<byte>();
            while (scanCode.Length > 0)
            {
                var addByte = true;
                if (scanCode.StartsWith(@"\x") && scanCode.Length > 3)
                {
                    var text = scanCode.Substring(2, 2);
                    if (int.TryParse(text, System.Globalization.NumberStyles.HexNumber, null, out int number))
                    {
                        result.Add(Convert.ToByte(number));
                        scanCode = scanCode.Skip(4);
                        addByte = false;
                    }
                }
                if (addByte)
                {
                    result.Add(Convert.ToByte(scanCode[0]));
                    scanCode = scanCode.Skip(1);
                }
            }
            return result.ToArray();
        }

        public static string NormalizeEscape(string scanCode)
        {
            return Escape(UpEscape(scanCode));
        }


        #endregion
        public string OrgScanCode { get; set; }
        public string ScanCode { get; set; }
        public virtual bool CanParse()
        {
            return !string.IsNullOrEmpty(ScanCode);
        }

        public virtual string ScanCodeWithoutFrame()
        {
            return ScanCode;
        }

        public ParserState State { get; set; } = ParserState.None;

        public string ErrorMessage { get; set; } = "";

        public void Reset()
        {
            foreach (var i in Identifiers)
                i.Reset();
            State = ParserState.None;
            ErrorMessage = "";
        }

        #region Identifiers
        protected List<Identifier> _Identifiers = null;
        public List<Identifier> Identifiers
        {
            get
            {
                if (_Identifiers == null)
                {
                    _Identifiers = new List<Identifier>();
                    var properties = this.GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .Where(x => x.PropertyType.IsSubclassOf(typeof(Identifier)));
                    foreach (var p in properties)
                    {
                        var identifier = (Identifier)(p.GetValue(this, null));
                        if (identifier != null)
                            if (!_Identifiers.Contains(identifier))
                                _Identifiers.Add(identifier);
                    }
                }
                return _Identifiers;
            }
        }

        public IEnumerable<int> IdentiersLength { get => Identifiers.Select(i => i.Id.Length).OrderByDescending(i => i).Distinct(); }
        public IEnumerable<Identifier> IdentifiersParsed { get => Identifiers.Where(i => i.TextState == IdentifierTextState.Parsed); }
        public IEnumerable<Identifier> IdentifiersConverted { get => Identifiers.Where(i => i.ValueState == IdentifierValueState.Converted); }
        public IEnumerable<Identifier> IdentifiersConvertError { get => Identifiers.Where(i => i.ValueState == IdentifierValueState.ConvertError); }

        #endregion

        public virtual void Parse()
        {
            Reset();

            string scanCode = ScanCodeWithoutFrame();
            while (scanCode.Length > 0)
            {
                bool found = false;
                foreach (var identierLength in IdentiersLength)
                    if (ParseIdentifier(identierLength))
                    {
                        found = true;
                        break;
                    }
                if (!found)
                {
                    State = ParserState.Error;
                    ErrorMessage = $"No implemented idenfifier found in {ScanCode} {scanCode}";
                    scanCode = "";
                    break;
                }
            }
            if (State != ParserState.Error)
                State = ParserState.Ok;

            bool ParseIdentifier(int identifiertLen)
            {
                var id = scanCode.Take(identifiertLen);
                var identifier = Identifiers.Where(i => i.Id.StartsWith(id)).FirstOrDefault();
                if (identifier != null)
                {
                    scanCode = scanCode.Skip(identifiertLen);
                    identifier.Text = scanCode.Take(identifier.Length, identifier.Stop);
                    identifier.TextState = IdentifierTextState.Parsed;
                    var (ok, value) = identifier.Converter.TextToValue(identifier.Text);
                    if (ok)
                        identifier.Value = value;
                    identifier.ValueState = ok ? IdentifierValueState.Converted : IdentifierValueState.ConvertError;
                    scanCode = scanCode.Skip(identifier.Length, identifier.Stop);
                    return true;
                }
                return false;
            }
        }
    }
}
