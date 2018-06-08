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

    public class MvvmObject : INotifyPropertyChanged, IMvvmOnPropertyChanged {
	public MvvmObject()
	{
	    foreach (var mvvmProperty in MvvmProperties) {
		mvvmProperty.MvvmParent = this;
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
	public void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
	    (PropertyChanged)?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	public void RefreshAll() => OnPropertyChanged(string.Empty);
	#endregion
    }

    public class MvvmProperty {
	public string PropertyName { get; set; }
	public IMvvmOnPropertyChanged MvvmParent { get; set; } = null;
	public void OnPropertyChanged(string propertyName = null) =>
		MvvmParent?.OnPropertyChanged(propertyName ?? PropertyName);
	public void RefreshAll() => OnPropertyChanged(string.Empty);
	public static MvvmProperty<T> Create<T>([CallerMemberName]string propertyName = "") where T : IComparable
	{
	    return new MvvmProperty<T>() { PropertyName = propertyName };
	}
    }
    public class MvvmProperty<T> : MvvmProperty where T : IComparable {
	protected T _Value = default(T);
	public T Value {
	    get => _Value; set {
		try {
		    if (value.CompareTo(_Value) != 0) {
			var o = _Value;
			_Value = value;
			OnPropertyChanged();
			OnChanged?.Invoke(o, value);
		    }
		} catch (Exception ex) {
		    Debug.WriteLine(ex);
		    _Value = value;
		    OnPropertyChanged();
		}
	    }
	}
	public Action<T, T> OnChanged { get; set; }

	public MvvmProperty<T> DoOnChanged(Action<T, T> onChanged)
	{
	    OnChanged = onChanged;
	    return this;
	}
    }
}
