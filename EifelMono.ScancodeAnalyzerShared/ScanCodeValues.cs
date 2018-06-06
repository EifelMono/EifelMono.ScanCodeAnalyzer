using System;
using System.Collections.Generic;
using System.Text;

namespace EifelMono.ScanCodeAnalyzer
{
    public class ScanCodeValues
    {
        public string ScanCode { get; set; } = "";
        public ScanCodeType ScanCodeType { get; set; } = ScanCodeType.NONE;
        public string ProductCode { get; set; } = "";
        public ProductCodeType ProductCodeType { get; set; } = ProductCodeType.NONE;
        public ProductCodeDetails ProductCodeDetails { get; set; } = new ProductCodeDetails();

	public DateTime ExpiryDate { get; set; } = DateTime.MinValue;

	public string BatchNumber { get; set; } = "";

	public string SerialNumber { get; set; } = "";
    }
}
