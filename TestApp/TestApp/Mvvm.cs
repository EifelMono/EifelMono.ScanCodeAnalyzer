using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace TestApp {

    public interface IOnPropertyChanged {
	void OnPropertyChanged(string propertyName);
    }

    public interface IMvvmParent {
	IOnPropertyChanged MvvmParent { get; set; }
    }

    public class MvvmObject : INotifyPropertyChanged, IOnPropertyChanged, IMvvmParent {

	public MvvmObject()
	{
	    foreach (var property in MvvmProperties) {
		property.MvvmParent = this;
	    }
	}

	protected List<MvvmProperty> _MvvmProperties = null;
	public List<MvvmProperty> MvvmProperties {
	    get {
		if (_MvvmProperties == null) {
		    _MvvmProperties = new List<MvvmProperty>();
		    var properties = this.GetType()
			.GetProperties(BindingFlags.Instance | BindingFlags.Public)
			.Where(ø => ø.PropertyType.IsSubclassOf(typeof(MvvmProperty)));
		    foreach (var p in properties) {
			var identifier = (MvvmProperty)(p.GetValue(this, null));
			if (identifier != null)
			    if (!_MvvmProperties.Contains(identifier))
				_MvvmProperties.Add(identifier);
		    }
		}
		return _MvvmProperties;
	    }
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public IOnPropertyChanged MvvmParent { get; set; } = null;
	public void OnPropertyChanged([CallerMemberName]string propertyName = "")
	{
	    (PropertyChanged)?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
	public void UseOnPropertyChanged(string propertyName = null)
	{
	    if (MvvmParent != null)
		MvvmParent.OnPropertyChanged(propertyName);
	    else
		OnPropertyChanged(propertyName);
	}

	public void Refresh() => OnPropertyChanged(string.Empty);
    }


    public class MvvmProperty : INotifyPropertyChanged, IOnPropertyChanged, IMvvmParent {

	public string PropertyName { get; set; }
	public event PropertyChangedEventHandler PropertyChanged;
	public IOnPropertyChanged MvvmParent { get; set; } = null;

	public void OnPropertyChanged(string propertyName = null)
	{
	    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName ?? PropertyName));
	}
	public void UseOnPropertyChanged(string propertyName = null)
	{
	    if (MvvmParent != null)
		MvvmParent.OnPropertyChanged(propertyName);
	    else
		OnPropertyChanged(propertyName);
	}

	public static MvvmProperty<T> Create<T>([CallerMemberName]string propertyName = "") where T : IComparable
	{
	    return new MvvmProperty<T>() { PropertyName = propertyName };
	}

    }
    public class MvvmProperty<T> : MvvmProperty where T : IComparable {

	protected T _Value;
	public T Value {
	    get => _Value; set {
		try {
		    if (value.CompareTo(_Value) != 0) {
			_Value = value;
			UseOnPropertyChanged();
		    }
		} catch (Exception ex) {
		    Debug.WriteLine(ex);
		    _Value = value;
		    UseOnPropertyChanged();
		}

	    }
	}
    }
}
