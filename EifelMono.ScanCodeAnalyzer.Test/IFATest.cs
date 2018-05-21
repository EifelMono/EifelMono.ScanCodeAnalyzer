using EifelMono.ScanCodeAnalyzer.ContentParser;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EifelMono.ScanCodeAnalyzer.Test
{
    public class IFATest
    {
        public static string IFA_1 = @"[)>\x1E06\x1D9N111234567842\x1D1T1A234B5\x1DD151231\x1DS1234567890123456\x1E\x04";
        /*
        [)>\x1E06\x1D9N111234567842\x1D1T1A234B5\x1DD151231\x1DS1234567890123456\x1E\x04
        [)>\x1E06\x1D
        9N
            111234567842\x1D
        1T
            1A234B5\x1D
        D
            151231\x1D
        S
            1234567890123456\x1E
        \x04
        */
         [Fact]
        public void Core()
        {
            try
            {
                var ifa = new IFAParser(IFA_1);
                Assert.Equal(ParserState.Ok, ifa.State);
                Assert.Equal("111234567842", ifa.ProductNumber.Value);
                Assert.Equal("1A234B5", ifa.BatchNumber.Value);

                Assert.Equal(31, ifa.ExpiryDate.Value.Day);
                Assert.Equal(12, ifa.ExpiryDate.Value.Month);
                Assert.Equal(2015, ifa.ExpiryDate.Value.Year);
                Assert.Equal("1234567890123456", ifa.SerialNumber.Value);
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.ToString());
            }
        }
    }
}
