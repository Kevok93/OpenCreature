/*
 * Created by SharpDevelop.
 * User: kwest
 * Date: 6/8/2016
 * Time: 17:01
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace opencreature
{
	public class DeserializedElement {
		public int id;
		
		public static Dictionary<int,DeserializedElement> getGenericCollection<t>(Dictionary<int,t> complex) where t : DeserializedElement {
			return complex.ToDictionary(
				item => item.Key, 
				item => (DeserializedElement)item.Value
			);
		}
		
	}
	
}
