using EifelMono.ScanCodeAnalyzer.ContentParser;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EifelMono.ScanCodeAnalyzer.Test {
    public class IFATest {
	public static string IFA_1 = @"[)>\x1e06\x1d9N111234567842\x1d1T1A234B5\x1dD151231\x1DS1234567890123456\x1e\x04";
	public static string IFA_1UpperCase = @"[)>\x1E06\x1D9N111234567842\x1D1T1A234B5\x1DD151231\x1DS1234567890123456\x1E\x04";
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
	public static string IFA_2Fehler = @"[)>\x1e06\x1d9N110461290653\x1d1T171011\x1dD201031\x1dS67CBE405FBC5\x1e\x4";
	public static string IFA_2 = @"[)>\x1e06\x1d9N110461290653\x1d1T171011\x1dD201031\x1dS67CBE405FBC5\x1e\x04";

	[Fact]
	public void TestIFA_1()
	{
	    var ifa = new IFAParser(IFA_1);
	    Assert.Equal(ParserState.Ok, ifa.State);
	    Assert.Equal("111234567842", ifa.ProductCode.Value);
	    Assert.Equal("1A234B5", ifa.BatchNumber.Value);

	    Assert.Equal(31, ifa.ExpiryDate.Value.Day);
	    Assert.Equal(12, ifa.ExpiryDate.Value.Month);
	    Assert.Equal(2015, ifa.ExpiryDate.Value.Year);
	    Assert.Equal("1234567890123456", ifa.SerialNumber.Value);

	    ifa = new IFAParser(IFA_1UpperCase);
	    Assert.Equal(ParserState.Ok, ifa.State);
	    Assert.Equal("111234567842", ifa.ProductCode.Value);
	    Assert.Equal("1A234B5", ifa.BatchNumber.Value);

	    Assert.Equal(31, ifa.ExpiryDate.Value.Day);
	    Assert.Equal(12, ifa.ExpiryDate.Value.Month);
	    Assert.Equal(2015, ifa.ExpiryDate.Value.Year);
	    Assert.Equal("1234567890123456", ifa.SerialNumber.Value);
	}

	[Fact]
	public void TestIFA_2()
	{
	    var ifa = new IFAParser(IFA_2);
	    Assert.Equal(ParserState.Ok, ifa.State);
	    Assert.Equal("110461290653", ifa.ProductCode.Value);
	    Assert.Equal("171011", ifa.BatchNumber.Value);

	    Assert.Equal(31, ifa.ExpiryDate.Value.Day);
	    Assert.Equal(10, ifa.ExpiryDate.Value.Month);
	    Assert.Equal(2020, ifa.ExpiryDate.Value.Year);
	    Assert.Equal("67CBE405FBC5", ifa.SerialNumber.Value);
	}
	[Fact]
	public void TestIFA_2Fehler()
	{
	    string fixIFA_2 = IFA_2Fehler.Replace(@"\x4", @"\x04");
	    var ifa = new IFAParser(fixIFA_2);
	    Assert.Equal(ParserState.Ok, ifa.State);
	    Assert.Equal("110461290653", ifa.ProductCode.Value);
	    Assert.Equal("171011", ifa.BatchNumber.Value);

	    Assert.Equal(31, ifa.ExpiryDate.Value.Day);
	    Assert.Equal(10, ifa.ExpiryDate.Value.Month);
	    Assert.Equal(2020, ifa.ExpiryDate.Value.Year);
	    Assert.Equal("67CBE405FBC5", ifa.SerialNumber.Value);
	}
    }
}
