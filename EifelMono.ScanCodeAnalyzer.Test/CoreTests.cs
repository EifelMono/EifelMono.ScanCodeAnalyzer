using EifelMono.ScanCodeAnalyzer.ContentParser;
using System;
using Xunit;

namespace EifelMono.ScanCodeAnalyzer.Test
{
    public class CoreTests
    {
        public byte ToByte(char c) => Convert.ToByte(c);
        public char ToChar(byte b) => Convert.ToChar(b);

        [Fact]
        public void Escaping()
        {
            Assert.Equal("abc", Parser.Escape(new byte[] { ToByte('a'), ToByte('b'), ToByte('c') }));
            Assert.Equal(@"\x01", Parser.Escape(new byte[] { 1 }));
            Assert.Equal(@"\x07", Parser.Escape(new byte[] { 7 }));
            Assert.Equal(@"\x07\x01", Parser.Escape(new byte[] { 7, 1 }));

            Assert.Equal(@"abc\x01def", Parser.Escape(new char[] { 'a', 'b', 'c', '\x01', 'd', 'e', 'f' }));
            Assert.Equal(@"abc\x01def", Parser.Escape("abc"+ ToChar(1) + "def"));
        }
    }
}
