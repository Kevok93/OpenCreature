using System.Collections.Generic;
using System;

namespace opencreature {

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

public class EggGroup : DeserializedElement {
	public static TypeCastDictionary<int,DeserializedElement> EGG_GROUPS;
	public string name;
	public static long init(List<Dictionary<string,string>> egg_group_defs) {
		EGG_GROUPS = new TypeCastDictionary<int,DeserializedElement> (typeof(EggGroup), egg_group_defs.Count);
		foreach (Dictionary<string,string> row in egg_group_defs) {
			EggGroup temp = new EggGroup ();
			temp.id = Convert.ToInt32 (row ["id"]);
			temp.name = row ["name"];
			EGG_GROUPS [temp.id] = temp;
		}
		return EGG_GROUPS.Count;
	}
}

public class Nature : DeserializedElement {
	public static TypeCastDictionary<int,DeserializedElement> NATURES;
	public string name;
	public BetterEnumArray<StatsType,sbyte> stats_mod;
	public static long init(List<Dictionary<string,string>> nature_defs) {
		NATURES = new TypeCastDictionary<int,DeserializedElement> 
			(typeof(Nature), nature_defs.Count);
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
		return NATURES.Count;
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

public class ItemType : DeserializedElement {
	public static TypeCastDictionary<int,DeserializedElement> ITEM_TYPES;
	public string name;
	public string description;
	public static long init(List<Dictionary<string,string>> item_type_defs) {
		ITEM_TYPES = new TypeCastDictionary<int,DeserializedElement> 
			(typeof(ItemType), item_type_defs.Count);
		foreach (Dictionary<string,string> row in item_type_defs) {
			ItemType temp = new ItemType ();
			temp.id = Convert.ToInt32 (row ["id"]);
			temp.name = row ["name"];
			temp.description = row ["description"];
			ITEM_TYPES [temp.id] = temp;
		}
		return ITEM_TYPES.Count;
	}
}

public class TrainerStyle : DeserializedElement {
	public static TypeCastDictionary<int,DeserializedElement> TRAINER_STYLES;
	public string name;
	public string sprite_path;
	/*	public Sprite sprite;
	 * public music bgm;*/
}

public class NPCStyle : DeserializedElement {
	public static TypeCastDictionary<int,DeserializedElement> NPC_STYLES;
	public string name;
	public string sprite_path;
	/*public Sprite sprite;*/
}

public class PlotFlag : DeserializedElement {
		//TODO: unify by_name and by_id variables?
	public static TypeCastDictionary<string,DeserializedElement> PLOT_FLAG_NAME;
	public static TypeCastDictionary<int   ,DeserializedElement> PLOT_FLAG_ID;
	public string name;
	public sbyte value;
	public static long init(List<Dictionary<string,string>> plot_flag_defs) {
		PLOT_FLAG_ID =   new TypeCastDictionary<int   ,DeserializedElement> (typeof(PlotFlag),plot_flag_defs.Count);
		PLOT_FLAG_NAME = new TypeCastDictionary<string,DeserializedElement> (typeof(PlotFlag),plot_flag_defs.Count);
		foreach (Dictionary<string,string> row in plot_flag_defs) {
			PlotFlag temp = new PlotFlag ();
			temp.id = Convert.ToInt32 (row ["id"]);
			temp.name = row ["name"];
			temp.value = Convert.ToSByte (row ["value"]);
			PLOT_FLAG_ID [temp.id] = temp;
			PLOT_FLAG_NAME [temp.name] = temp;
		}
		return PLOT_FLAG_ID.Count;
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
	
	public class point3D {
		public float x,y,z;
		public point3D(float x, float y, float z) {
			this.x=x; this.y=y; this.z=z;
		}
	}
	
	public class point2D {
		public float x,y;
		public point2D(float x, float y) {
			this.x=x; this.y=y; 
		}
	}
	public class tri3D {
		public point3D a,b,c;
		public tri3D(point3D a, point3D b, point3D c) {
			this.a=a; this.b=b; this.c=c;
		}
	}
	public class tri2D {
		public point2D a,b,c;
		public tri2D(point2D a, point2D b, point2D c) {
			this.a=a; this.b=b; this.c=c;
		}
	}

}