using System;
using System.Collections.Generic;
using System.Text;

namespace EifelMono.ScanCodeAnalyzer.ContentParser {
    public static class StringExtensions {
	public static string Skip(this string value, int count, string tag = "")
	{
	    if (string.IsNullOrEmpty(value))
		return "";
	    tag = tag ?? "";
	    var index = value.IndexOf(tag);
	    if (tag != "" && index >= 0 && index < count) {
		return value.Substring(index + tag.Length);
	    }
	    if (count < value.Length)
		return value.Substring(count);
	    return "";
	}
	public static string Take(this string value, int count, string tag = "")
	{
	    if (string.IsNullOrEmpty(value))
		return "";
	    tag = tag ?? "";
	    var index = value.IndexOf(tag);
	    if (tag != "" && index >= 0 && index < count)
		return value.Substring(0, index);
	    if (count < value.Length)
		return value.Substring(0, count);
	    return value;
	}

    }
}
