using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;
using log4net;

namespace System.Collections.Generic { 
	//Stores and retrieves information encoded as a subclass in a list backed by a generic class
	public class TypeCastDictionary<key_type,parent_type,sub_type> : Dictionary<key_type,parent_type> where sub_type : parent_type  {
		public new sub_type this[key_type key] {
			get => (sub_type) base[key];
			set => base[key] = value;
		}
		
		public TypeCastDictionary() { }
		public TypeCastDictionary(int capacity) : base(capacity) { }
		public TypeCastDictionary(IEqualityComparer<key_type> comparer) : base(comparer) { }
		public TypeCastDictionary(int capacity, IEqualityComparer<key_type> comparer) : base(capacity, comparer) { }
		public TypeCastDictionary(IDictionary<key_type, parent_type> dictionary) : base(dictionary) { }
		public TypeCastDictionary(IDictionary<key_type, parent_type> dictionary, IEqualityComparer<key_type> comparer) : base(dictionary, comparer) { }
	}
}
