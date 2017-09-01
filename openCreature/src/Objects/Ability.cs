using System;
using System.Collections.Generic;

namespace opencreature {
public class Ability : DeserializedElement {
	public static TypeCastDictionary<int,DeserializedElement> ABILITIES;
	public string name, description;
	public int battle_effect_id, world_effect_id;
	public Effect world_effect, battle_effect;
	
	private Ability(){}
	public static long init(List<Dictionary<string,string>> ability_defs) {
		ABILITIES = new TypeCastDictionary<int,DeserializedElement>(typeof(Ability),ability_defs.Count);
		foreach (Dictionary<string,string> row in ability_defs) {
		    Ability temp = new Ability();
			temp.id = Convert.ToInt32(row["id"]);
			temp.name = row["name"];
			temp.description = row["description"];
			temp.battle_effect_id = Convert.ToInt32(row["battle_effect_id"]);
			temp.world_effect_id = Convert.ToInt32(row["world_effect_id"]);
			ABILITIES[temp.id] = temp;
		}
		return ABILITIES.Count;
	}
	public static void link() {
	    foreach (Ability temp in ABILITIES.Values) {
			if (temp.world_effect_id  != 0) temp.world_effect  = Effect.EFFECTS[temp.world_effect_id ];
	        if (temp.battle_effect_id != 0)	temp.battle_effect = Effect.EFFECTS[temp.battle_effect_id];
	    }
	}
	public override string ToString() {
		return String.Format("{0}", name);
	}
}
}