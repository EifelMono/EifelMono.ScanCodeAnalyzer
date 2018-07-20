using System;
using System.Collections.Generic;
using System.Text;

namespace EifelMono.ScanCodeAnalyzer
{
    public class ProductCodeDetails
    {
        public ProductCodeType CodeType = ProductCodeType.NONE;
        public Country Country { get; set; } = Country.none;
        public string CountryProductCode { get; set; } = "";

        public ProductCodeType CountryProductCodeCodeType { get; set; } = ProductCodeType.NONE;
    }
}
