using System.Collections;
using System.Collections.Generic;

public class Move
{
	public Dictionary<int,Move> MOVES;
	int id;
	string name;
	byte pp, power, accuracy;
	int type_id;
	int world_effect_id, battle_effect_id;
	MoveAffinity affinity;
	bool[] misc_info;
	byte misc_value;
	string description;
	
	Effect world_effect, battle_effect;
	Type movetype;

	private Move(){}
	public static void init(List<Dictionary<string,string>> move_defs) {
		MOVES = new Dictionary<int, Move>(move_defs.Count);
		foreach (Dictionary<string,string> row in move_defs) {
			Move temp = new Move ();
			temp.id = System.Convert.ToInt32 (row ["id"]);
			temp.name = row["name"];
			temp.pp = System.Convert.ToByte (row ["pp"]);
			temp.power = System.Convert.ToByte (row ["power"]);
			temp.accuracy = System.Convert.ToByte (row ["accuracy"]);
			temp.type_id = System.Convert.ToByte (row ["type"]);
			temp.world_effect_id = System.Convert.ToByte (row["world_effect"]);
			temp.battle_effect_id = System.Convert.ToByte (row["battle_effect"]);
			temp.affinity = (MoveAffinity)(System.Convert.ToByte (row["affinity"]);
			temp.misc_info = Sqlite.getBitsFromBlob(row["misc_info"]);
			temp.misc_value = System.Convert.ToByte(row["misc_value"]);
			temp.description = row["description"];
			MOVES[temp.id] = temp;
		}
	}

	public static void link() {
		foreach (Move temp in MOVES.Values) {
			world_effect = Effect.EFFECTS[temp.world_effect_id];
			battle_effect = Effect.EFFECTS[temp.battle_effect_id];
			movetype = Type.TYPES[type_id];
		}
	}

	/*public Move(
		int id,
		string name,
		byte pp, 
		byte power, 
		byte accuracy,
		Type movetype,
		Effect world_effect, 
		Effect battle_effect,
		MoveAffinity affinity,
		bool[] misc_info,
		byte misc_value,
		string description
	) { 
		this.id = id;
		this.name = name;
		this.pp = pp;
		this.power = power;
		this.accuracy = accuracy;
		this.movetype = movetype;
		this.world_effect = world_effect;
		this.battle_effect = battle_effect;
		this.affinity = affinity;
		this.misc_info = misc_info;
		this.misc_value = misc_value;
		this.description = description;
	}*/


}

