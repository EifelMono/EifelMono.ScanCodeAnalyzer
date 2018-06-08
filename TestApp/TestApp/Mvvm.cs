using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace EifelMono.Mvvm {

    public interface IMvvmOnPropertyChanged {
	void OnPropertyChanged(string propertyName);
    }

    public interface IMvvmObject {
	IMvvmOnPropertyChanged MvvmParent { get; set; }
    }

    public class MvvmObject : INotifyPropertyChanged, IMvvmOnPropertyChanged, IMvvmObject {

	public MvvmObject()
	{
	    foreach (var property in MvvmProperties) {
		property.MvvmParent = this;
	    }
	}

	#region MvvmProperties
	protected List<MvvmProperty> _MvvmProperties = null;
	public List<MvvmProperty> MvvmProperties {
	    get {
		if (_MvvmProperties == null) {
		    _MvvmProperties = new List<MvvmProperty>();
		    var properties = this.GetType()
			.GetProperties(BindingFlags.Instance | BindingFlags.Public)
			.Where(x => x.PropertyType.IsSubclassOf(typeof(MvvmProperty)));
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
	#endregion

	#region Bindings

	public event PropertyChangedEventHandler PropertyChanged;
	public IMvvmOnPropertyChanged MvvmParent { get; set; } = null;
	public void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
	    (PropertyChanged)?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	public void SelfOnPropertyChanged(string propertyName = null)
	{
	    if (MvvmParent != null)
		MvvmParent.OnPropertyChanged(propertyName);
	    else
		OnPropertyChanged(propertyName);
	}
	public void RefreshAll() => SelfOnPropertyChanged(string.Empty);
	#endregion
    }

    public class MvvmProperty : INotifyPropertyChanged, IMvvmOnPropertyChanged, IMvvmObject  {

	public string PropertyName { get; set; }
	public event PropertyChangedEventHandler PropertyChanged;
	public IMvvmOnPropertyChanged MvvmParent { get; set; } = null;

	public void OnPropertyChanged(string propertyName = null) =>
	    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName ?? PropertyName));
	public void SelfOnPropertyChanged(string propertyName = null)
	{

	    if (MvvmParent != null)
		MvvmParent.OnPropertyChanged(propertyName?? PropertyName);
	    else
		OnPropertyChanged(propertyName ?? PropertyName);
	}

	public void RefreshAll() => SelfOnPropertyChanged(string.Empty);

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
			SelfOnPropertyChanged();
		    }
		} catch (Exception ex) {
		    Debug.WriteLine(ex);
		    _Value = value;
		    SelfOnPropertyChanged();
		}

	    }
	}
    }
}
