using System;
using System.Collections.Generic;


namespace opencreature {
public class Effect : DeserializedElement {
	public static TypeCastDictionary<int,DeserializedElement> EFFECTS;
	public string name, description;
	public BetterEnumArray<EffectData,bool> misc_info;
	public byte misc_val1, misc_val2;
	public byte length;
	public string text;

	private Effect(){}

	public static long init(List<Dictionary<string,string>> results) {
		EFFECTS = new TypeCastDictionary<int,DeserializedElement>(typeof(Effect),results.Count);
	    foreach (Dictionary<string,string> row in results) {
	        Effect temp = new Effect();
	        temp.id = Convert.ToInt32(row["id"]);
	        temp.name = row["name"];
	        temp.description = row["description"];
	        temp.text = row["text"];
	        temp.misc_info = SqliteWrapper.getBitsFromBlob(row["misc_info"]);
	        temp.misc_val1 = Convert.ToByte(row["misc_val1"]);
	        temp.misc_val2 = Convert.ToByte(row["misc_val2"]);
	        temp.length = Convert.ToByte(row["length"]);
	        EFFECTS[temp.id] = temp;
	    }
	    return EFFECTS.Count;
	}

}
}