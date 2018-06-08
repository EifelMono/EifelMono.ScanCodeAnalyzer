using EifelMono.Mvvm;
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

namespace TestApp {
    public partial class MainPage : ContentPage {
	public class TVO : MvvmObject {
	    public TVO(ScanCodeType type, string code)
	    {
		Type.Value = type;
		Code.Value = code;
	    }
	    public MvvmProperty<ScanCodeType> Type { get; set; } = new MvvmProperty<ScanCodeType>();
	    public MvvmProperty<string> Code { get; set; } = new MvvmProperty<string>();
	}

	public class CounterClass : MvvmObject {
	    public MvvmProperty<int> TimerCount { get; set; }
		= MvvmProperty.Create<int>()
		    .DoOnChanged((o, n) => {
			Debug.WriteLine($"old={o} new={n}");
		    });
	}

	public class MvvmData : MvvmObject {

	    public MvvmData()
	    {
		var ChangeValue = 0;
		CommandChangeValueInList = new Command(() => {
		    ChangeValue++;
		    ScanCodes[2].Code.Value += ChangeValue.ToString();
		});
		CommandAddValueToList = new Command(() => {
		    ScanCodes.Add(new TVO(ScanCodeType.NONE, DateTime.Now.ToString()));
		});
	    }

	    private ObservableCollection<TVO> scanCodes = new ObservableCollection<TVO>() {
		 new TVO(ScanCodeType.GS1, @"0104150123456782101A234B5\x1D17151231211234567890123456a"),
		 new TVO(ScanCodeType.IFA ,@"[)>\x1E06\x1D9N111234567842\x1D1T1A234B5\x1DD151231\x1DS1234567890123456\x1E\x04" ),
		 new TVO(ScanCodeType.GS1 ,@"0114150123456782101A234B5\x1D17151231211234567890123456b" ),
		 new TVO(ScanCodeType.IFA ,@"[)>\x1E06\x1D9N111234567842\x1D1T1A234B5\x1DD151231\x1DS1234567890123456\x1E\x04" ),
	    };
	    public ObservableCollection<TVO> ScanCodes { get => scanCodes; set => scanCodes = value; }

	    public ScanCodeValues ScanCodeValues { get; set; } = new ScanCodeValues();

	    public MvvmProperty<string> SerialNumber { get; set; } = MvvmProperty.Create<string>();
	    public MvvmProperty<DateTime> TimeStamp { get; set; } = MvvmProperty.Create<DateTime>();

	    public CounterClass Counters { get; set; } = new CounterClass();

	    public ICommand CommandChangeValueInList { get; set; }
	    public ICommand CommandAddValueToList { get; set; }
	}

	public MvvmData Data { get; set; } = new MvvmData();

	public MainPage()
	{
	    InitializeComponent();
	    BindingContext = this;
	    Task.Run(async () => {
		while (true) {
		    await Task.Delay(TimeSpan.FromSeconds(1));
		    Data.Counters.TimerCount.Value += 1;
		    Data.TimeStamp.Value = DateTime.Now;
		}
	    });
	}

	private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
	    var o = ((sender as ListView).SelectedItem) as TVO;
	    Data.ScanCodeValues = ScanCode.Analyze(o.Code.Value);
	    Data.RefreshAll();
	    Data.SerialNumber.Value = Data.ScanCodeValues.SerialNumber;
	}
    }


}

