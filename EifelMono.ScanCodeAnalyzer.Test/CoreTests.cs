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
            Assert.Equal("abc", ScanCode.Escape(new byte[] { ToByte('a'), ToByte('b'), ToByte('c') }));
            Assert.Equal(@"\x01", ScanCode.Escape(new byte[] { 1 }));
            Assert.Equal(@"\x07", ScanCode.Escape(new byte[] { 7 }));
            Assert.Equal(@"\x07\x01", ScanCode.Escape(new byte[] { 7, 1 }));

            Assert.Equal(@"abc\x01def", ScanCode.Escape(new char[] { 'a', 'b', 'c', '\x01', 'd', 'e', 'f' }));
            Assert.Equal(@"abc\x01def", ScanCode.Escape("abc"+ ToChar(1) + "def"));
        }
    }
}
