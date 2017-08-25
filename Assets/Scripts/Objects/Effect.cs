using System;
using System.Collections.Generic;

public class Effect {
	public static Dictionary<int,Effect> EFFECTS;
	public int id;
	public string name, description;
	public bool[] misc_info;
	public byte misc_val1, misc_val2;
	public byte length;
	public string text;

	private Effect(){}

	public static void init(List<Dictionary<string,string>> results) {
	    EFFECTS = new Dictionary<int, Effect>(results.Count);
	    foreach (Dictionary<string,string> row in results) {
	        Effect temp = new Effect();
	        temp.id = Convert.ToInt32(row["id"]);
	        temp.name = row["name"];
	        temp.description = row["description"];
	        temp.text = row["text"];
	        temp.misc_info = SqliteConnection.getBitsFromBlob(row["misc_info"]);
	        temp.misc_val1 = Convert.ToByte(row["misc_val1"]);
	        temp.misc_val2 = Convert.ToByte(row["misc_val2"]);
	        temp.length = Convert.ToByte(row["length"]);
	        EFFECTS[temp.id] = temp;
	    }
	}

}
