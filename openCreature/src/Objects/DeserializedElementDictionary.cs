using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Collections.Generic { 
	//Stores and retrieves information encoded as a subclass in a list backed by a generic class
	public class TypeCastDictionary<key_type,generic_type> : Dictionary<key_type,generic_type> {
		public Type sub_type;
		
		public TypeCastDictionary(Type inheritingClass) 
			{ sharedInitializer(inheritingClass); }
		public TypeCastDictionary(Type inheritingClass, int initialSize) : base(initialSize) 
			{ sharedInitializer(inheritingClass); }
		
		public void sharedInitializer(Type sub_type) {
			this.sub_type = sub_type;
		}
		
		public new dynamic this[key_type key] {
			get{return      Convert.ChangeType(base[key],this.sub_type);}
			set{base[key] = Convert.ChangeType(value    ,this.sub_type);}
		}
	}
}
