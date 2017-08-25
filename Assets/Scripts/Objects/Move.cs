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
    public StatsType atkAffinity, defAffinity;
	public BetterEnumArray<MoveData,bool> misc_info;
	public byte misc_value;
	public string description;
	
	public Effect world_effect, battle_effect;
	public Type movetype;

	private Move(){}
	public static long init(List<Dictionary<string,string>> move_defs) {
		long count = 0;
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
			temp.misc_info = SqliteConnection.getBitsFromBlob(row["misc_info"]);
			temp.misc_value = System.Convert.ToByte(row["misc_val"]);
			temp.description = row["description"];
            switch (temp.affinity) {
                case MoveAffinity.Physical:
                    temp.atkAffinity = StatsType.atk;
                    temp.defAffinity = StatsType.def;
                    break;
                case MoveAffinity.Special:
                    temp.atkAffinity = StatsType.sp_atk;
                    temp.defAffinity = StatsType.sp_def;
                    break;
            }
			MOVES[temp.id] = temp;
			count++;
		}
		return count;
	}

	public static void link() {
		foreach (Move temp in MOVES.Values) {
			if (temp.world_effect_id != 0)	temp.world_effect = Effect.EFFECTS[temp.world_effect_id];
			if (temp.battle_effect_id != 0)	temp.battle_effect = Effect.EFFECTS[temp.battle_effect_id];
			temp.movetype = Type.TYPES[temp.type_id];
		}
	}
	public override string ToString() {
		return System.String.Format("{0} ({1}/{2})", name, power, accuracy);
	}
}

