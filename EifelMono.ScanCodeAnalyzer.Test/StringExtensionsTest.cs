using System;
using System.Collections.Generic;
using Xunit;
using EifelMono.ScanCodeAnalyzer.ContentParser;


namespace EifelMono.ScanCodeAnalyzer.Test
{
    public class StringExtensionsTest
    {
        [Fact]
        public void String()
        {
            string text = "1234567890";
            Assert.Equal("12", text.Take(2));
            Assert.Equal("34567890", text.Skip(2));
            Assert.Equal("1234", text.Take(4, "567"));
            Assert.Equal("1234", text.Take(5, "567"));
            Assert.Equal("1234", text.Take(6, "567"));
            Assert.Equal("1234", text.Take(7, "567"));
            Assert.Equal("1234", text.Take(8, "567"));
            Assert.Equal("1234", text.Take(9, "567"));
            Assert.Equal("1234", text.Take(10, "567"));
            Assert.Equal("567890", text.Skip(4, "567"));
            Assert.Equal("890", text.Skip(5, "567"));
            Assert.Equal("567890", text.Skip(4, "67"));
        }
    }
}
