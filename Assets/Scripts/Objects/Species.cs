using System.Collections.Generic;
using System;

public class Species {
	public static Dictionary<int,Species> SPECIES;
	public int id;
	public string name;
	public short ability_id1, ability_id2;
	public byte type_id1, type_id2;
	public byte 
		gender_ratio, 
		capture_rate,
		ev_val;
	public StatsType ev_type;
    public short base_happiness;
	public bool[] misc_info;
	public ushort egg_steps;
	public byte egg_group_id1, egg_group_id2;
	public int wild_item_id;
	public byte wild_item_pct;
	public float height, weight;
	public string classification, dex_entry;
	public int cry_sfx_id;
	public string path_to_sprites;
	public short[] 
		base_stats,
		max_stats;
	public int max_exp;
	public bool[] tm_list;
	public LinkedList<LevelMove> level_moves;
	public LinkedList<Evolution> evolutions;
	
	public Ability ability1, ability2;
	public Type type1, type2;
	public EggGroup egg_group1, egg_group2;
	public Item wild_item;
	/* 
	 * public Sfx cry_sfx;
	 * public Sprite sprite_def;
	 * */
	
	private Species(){}
	public static void init(List<Dictionary<string,string>> species_defs, List<Dictionary<string,string>> level_move_defs, List<Dictionary<string,string>> evolution_defs) {
		SPECIES = new Dictionary<int, Species>(species_defs.Count);
		foreach (Dictionary<string,string> row in species_defs) {
			Species temp = new Species();
			temp.id = Convert.ToInt32 (row ["id"]);
			temp.name = row["name"];
			temp.gender_ratio = Convert.ToByte (row["gender_ratio"]);
			temp.capture_rate = Convert.ToByte (row["capture_rate"]);
			temp.ev_val = Convert.ToByte (row["ev_val"]);
			temp.ev_type = (StatsType) Convert.ToByte (row["ev_type"]);
			temp.max_exp = Convert.ToInt32 (row["max_exp"]);
			temp.tm_list = SqliteConnection.getBitsFromBlob(row["tm_list"]);
			temp.misc_info = SqliteConnection.getBitsFromBlob(row["misc_info"]);
			temp.egg_steps = Convert.ToUInt16(row["egg_steps"]);
			temp.egg_group_id1 = Convert.ToByte(row["egg_group1"]);
			temp.egg_group_id2 = Convert.ToByte(row["egg_group2"]);
			temp.wild_item_id = Convert.ToInt32(row["wild_item"]);
			temp.wild_item_pct = Convert.ToByte(row["wild_item_pct"]);
			temp.height = Convert.ToSingle(row["height"]);
			temp.weight = Convert.ToSingle(row["weight"]);
			temp.classification = row["classification"];
			temp.dex_entry = row["dex_entry"];
			temp.cry_sfx_id = Convert.ToInt32(row["cry"]);
			temp.path_to_sprites = row["sprite_path"];
			
			temp.base_stats = new short[6];
			temp.base_stats[(int)StatsType.atk] = Convert.ToInt16(row["base_atk"]);
			temp.base_stats[(int)StatsType.def] = Convert.ToInt16(row["base_def"]);
			temp.base_stats[(int)StatsType.sp_atk] = Convert.ToInt16 (row["base_spatk"]);
			temp.base_stats[(int)StatsType.sp_def] = Convert.ToInt16 (row["base_spdef"]);
			temp.base_stats[(int)StatsType.hp] = Convert.ToInt16(row["base_hp"]);
			temp.base_stats[(int)StatsType.speed] = Convert.ToInt16(row["base_speed"]);
			
			temp.max_stats  = new short[6];
			temp.max_stats[(int)StatsType.atk] = Convert.ToInt16(row["max_atk"]);
			temp.max_stats[(int)StatsType.def] = Convert.ToInt16(row["max_def"]);
			temp.max_stats[(int)StatsType.sp_atk] = Convert.ToInt16 (row["max_spatk"]);
			temp.max_stats[(int)StatsType.sp_def] = Convert.ToInt16 (row["max_spdef"]);
			temp.max_stats[(int)StatsType.hp] = Convert.ToInt16(row["max_hp"]);
			temp.max_stats[(int)StatsType.speed] = Convert.ToInt16(row["max_speed"]);
			temp.level_moves = new LinkedList<LevelMove>();
            temp.evolutions = new LinkedList<Evolution>();
			SPECIES[temp.id] = temp;
		}
		
		foreach(Dictionary<string,string> row in level_move_defs) {
		    LevelMove temp = new LevelMove();
		    temp.level = Convert.ToByte(row["level"]);
		    temp.move_id = Convert.ToInt32(row["move_id"]);
		    int species_id = Convert.ToInt32(row["species_id"]);
		    SPECIES[species_id].level_moves.AddLast(temp);
		}
		
		foreach(Dictionary<string,string> row in evolution_defs) {
		    Evolution temp = new Evolution();
		    temp.value = Convert.ToInt32(row["value"]);
		    temp.species_id = Convert.ToInt32(row["to_species"]);
		    temp.species = SPECIES[temp.species_id];
		    temp.type = (EvolutionType)Convert.ToByte(row["type"]);
		    int species_id = Convert.ToInt32(row["from_species"]);
		    SPECIES[species_id].evolutions.AddLast(temp);
		}
	}
	public static void link() {
	    foreach (Species temp in SPECIES.Values) {
            temp.ability1 = Ability.ABILITIES[temp.ability_id1];
            temp.ability1 = Ability.ABILITIES[temp.ability_id1];
            temp.type1 = Type.TYPES[temp.type_id1];
            temp.type2 = Type.TYPES[temp.type_id2];
			temp.egg_group1 = EggGroup.EGG_GROUPS [temp.egg_group_id1];
			temp.egg_group2 = EggGroup.EGG_GROUPS [temp.egg_group_id2];
			temp.wild_item = Item.ITEMS[temp.wild_item_id];

            foreach (LevelMove move in temp.level_moves) 
                move.move = Move.MOVES[move.move_id];
            foreach (Evolution ev in temp.evolutions) 
                if ((ev.type == EvolutionType.Item || ev.type == EvolutionType.Trade) && ev.value != 0) 
                    {ev.item = Item.ITEMS[ev.value];}
	    }
	}
}
