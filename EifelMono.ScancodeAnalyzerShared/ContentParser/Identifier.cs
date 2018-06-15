using System;
using System.Collections.Generic;
using System.Text;

namespace EifelMono.ScanCodeAnalyzer.ContentParser
{
    public class Identifier
    {
        public string Name { get; set; } = "";
        public string Id { get; set; }
        public int Length { get; set; }

        public string Stop { get; set; } = "";

        public bool Active { get; set; } = true;

        public Identifier()
        {
        }

        public Identifier(string name, string id, int length, string stop = "", IdentifierTextConverter converter = null) : this()
        {
            Name = name;
            Id = id;
            Length = length;
            Stop = stop;
            Converter = converter ?? new IdentifierTextConverter();
        }

        public Identifier(string name, string id, int length, IdentifierTextConverter converter) : this(name, id, length, "", converter)
        {
        }


        public IdentifierTextState TextState { get; set; } = IdentifierTextState.None;
        public string Text { get; set; }

        public IdentifierValueState ValueState { get; set; } = IdentifierValueState.None;

        public object Value { get; set; }

        public bool HasValue { get => ValueState == IdentifierValueState.Converted; }

        public virtual bool IsValueType()
        {
            return false;
        }

        public string ErrorMessage { get; set; } = "";

        public IdentifierTextConverter Converter { get; set; }

        public virtual void Reset()
        {
            Text = "";
            TextState = IdentifierTextState.None;
            Value = null;
            ValueState = IdentifierValueState.None;
            ErrorMessage = "";
        }
    }
    public class Identifier<T> : Identifier
    {
        public Identifier() : base()
        {
            Value = default;
            if (typeof(T) == typeof(string))
                base.Value = "";

        }

        public Identifier(string name, string id, int length, string stop = "", IdentifierTextConverter converter = null) : base(name, id, length, stop, converter)
        {
        }

        public Identifier(string name, string id, int length, IdentifierTextConverter converter) : this(name, id, length, "", converter)
        {
        }

        public new T Value { get => (T)base.Value; set => base.Value = value; }

        public override void Reset()
        {
            base.Reset();
            Value = default;
        }

        public override bool IsValueType()
        {
            return Value is T;
        }

        public override string ToString()
        {
            return $"{Name} {Text}";
        }

    }
}
