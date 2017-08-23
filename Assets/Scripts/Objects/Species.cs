using System.Collections;
using System.Collections.Generic;

public class Species {
	public static Dictionary<int,Species> SPECIES;
	public int id;
	public string name;
	public byte 
		gender_ratio, 
		capture_rate,
		ev_val;
	public StatsType ev_type; 
	public int max_exp;
	public short[] 
		base_stats,
		max_stats;
	public bool[] tm_list;
	
	public Ability ability1, ability2;
	public Type type1, type2;
	
	private Species(){}
	public static void init(List<Dictionary<string,string>> species_defs) {
		SPECIES = new Dictionary<int, Species>(species_defs.Count);
		foreach (Dictionary<string,string> row in species_defs) {
			Species temp = new Species();
			temp.id = System.Convert.ToInt32 (row ["id"]);
			temp.name = row["name"];
			temp.gender_ratio = System.Convert.ToByte (row["gender_ratio"]);
			temp.capture_rate = System.Convert.ToByte (row["capture_rate"]);
			temp.ev_val = System.Convert.ToByte (row["ev_val"]);
			temp.ev_type = (StatsType) System.Convert.ToByte (row["ev_type"]);
			temp.max_exp = System.Convert.ToInt32 (row["max_exp"]);

			temp.base_stats = new short[6];
			temp.base_stats[StatsType.atk] = System.Convert.ToInt16(row["base_atk"]);
			temp.base_stats[StatsType.def] = System.Convert.ToInt16(row["base_def"]);
			temp.base_stats[StatsType.sp_atk] = System.Convert.ToInt16 (row["base_spatk"]);

			temp.max_stats  = new short[6];

			
			
			
	
	
	}
	/*
	public Species(
		int id,
		string name,
		byte gender_ratio, 
		byte capture_rate, 
		byte ev_type, 
		byte ev_val,
		int max_exp,
		short base_atk,
		short base_def,
		short base_spatk,
		short base_spdef,
		short base_hp,
		short base_speed,
		short max_atk,
		short max_def,
		short max_spatk,
		short max_spdef,
		short max_hp,
		short max_speed,
		int ability1, 
		int ability2,
		int type1, 
		int type2,
		string tm_list
	) {
		this.id 			= id + 1;
		this.name			= name;
		this.gender_ratio	= gender_ratio;
		this.capture_rate	= capture_rate;
		this.ev_type 		= (StatsType) ev_type;
		this.ev_val			= ev_val;
		this.max_exp		= max_exp;
		this.base_stats[0]	= base_atk;
		this.base_stats[1]	= base_def;
		this.base_stats[2]	= base_spatk;
		this.base_stats[3]	= base_spdef;
		this.base_stats[4]	= base_hp;
		this.base_stats[5]	= base_speed;
		this.max_stats[0]	= max_atk;
		this.max_stats[1]	= max_def;
		this.max_stats[2]	= max_spatk;
		this.max_stats[3]	= max_spdef;
		this.max_stats[4]	= max_hp;
		this.max_stats[5]	= max_speed;
		
		this.ability1 		= Creaturedb.abilities[ability1];
		this.ability2 		= Creaturedb.abilities[ability2];
		
		this.type1 			= Creaturedb.types[type1];
		this.type2			= Creaturedb.types[type2];
		
		
		for (int i = 0; i < 128; i++) {
			this.tm_list[i] = Sqlite.getBitFromBlob(tm_list, i);
		}
	}
	*/
	
}
