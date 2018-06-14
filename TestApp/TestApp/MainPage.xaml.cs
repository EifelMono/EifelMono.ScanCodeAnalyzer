using EifelMono.QuickButDirty.Binding;
using EifelMono.ScanCodeAnalyzer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using TestSamples;

namespace TestApp {
    public partial class MainPage : ContentPage {

	public class BindingData : BindingClass {
	    private BindingCollection<TestSample> _ScanCodes = null;
	    public BindingCollection<TestSample> ScanCodes {
		get {
		    if (_ScanCodes == null) {
			_ScanCodes = new BindingCollection<TestSample>();
			_ScanCodes.AddRange(new GS1Samples().Samples);
			_ScanCodes.AddRange(new IFASamples().Samples);
		    }
		    return _ScanCodes;
		}
	    }
	    public ScanCodeValues ScanCodeValues { get; set; } = new ScanCodeValues();

	    public BindingProperty<string> ScanCode { get; set; } = new BindingProperty<string>().Default("");

	    ICommand _CommandScanCodeAnaylze = null;

	    public ICommand CommandScanCodeAnaylze {
		get {
		    return _CommandScanCodeAnaylze ?? (_CommandScanCodeAnaylze = new Command(() => {
			ScanCodeValues = EifelMono.ScanCodeAnalyzer.ScanCode.Analyze(ScanCode.Value.Trim());
			// No Binding object!
			RefreshAll();
		    }));
		}
	    }
	}
	public BindingData Data { get; set; }
	public MainPage()
	{
	    InitializeComponent();
	    BindingContext = Data = new BindingData();
	}

	private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
	    var o = ((sender as ListView).SelectedItem) as TestSample;
	    Data.ScanCodeValues = ScanCode.Analyze(o.ScanCode);
	    // No Binding object!
	    Data.RefreshAll();
	}
    }
}

