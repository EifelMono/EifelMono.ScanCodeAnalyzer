using EifelMono.ScanCodeAnalyzer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestApp {
    public partial class MainPage : ContentPage {
	public class ModelView {
	    private List<object> scanCodes = new List<object>() {
		 new {Type= ScanCodeType.GS1, Value= @"0104150123456782101A234B5\x1D17151231211234567890123456" },
		 new {Type= ScanCodeType.IFA, Value= @"[)>\x1E06\x1D9N111234567842\x1D1T1A234B5\x1DD151231\x1DS1234567890123456\x1E\x04" },
		  new {Type= ScanCodeType.GS1, Value= @"0104150123456782101A234B5\x1D17151231211234567890123456" },
		 new {Type= ScanCodeType.IFA, Value= @"[)>\x1E06\x1D9N111234567842\x1D1T1A234B5\x1DD151231\x1DS1234567890123456\x1E\x04" },
		  new {Type= ScanCodeType.GS1, Value= @"0104150123456782101A234B5\x1D17151231211234567890123456" },
		 new {Type= ScanCodeType.IFA, Value= @"[)>\x1E06\x1D9N111234567842\x1D1T1A234B5\x1DD151231\x1DS1234567890123456\x1E\x04" }
	    };

	    public List<object> ScanCodes { get => scanCodes; set => scanCodes = value; }
	}
	public MainPage()
	{
	    InitializeComponent();
	    BindingContext = new ModelView();
	}
    }


}
