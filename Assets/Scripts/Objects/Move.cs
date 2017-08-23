using System.Collections;
using System.Collections.Generic;

public class Move
{
	public static Dictionary<int,Move> MOVES;
	public int id;
	public string name;
	public byte pp, power, accuracy;
	public int type_id;
	public int world_effect_id, battle_effect_id;
	public MoveAffinity affinity;
	public bool[] misc_info;
	public byte misc_value;
	public string description;
	
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
			temp.world_effect_id = System.Convert.ToByte (row["world_effect_id"]);
			temp.battle_effect_id = System.Convert.ToByte (row["battle_effect_id"]);
			temp.affinity = (MoveAffinity)(System.Convert.ToByte (row["affinity"]));
			temp.misc_info = Sqlite.getBitsFromBlob(row["misc_info"]);
			temp.misc_value = System.Convert.ToByte(row["misc_val"]);
			temp.description = row["description"];
			MOVES[temp.id] = temp;
		}
	}

	public static void link() {
		foreach (Move temp in MOVES.Values) {
			temp.world_effect = Effect.EFFECTS[temp.world_effect_id];
			temp.battle_effect = Effect.EFFECTS[temp.battle_effect_id];
			temp.movetype = Type.TYPES[temp.type_id];
		}
	}
}

