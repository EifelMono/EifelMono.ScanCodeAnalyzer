using EifelMono.ScanCodeAnalyzer.ContentParser;
using System;
using Xunit;

namespace EifelMono.ScanCodeAnalyzer.Test {
    public class GS1Test {
	public static string GS1_1 = @"0104150123456782101A234B5\x1d17151231211234567890123456";
	/*
        GS1
            0104150123456782101A234B5\x1d17151231211234567890123456
        01
            04150123456782
        10
            1A234B5\x1d
        17
            151231
        21
            1234567890123456
        */
	public static string GS1_1UpperCase = @"0104150123456782101A234B5\x1D17151231211234567890123456";

	[Fact]
	public void TestGs1_1()
	{
	    var gs1 = new GS1Parser(GS1_1);
	    Assert.Equal(ParserState.Ok, gs1.State);
	    Assert.Equal("04150123456782", gs1.ProductCode.Value);
	    Assert.Equal("1A234B5", gs1.BatchNumber.Value);

	    Assert.Equal(31, gs1.ExpiryDate.Value.Day);
	    Assert.Equal(12, gs1.ExpiryDate.Value.Month);
	    Assert.Equal(2015, gs1.ExpiryDate.Value.Year);
	    Assert.Equal("1234567890123456", gs1.SerialNumber.Value);

	    gs1 = new GS1Parser(GS1_1UpperCase);
	    Assert.Equal(ParserState.Ok, gs1.State);
	    Assert.Equal("04150123456782", gs1.ProductCode.Value);
	    Assert.Equal("1A234B5", gs1.BatchNumber.Value);

	    Assert.Equal(31, gs1.ExpiryDate.Value.Day);
	    Assert.Equal(12, gs1.ExpiryDate.Value.Month);
	    Assert.Equal(2015, gs1.ExpiryDate.Value.Year);
	    Assert.Equal("1234567890123456", gs1.SerialNumber.Value);
	}
    }
}
