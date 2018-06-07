using EifelMono.ScanCodeAnalyzer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestApp {
    public partial class MainPage : ContentPage {
	public class TVO {
	    public ScanCodeType Type { get; set; }
	    public string Value { get; set; }
	}
	public class MvvmData : MvvmObject {

	    private List<object> scanCodes = new List<object>() {
		 new TVO() {Type= ScanCodeType.GS1, Value= @"0104150123456782101A234B5\x1D17151231211234567890123456" },
		 new TVO() {Type= ScanCodeType.IFA, Value= @"[)>\x1E06\x1D9N111234567842\x1D1T1A234B5\x1DD151231\x1DS1234567890123456\x1E\x04" },
		 new TVO() {Type= ScanCodeType.GS1, Value= @"0104150123456782101A234B5\x1D17151231211234567890123456" },
		 new TVO() {Type= ScanCodeType.IFA, Value= @"[)>\x1E06\x1D9N111234567842\x1D1T1A234B5\x1DD151231\x1DS1234567890123456\x1E\x04" },
		 new TVO() {Type= ScanCodeType.GS1, Value= @"0104150123456782101A234B5\x1D17151231211234567890123456" },
		 new TVO() {Type= ScanCodeType.IFA, Value= @"[)>\x1E06\x1D9N111234567842\x1D1T1A234B5\x1DD151231\x1DS1234567890123456\x1E\x04" }
	    };
	    public List<object> ScanCodes { get => scanCodes; set => scanCodes = value; }

	    public ScanCodeValues ScanCodeValues { get; set; } = new ScanCodeValues();

	    public MvvmProperty<string> SerialNumber { get; set; } = MvvmProperty.Create<string>();
	    public MvvmProperty<DateTime> TimeStamp { get; set; } = MvvmProperty.Create<DateTime>();
	}

	public MvvmData Data { get; set; } = new MvvmData();

	public MainPage()
	{
	    InitializeComponent();
	    BindingContext = this;
	    Task.Run(async () => {
		while (true) {
		    await Task.Delay(TimeSpan.FromSeconds(1));
		    Data.TimeStamp.Value = DateTime.Now;
		}
	    });
	}

	private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
	    var o = ((sender as ListView).SelectedItem) as TVO;
	    Data.ScanCodeValues = ScanCode.Analyze(o.Value);
	    Data.SerialNumber.Value = Data.ScanCodeValues.SerialNumber;
	    // Data.Refresh();
	}
    }


}

