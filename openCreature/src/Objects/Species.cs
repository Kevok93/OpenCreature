using System.Collections.Generic;
using System;
using System.Linq;


namespace opencreature {
public class Species : DeserializedElement {
	public static TypeCastDictionary<int,DeserializedElement,Species> SPECIES;
	public string name;
	
	//base
	public short 
		ability_id1, 
		ability_id2;
	public Ability ability1, ability2;
	
	public byte 
		type_id1, 
		type_id2;
	public Element type1, type2;
	
	public short base_happiness;
	public BetterEnumArray<UniqueCreatureData,bool> misc_info;
	public BetterEnumArray<StatsType,short> 
		base_stats,
		max_stats;
	public int max_exp;
	public bool[] tm_list;
	public List<LevelMove> level_moves;
	public List<Evolution> evolutions;

	
	//wild
	public int wild_item_id;
	public byte wild_item_pct;
	public Item wild_item;
	public byte ev_val;
	public StatsType ev_type;
	public byte
		gender_ratio,
		capture_rate;
	
	//egg
	public ushort egg_steps;
	public byte egg_group_id1, egg_group_id2;
	public EggGroup egg_group1, egg_group2;
	
	//dex info
	public float height, weight;
	public string classification, dex_entry;
	public int cry_sfx_id;
	public string path_to_sprites;
	/* todo; abstract
	 * public Sfx cry_sfx;
	 * public Sprite sprite_def;
	 * */
	
	private Species(){}
	public static long init(List<Dictionary<string,string>> species_defs, List<Dictionary<string,string>> level_move_defs, List<Dictionary<string,string>> evolution_defs) {
		SPECIES = new TypeCastDictionary<int,DeserializedElement,Species>(species_defs.Count);
		foreach (Dictionary<string,string> row in species_defs) {
			int id = Convert.ToInt32(row["id"]);
			Species temp = new Species {
				             id = id,
				           name = row["name"],
				   gender_ratio = Convert.ToByte(row["gender_ratio"]),
				   capture_rate = Convert.ToByte(row["capture_rate"]),
				       type_id1 = Convert.ToByte(row["type1"]),
				       type_id2 = Convert.ToByte(row["type2"]),
				         ev_val = Convert.ToByte(row["ev_val"]),
				        ev_type = (StatsType) Convert.ToByte(row["ev_type"]),
				        max_exp = Convert.ToInt32(row["max_exp"]),
				        tm_list = AbstractDatabase.getBitsFromBlob(row["tm_list"]),
				      misc_info = AbstractDatabase.getBitsFromBlob(row["misc_info"]),
				    ability_id1 = Convert.ToByte(row["ability1"]),
				    ability_id2 = Convert.ToByte(row["ability2"]),
				      egg_steps = Convert.ToUInt16(row["egg_steps"]),
				  egg_group_id1 = Convert.ToByte(row["egg_group1"]),
				  egg_group_id2 = Convert.ToByte(row["egg_group2"]),
				   wild_item_id = Convert.ToInt32(row["wild_item"]),
				  wild_item_pct = Convert.ToByte(row["wild_item_pct"]),
				         height = Convert.ToSingle(row["height"]),
				         weight = Convert.ToSingle(row["weight"]),
				 classification = row["classification"],
				      dex_entry = row["dex_entry"],
				     cry_sfx_id = Convert.ToInt32(row["cry"]),
				path_to_sprites = row["sprite_path"],
				     base_stats = new short[6],
				      max_stats = new short[6],
				    level_moves = new List<LevelMove>(),
				     evolutions = new List<Evolution>(),
			};

			temp.base_stats[StatsType.atk    ] = Convert.ToInt16(row["base_atk"   ]);
			temp.base_stats[StatsType.def    ] = Convert.ToInt16(row["base_def"   ]);
			temp.base_stats[StatsType.sp_atk ] = Convert.ToInt16(row["base_spatk" ]);
			temp.base_stats[StatsType.sp_def ] = Convert.ToInt16(row["base_spdef" ]);
			temp.base_stats[StatsType.hp     ] = Convert.ToInt16(row["base_hp"    ]);
			temp.base_stats[StatsType.speed  ] = Convert.ToInt16(row["base_speed" ]);
			
			temp.max_stats [StatsType.atk    ] = Convert.ToInt16(row["max_atk"   ]);
			temp.max_stats [StatsType.def    ] = Convert.ToInt16(row["max_def"   ]);
			temp.max_stats [StatsType.sp_atk ] = Convert.ToInt16(row["max_spatk" ]);
			temp.max_stats [StatsType.sp_def ] = Convert.ToInt16(row["max_spdef" ]);
			temp.max_stats [StatsType.hp     ] = Convert.ToInt16(row["max_hp"    ]);
			temp.max_stats [StatsType.speed  ] = Convert.ToInt16(row["max_speed" ]);
			
			SPECIES[temp.id] = temp;
		}
		
		foreach(Dictionary<string,string> row in level_move_defs) {
		    LevelMove temp = new LevelMove();
		    temp.level = Convert.ToByte(row["level"]);
		    temp.move_id = Convert.ToInt32(row["move_id"]);
		    int species_id = Convert.ToInt32(row["species_id"]);
		    SPECIES[species_id].level_moves.Add(temp);
		}
		
		foreach(Dictionary<string,string> row in evolution_defs) {
			int from_id = Convert.ToInt32(row["from_species"]);
			int to_id = Convert.ToInt32(row["to_species"]);
			Evolution temp = new Evolution {
				species_id = to_id,
				species = SPECIES[to_id],
				type = (EvolutionType)Convert.ToByte(row["type"]),
				value = Convert.ToInt32(row["value"]),
			};
		    SPECIES[from_id].evolutions.Add(temp);
		}
		return SPECIES.Count;
	}
	public static void link() {
	    foreach (Species temp in SPECIES.Values) {
			if (temp.ability_id1   != 0) temp.ability1   = Ability.ABILITIES   [ temp.ability_id1   ];
            if (temp.ability_id2   != 0) temp.ability2   = Ability.ABILITIES   [ temp.ability_id2   ];
            if (temp.type_id1      != 0) temp.type1      = Element.TYPES       [ temp.type_id1      ];
            if (temp.type_id2      != 0) temp.type2      = Element.TYPES       [ temp.type_id2      ];
            if (temp.egg_group_id1 != 0) temp.egg_group1 = EggGroup.EGG_GROUPS [ temp.egg_group_id1 ];
			if (temp.egg_group_id2 != 0) temp.egg_group2 = EggGroup.EGG_GROUPS [ temp.egg_group_id2 ];
			if (temp.wild_item_id  != 0) temp.wild_item  = Item.ITEMS          [ temp.wild_item_id  ];

            foreach (LevelMove lvmove in temp.level_moves) lvmove.move = Move.MOVES[lvmove.move_id];
            foreach (Evolution evol in temp.evolutions) 
                if (new[]{EvolutionType.Item,EvolutionType.Trade}.Contains(evol.type) && evol.value != 0) 
                    evol.item = Item.ITEMS[evol.value];
		    
		    
	    }
	}
	public override string ToString() {
		return name;
	}
}
}