using System.Collections;
using System.Collections.Generic;

public class Type {
	public static Dictionary<int,Type> TYPES;
	public int id;
	public string name;
	private Dictionary<int,float> bonus;
	private Type() {}

	public static void init(
		List<Dictionary<string,string>> type_defs, 
		List<Dictionary<string,string>> bonuses
	) {
		//var results = db["Select * from types"][0];
		TYPES = new Dictionary<int, Type>(type_defs.Count);
		foreach (Dictionary<string,string> row in type_defs) {
			Type temp = new Type ();
			temp.id = System.Convert.ToInt32 (row ["id"]);
			temp.name = row["name"];
			temp.bonus = new Dictionary<int, float> ();
			TYPES[temp.id] = temp;
		}
		
		//results = db["Select * from type_bonus"][0];
		foreach (Dictionary<string,string> row in bonuses) {
			int atk_id = System.Convert.ToInt32(row["atk_id"]);
			int def_id = System.Convert.ToInt32(row["def_id"]);
			float bonus = System.Convert.ToSingle(row["bonus"]);
			TYPES[atk_id].bonus[def_id] = bonus;
		}
	}

	public static float getTypeBonus(int atk_id, int def_id) {
		if (!TYPES.ContainsKey (atk_id) || !TYPES [atk_id].bonus.ContainsKey (def_id))
			return 1f;
		return TYPES [atk_id].bonus [def_id];
	}
}
