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
            ScanCode = scanCode;
            Reset();
            if (CanParse())
                Parse();
        }

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
                        .Where(ø => ø.PropertyType.IsSubclassOf(typeof(Identifier)));
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
                    var convert = identifier.Converter.TextToValue(identifier.Text);
                    identifier.Value = convert.Value;
                    identifier.ValueState = convert.Ok ? IdentifierValueState.Converted : IdentifierValueState.ConvertError;
                    scanCode = scanCode.Skip(identifier.Length, identifier.Stop);
                    return true;
                }
                return false;
            }
        }
    }
}
