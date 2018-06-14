using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TestSamples
{
    public class CoreSamples
    {
	protected List<TestSample> _Samples = null;
	public List<TestSample> Samples {
	    get {
		if (_Samples == null) {
		    _Samples = new List<TestSample>();
		    var properties = this.GetType()
			.GetProperties(BindingFlags.Static| BindingFlags.Public)
			.Where(x => x.PropertyType.UnderlyingSystemType == typeof(TestSample));
		    foreach (var p in properties) {
			var testSample= (TestSample)(p.GetValue(this, null));
			if (testSample != null)
			    if (!_Samples.Contains(testSample))
				_Samples.Add(testSample);
		    }
		}
		return _Samples;
	    }
	}
    }
}
