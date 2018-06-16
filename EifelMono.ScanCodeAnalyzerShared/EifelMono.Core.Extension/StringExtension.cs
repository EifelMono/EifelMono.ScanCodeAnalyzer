using System;
using System.Collections.Generic;
using System.Text;

namespace EifelMono.Core.Extension
{
    public static class StringExtension
    {
        public static string IfEndsWithRemove(this string pipe, string value)
        {
            pipe = pipe.NoNullString();
            return pipe.EndsWith(value) ? pipe.Substring(0, pipe.Length - value.Length) : value;
        }
        public static string NoNullString(this string pipe) => pipe ?? "";

    }
}
