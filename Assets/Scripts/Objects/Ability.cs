using System.Collections;
using System.Collections.Generic;

public class Ability {
	public static Dictionary<int,Ability> ABILITIES;
	public int id;
	public string name, description;
	/*public Ability(int id, string name, string description) {
		this.name 			= name;
		this.id				= id;
		this.description 	= description;
	}*/
	private Ability(){}
	private static void initAbilities(List<Dictionary<string,string>> ability_defs) {
		//var results = db["Select * from abilities"][0];
		ABILITIES = new Dictionary<int, Ability> (ability_defs.Count);
		foreach (Dictionary<string,string> row in ability_defs) {
			int id = System.Convert.ToInt32(row["id"]);
			string name = row["name"];
			string desc = row["description"];
			ABILITIES[id] = new Ability(id, name, desc);
		}
	}
}
