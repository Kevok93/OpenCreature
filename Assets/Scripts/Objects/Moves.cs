using System.Collections;
using System.Collections.Generic;

public class Move
{
	public Dictionary<int,Move> MOVES;
	int id;
	string name;
	byte pp, power, accuracy;
	Type movetype;
	int world_effect_id, battle_effect_id;
	Effect world_effect, battle_effect;
	MoveAffinity affinity;
	bool[] misc_info;
	byte misc_value;
	string description;

	private Move(){}
	public static void init(List<Dictionary<string,string>> move_defs) {
		MOVES = new Dictionary<int, Type>(move_defs.Count);
		foreach (Dictionary<string,string> row in move_defs) {
			Move temp = MOVES[id] = new Move ();
			temp.id = System.Convert.ToInt32 (row ["id"]);
			temp.name = row["name"];
			temp.pp = System.Convert.ToByte (row ["pp"]);
			temp.power = System.Convert.ToByte (row ["power"]);
			temp.
		}
	}
	public Move(
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
	}


}

