using System.Collections;
using System.Collections.Generic;

namespace opencreature {
public class Element : DeserializedElement {
	public static Dictionary<int,Element> TYPES;
	public string name;
	private Dictionary<int,int> bonus;
	private Element() {}

	public static long init(
		List<Dictionary<string,string>> type_defs, 
		List<Dictionary<string,string>> bonuses
	) {
		long count = 0;
		//var results = db["Select * from types"][0];
		TYPES = new Dictionary<int, Element>(type_defs.Count);
		foreach (Dictionary<string,string> row in type_defs) {
			Element temp = new Element ();
			temp.id = System.Convert.ToInt32 (row ["id"]);
			temp.name = row["name"];
			temp.bonus = new Dictionary<int, int> ();
			TYPES[temp.id] = temp;
			count++;
		}
		
		//results = db["Select * from type_bonus"][0];
		foreach (Dictionary<string,string> row in bonuses) {
			int atk_id = System.Convert.ToInt32(row["atk_id"]);
			int def_id = System.Convert.ToInt32(row["def_id"]);
			int bonus  = System.Convert.ToInt32(row["bonus"]);
			TYPES[atk_id].bonus[def_id] = bonus;
		}
		return count;
	}

	public static int getTypeBonus(int atk_id, int def_id) {
		if (!TYPES.ContainsKey (atk_id) || !TYPES [atk_id].bonus.ContainsKey (def_id))
			return 100;
		return TYPES [atk_id].bonus [def_id];
	}
	
    public override string ToString() {
    	return name;
    }
}
}