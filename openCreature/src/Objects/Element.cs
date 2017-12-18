using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace opencreature {
public class Element : DeserializedElement {
	
	public static TypeCastDictionary<int,DeserializedElement,Element> TYPES;
	public string name;
	
	public Dictionary<int,int> bonus;
	private Element() {}


	public static List<int> VALID_BONUSES = new List<int> {
		000,100,050,200	
	};
	
	public static long init(
		List<Dictionary<string,string>> type_defs, 
		List<Dictionary<string,string>> bonuses
	) {
		TYPES = new TypeCastDictionary<int,DeserializedElement,Element>(type_defs.Count);
		foreach (Dictionary<string,string> row in type_defs) {
			int id = Convert.ToInt32(row["id"]);
			TYPES[id] = new Element {
				id = id,
				name = row["name"],
				bonus = new Dictionary<int, int>()
			};
		}
		
		//results = db["Select * from type_bonus"][0];
		foreach (Dictionary<string,string> row in bonuses) {
			int atk_id = Convert.ToInt32(row["atk_id"]);
			int def_id = Convert.ToInt32(row["def_id"]);
			int bonus  = Convert.ToInt32(row["bonus"]);
			validateBonus(atk_id, def_id, bonus);
			(TYPES[atk_id] as Element).bonus[def_id] = bonus;
		}
		return TYPES.Count;
	}

	public static void validateBonus(int atk_id, int def_id, int bonus) {
		if (!TYPES.ContainsKey(atk_id)) 
			throw new InvalidDataException(String.Format(
				"Bonus {0} set for non-existant type id {1}", 
				bonus, 
				atk_id
			));
		if (!TYPES.ContainsKey(def_id)) 
			throw new InvalidDataException(String.Format(
				"Bonus {0} set for {1}.{2} versus non-existant type id {3}", 
				bonus, 
				atk_id,
				TYPES[atk_id].ToString(), 
				def_id
			));
		if (!VALID_BONUSES.Contains(bonus))
			throw new InvalidDataException(String.Format(
				"Bonus of {0} for {1}.{2} vs {3}.{4} not valid!", 
				bonus, 
				atk_id,
				TYPES[atk_id].ToString(), 
				def_id,
				TYPES[def_id].ToString()
			));
	}

	public static int getTypeBonus(int atk_id, int def_id) {
		return (TYPES[atk_id] as Element).bonus.ContainsKey(def_id)
			? (TYPES[atk_id] as Element).bonus[def_id]
			: 100;
	}	
	
	public static int getTypeBonus(Element atk, Element def) {
		return getTypeBonus(atk.id,def.id);
	}

	public void setTypeBonus(int def_id, int value) {
		validateBonus(this.id, def_id, value);
		this.bonus[def_id] = value;
	}
	
	public void setTypeBonus(Element def, int value) {
		setTypeBonus(def.id,value);
	}
	
	//public int getTypeBonuses() {
	//	if (!TYPES.ContainsKey (atk_id) || !TYPES [atk_id].bonus.ContainsKey (def_id))
	//		return 100;
	//	return TYPES [atk_id].bonus [def_id];
	//}	
	
    public override string ToString() {
    	return name;
    }
	
	public static int    ElemStringToBonus (string bonus) { 
		switch (bonus) {
			case "2" : return 200;
			case "½" : return 050;
			case "0" : return 000;
			case "1" : return 100;
			default  : throw new ArgumentException(String.Format("{0} is not a recognized bonus string!", bonus));
		}
	}
	public static string ElemBonusToString (int    bonus) { 
		switch (bonus) {
			case 200 : return "2";
			case 050 : return "½";
			case 000 : return "0";
			case 100 : return "1";
			default  : throw new ArgumentException(String.Format("{0} is not a recognized bonus amount!", bonus));
		}
	}
}
}