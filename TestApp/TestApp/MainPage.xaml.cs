using EifelMono.ScanCodeAnalyzer;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestApp {
    public partial class MainPage : ContentPage {
	public class TVO {
	    public ScanCodeType Type { get; set; }
	    public string Value { get; set; }
	}
	public class MvvmData : MvvmClass {

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
	}

	public MvvmData Data { get; set; } = new MvvmData();

	public MainPage()
	{
	    InitializeComponent();
	    BindingContext = this;
	}

	private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
	    var o = ((sender as ListView).SelectedItem) as TVO;
	    Data.ScanCodeValues = ScanCode.Analyze(o.Value);
	    Data.SerialNumber.Value = Data.ScanCodeValues.SerialNumber;
	    OnPropertyChanged("");
	}
    }

    public class MvvmClass : INotifyPropertyChanged {
	public event PropertyChangedEventHandler PropertyChanged;
	protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
	{
	    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
    }

    public class MvvmProperty {
	public static MvvmProperty<T> Create<T>([CallerMemberName]string propertyName = "")
	{
	    return new MvvmProperty<T>() { PropertyName = propertyName };
	}
    }
    public class MvvmProperty<T> : INotifyPropertyChanged {

	public T Value { get; set; }
	public string PropertyName { get; set; }

	public event PropertyChangedEventHandler PropertyChanged;

	protected virtual void OnPropertyChanged(string propertyName = null)
	{
	    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName ?? PropertyName));
	}
    }
}

