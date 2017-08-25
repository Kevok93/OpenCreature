using System.Collections.Generic;
using System;
public class LevelMove {
    public byte level;
    public int move_id;
    public Move move;
    public override string ToString() {
    	return String.Format("{0,2}: {1}",level, move);
    }
}
public class Evolution {
    public int value; //can be level, happiness, or item id
    public int species_id;
    public Species species;
    public EvolutionType type;
    public Item item;
    public override string ToString() {return "=> "+species;}
}
public class EggGroup {
	public static Dictionary<int,EggGroup> EGG_GROUPS;
	public int id;
	public string name;
	public static void init(List<Dictionary<string,string>> egg_group_defs) {
		EGG_GROUPS = new Dictionary<int, EggGroup> (egg_group_defs.Count);
		foreach (Dictionary<string,string> row in egg_group_defs) {
			EggGroup temp = new EggGroup ();
			temp.id = Convert.ToInt32 (row ["id"]);
			temp.name = row ["name"];
			EGG_GROUPS [temp.id] = temp;
		}
	}
}
public class Nature {
	public static Dictionary<int,Nature> NATURES;
	public int id;
	public string name;
	public BetterEnumArray<StatsType,sbyte> stats_mod;
	public static void init(List<Dictionary<string,string>> nature_defs) {
		NATURES = new Dictionary<int, Nature> (nature_defs.Count);
		foreach (Dictionary<string,string> row in nature_defs) {
			Nature temp = new Nature ();
			temp.id = Convert.ToInt32 (row ["id"]);
			temp.name = row ["name"];
			temp.stats_mod = new sbyte[] {
				Convert.ToSByte(row["atk"]),
				Convert.ToSByte(row["def"]),
				Convert.ToSByte(row["spatk"]),
				Convert.ToSByte(row["spdef"]),
				0,
				Convert.ToSByte(row["speed"]),
			};
			NATURES [temp.id] = temp;
		}
	}
	public override string ToString() {
		int plus = 0, minus = 0;
		for (int i = 0; i < 6; i++) {
			if (stats_mod[i] > 0) plus = i;
			if (stats_mod[i] < 0) minus = i;
		}
		return String.Format(
			"{0}: {1}+{2} {3}{4}", 
			name,
            (StatsType)plus,
            stats_mod[plus],
            (StatsType)minus,
            stats_mod[minus]
       );
	}
}
public class ItemType {
	public static Dictionary<int,ItemType> ITEM_TYPES;
	public int id;
	public string name;
	public string description;
	public static void init(List<Dictionary<string,string>> item_type_defs) {
		ITEM_TYPES = new Dictionary<int, ItemType> (item_type_defs.Count);
		foreach (Dictionary<string,string> row in item_type_defs) {
			ItemType temp = new ItemType ();
			temp.id = Convert.ToInt32 (row ["id"]);
			temp.name = row ["name"];
			temp.description = row ["description"];
			ITEM_TYPES [temp.id] = temp;
		}
	}
}
public class TrainerStyle {
	public static Dictionary<int,TrainerStyle> TRAINER_STYLES;
	public byte id;
	public string name;
	public string sprite_path;
	/*	public Sprite sprite;
	 * public music bgm;*/
}
public class NPCStyle {
	public static Dictionary<int,NPCStyle> NPC_STYLES;
	public int id;
	public string name;
	public string sprite_path;
	/*public Sprite sprite;*/
}
public class PlotFlag {
	public static Dictionary<string,PlotFlag> PLOT_FLAG_NAME;
	public static Dictionary<int,PlotFlag> PLOT_FLAG_ID;
	public int id;
	public string name;
	public sbyte value;
	public static void init(List<Dictionary<string,string>> plot_flag_defs) {
		PLOT_FLAG_ID = new Dictionary<int, PlotFlag> (plot_flag_defs.Count);
		PLOT_FLAG_NAME = new Dictionary<string, PlotFlag> (plot_flag_defs.Count);
		foreach (Dictionary<string,string> row in plot_flag_defs) {
			PlotFlag temp = new PlotFlag ();
			temp.id = Convert.ToInt32 (row ["id"]);
			temp.name = row ["name"];
			temp.value = Convert.ToSByte (row ["value"]);
			PLOT_FLAG_ID [temp.id] = temp;
			PLOT_FLAG_NAME [temp.name] = temp;
		}
	}
}

public struct LearnedMove {
	public short pp_cur;
	public short pp_max;
	public Move moveDef;
	public LearnedMove(Move moveDef, short pp_max, short pp_cur) {
		this.pp_cur = pp_cur;
		this.pp_max = pp_max;
		this.moveDef = moveDef;
	}
	public LearnedMove(Move moveDef) {
		this.moveDef = moveDef;
		this.pp_cur = this.pp_max = moveDef.pp;
	}
    public override string ToString() {
    	return String.Format("{0} [{1}/{2}]",moveDef, pp_cur, pp_max);
    }
}
