using System.Collections;
using System.Collections.Generic;

namespace opencreature {
public class Element : DeserializedElement {
	public static TypeCastDictionary<int,DeserializedElement> TYPES;
	public string name;
	private Dictionary<int,int> bonus;
	private Element() {}

	public static long init(
		List<Dictionary<string,string>> type_defs, 
		List<Dictionary<string,string>> bonuses
	) {
		TYPES = new TypeCastDictionary<int,DeserializedElement>(typeof(Element), type_defs.Count);
		foreach (Dictionary<string,string> row in type_defs) {
			Element temp = new Element ();
			temp.id = System.Convert.ToInt32 (row ["id"]);
			temp.name = row["name"];
			temp.bonus = new Dictionary<int, int> ();
			TYPES[temp.id] = temp;
		}
		
		//results = db["Select * from type_bonus"][0];
		foreach (Dictionary<string,string> row in bonuses) {
			int atk_id = System.Convert.ToInt32(row["atk_id"]);
			int def_id = System.Convert.ToInt32(row["def_id"]);
			int bonus  = System.Convert.ToInt32(row["bonus"]);
			TYPES[atk_id].bonus[def_id] = bonus;
		}
		return TYPES.Count;
	}

	public static int getTypeBonus(int atk_id, int def_id) {
		if (!TYPES.ContainsKey (atk_id) || !TYPES [atk_id].bonus.ContainsKey (def_id))
			return 100;
		return TYPES [atk_id].bonus [def_id];
	}	
	public static int getTypeBonus(Element atk, Element def) {
		return getTypeBonus(atk.id,def.id);
	}
	
	public void setTypeBonus(Element def, int value) {
		this.bonus[def.id] = value;
	}
	
    public override string ToString() {
    	return name;
    }
	
	public static int    ElemStringToBonus (string bonus) { 
		switch (bonus) {
			case "2" : return 200 ;
			case "½" : return 050 ;
			case "0" : return 000 ;
			default  : return 100 ;
		}
	}
	public static string ElemBonusToString (int    bonus) { 
		switch (bonus) {
			case 200 : return "2x";
			case 050 : return "½x";
			case 000 : return "0x";
			default  : return "1x";
		}
	}
}
}