using System;
using System.Collections.Generic;

public class Creature {
	public static Dictionary<int,Creature> CREATURES;
	public int id;
	public string nickname;
	public short hp;
	public byte level;
	public short[] stats;
	public bool[] misc_info;
	public int species_id, ability_id, held_item_id;
	public int[] move_ids;
    public byte nature_id;
    public byte[] pp_cur;
	
	public Species species;
	public Item held_item;
	public Move[] moves;
	public Ability ability;
	public Nature nature;
	
	public Creature(Species species, int level) {
        id = -1;
        this.species = species;
        this.nature = Nature.NATURES[Globals.RNG.Next() % Nature.NATURES.Count];
        
	}
	public short getStat(byte level, StatsType type) {
	    int base_stat = species.base_stats[(int)type] / level;
        int mod_pct = (100 + nature.stats_mod[(int)type])/100;
        int mod_stat = base_stat * mod_pct;
        return (short)mod_stat;
	}
	protected Creature(){}
	public static void init(List<Dictionary<string,string>> unique_creature_defs) {
		CREATURES = new Dictionary<int, Creature> (unique_creature_defs.Count);
		foreach (Dictionary<string,string> row in unique_creature_defs) {
			Creature temp = new Creature();
			temp.id = Convert.ToInt32(row["id"]);
			temp.nickname = row["nickname"];
			temp.level = Convert.ToByte(row["level"]);
			temp.misc_info = Sqlite.getBitsFromBlob(row["misc_info"]);
			temp.stats = new short[] {
			    Convert.ToInt16(row["atk"]),
			    Convert.ToInt16(row["def"]),
			    Convert.ToInt16(row["spatk"]),
			    Convert.ToInt16(row["spdef"]),
			    Convert.ToInt16(row["hp"]),
			    Convert.ToInt16(row["speed"]),
			};
			temp.hp = temp.stats[(int)StatsType.hp];
			temp.move_ids = new int[] {
			    Convert.ToInt32(row["move1_id"]),
			    Convert.ToInt32(row["move2_id"]),
			    Convert.ToInt32(row["move3_id"]),
			    Convert.ToInt32(row["move4_id"]),
			};
			temp.species_id = Convert.ToInt32(row["species"]);
			temp.ability_id = Convert.ToInt32(row["ability"]);
			temp.held_item_id = Convert.ToInt32(row["held_item"]);
			temp.nature_id = Convert.ToByte(row["nature"]);
			
			CREATURES[temp.id] = temp;
		}
	}
    public static void link() {
        foreach (Creature temp in CREATURES.Values) {
	        temp.species = Species.SPECIES[temp.species_id];
	        temp.held_item = Item.ITEMS[temp.held_item_id];
	        temp.ability = Ability.ABILITIES[temp.ability_id];
	        temp.nature = Nature.NATURES[temp.nature_id];
	        temp.moves = new Move[] {
	            Move.MOVES[temp.move_ids[0]],
	            Move.MOVES[temp.move_ids[1]],
	            Move.MOVES[temp.move_ids[2]],
	            Move.MOVES[temp.move_ids[3]],
	        };
	        temp.pp_cur = new byte[] { 
	            (temp.moves[0] != null) ? temp.moves[0].pp : (byte)0,
	            (temp.moves[1] != null) ? temp.moves[1].pp : (byte)0,
	            (temp.moves[2] != null) ? temp.moves[2].pp : (byte)0,
	            (temp.moves[3] != null) ? temp.moves[3].pp : (byte)0,
	        };
	    }
    }

}
