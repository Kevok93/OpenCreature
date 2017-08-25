using System;
using System.Collections.Generic;
using System.Linq; 

namespace opencreature {
	public class Creature : DeserializedElement {

	public static TypeCastDictionary<int,DeserializedElement> UNIQUE_CREATURES;
	public string nickname;
	public short hp;
	public byte level;
	public BetterEnumArray<StatsType,short> stats;
	public BetterEnumArray<UniqueCreatureData,bool> misc_info;
	public int species_id, ability_id, held_item_id;
	public int[] move_ids;
    public byte nature_id;
	
	public Species species;
	public Item held_item;
	public LearnedMove[] moves;
	public Ability ability;
	public Nature nature;
	
	public Creature(Species species, int level) {
        id = -1;
        
        this.species = species;
		this.species_id = species.id;
		
        this.nature = Nature.NATURES[Globals.RNG.Next() % Nature.NATURES.Count];
        this.level = (byte)level;
        
        this.stats = new short[6];
	    for (int i = 0; i < 6; i++)
	        this.stats[i] = getStat((byte) level, (StatsType) i);
	    this.hp = stats[StatsType.hp];
<<<<<<< HEAD
>>>>>>> ccffb5f... Replacing the 'BetterEnum' class with a 'BetterEnumArray' class, allowing arrays to be indexed by enums.
=======
>>>>>>> b16250a... Missed a couple of diff tags
		
		List<Move> availableMoves = new List<Move>(species.level_moves.Count);
		foreach(LevelMove lm in species.level_moves) {
			if (lm.level <= level) availableMoves.Add(lm.move);
		}
		availableMoves.Randomize();
		this.moves = new LearnedMove[4];
		for (int i = 0; i < 3; i++) {
			Move m = (availableMoves.Count > 0) ? availableMoves[0] : null;
			if (m == null) break;
			availableMoves.RemoveAt(0);
			this.moves[i] = new LearnedMove(m);
		}
		this.move_ids = new int[] {
			(moves[0].moveDef != null) ? moves[0].moveDef.id : 0,
			(moves[1].moveDef != null) ? moves[1].moveDef.id : 0,
			(moves[2].moveDef != null) ? moves[2].moveDef.id : 0,
			(moves[3].moveDef != null) ? moves[3].moveDef.id : 0,
		};
		
		this.ability = (Globals.RNG.Next()%2 == 1) ? species.ability1 : species.ability2;
		this.ability_id = ability.id;
		
		if (Globals.RNG.Next() % 255 < species.wild_item_pct) { 
			this.held_item = species.wild_item;
			this.held_item_id = species.wild_item_id;
		} else {
			held_item = null;
			held_item_id = 0;
		}
		
		this.nickname = species.name;
	}
	public short getStat(byte level, StatsType type) {
	    int base_stat = species.base_stats[type] * level / 50;
<<<<<<< HEAD
        int mod_pct = (100 + nature.stats_mod[type])/100;
        int mod_stat = (base_stat+5) * mod_pct;
<<<<<<< HEAD
>>>>>>> ccffb5f... Replacing the 'BetterEnum' class with a 'BetterEnumArray' class, allowing arrays to be indexed by enums.
=======
>>>>>>> b16250a... Missed a couple of diff tags
=======
        float mod_pct = (100f + nature.stats_mod[type])/100f;
        int mod_stat = (int)((base_stat+5) * mod_pct);
>>>>>>> 78b9222... Adding changes that were lost during the merge (?)
        return (short)mod_stat;
	}
	protected Creature(){}
	public Creature(Creature caughtCreature){
        id = caughtCreature.id;
        nickname = caughtCreature.nickname;
        hp = caughtCreature.hp;
        level = caughtCreature.level;
        stats = caughtCreature.stats;
        misc_info = caughtCreature.misc_info;
        species_id = caughtCreature.species_id;
        species = caughtCreature.species;
        ability_id = caughtCreature.ability_id;
        ability = caughtCreature.ability;
        held_item_id = caughtCreature.held_item_id;
        held_item = caughtCreature.held_item;
        move_ids = caughtCreature.move_ids;
        moves = caughtCreature.moves;
        nature_id = caughtCreature.nature_id;
        nature = caughtCreature.nature;
	}
	public static long init_unique(List<Dictionary<string,string>> unique_creature_defs) {
		UNIQUE_CREATURES = new TypeCastDictionary<int,DeserializedElement> 
			(typeof(Creature),unique_creature_defs.Count);
		foreach (Dictionary<string,string> row in unique_creature_defs) {
			Creature temp = new Creature();
			temp.id = Convert.ToInt32(row["id"]);
			temp.nickname = row["nickname"];
			temp.level = Convert.ToByte(row["level"]);
			temp.misc_info = SqliteConnection.getBitsFromBlob(row["misc_info"]);
            temp.hp = Convert.ToInt16(row["hp_max"]);
			temp.stats = new short[] {
			    Convert.ToInt16(row["atk"]),
			    Convert.ToInt16(row["def"]),
			    Convert.ToInt16(row["spatk"]),
			    Convert.ToInt16(row["spdef"]),
			    Convert.ToInt16(row["hp_max"]),
			    Convert.ToInt16(row["speed"]),
			};
			temp.hp = temp.stats[StatsType.hp];
			temp.move_ids = new int[] {
			    Convert.ToInt32(row["move1_id"]),
			    Convert.ToInt32(row["move2_id"]),
			    Convert.ToInt32(row["move3_id"]),
			    Convert.ToInt32(row["move4_id"]),
			};
			temp.species_id = Convert.ToInt32(row["species_id"]);
			temp.ability_id = Convert.ToInt32(row["ability"]);
			temp.held_item_id = Convert.ToInt32(row["held_item"]);
			temp.nature_id = Convert.ToByte(row["nature"]);
			
			UNIQUE_CREATURES[temp.id] = temp;
		}
		return UNIQUE_CREATURES.Count;
	}
    public static void link_unique() {
        foreach (Creature temp in UNIQUE_CREATURES.Values) {
	        temp.species = Species.SPECIES[temp.species_id];
	        temp.held_item = Item.ITEMS[temp.held_item_id];
	        temp.ability = Ability.ABILITIES[temp.ability_id];
	        temp.nature = Nature.NATURES[temp.nature_id];
	        temp.moves = new LearnedMove[] {
	        	(new LearnedMove(Move.MOVES[temp.move_ids[0]])),
	        	(new LearnedMove(Move.MOVES[temp.move_ids[1]])),
	        	(new LearnedMove(Move.MOVES[temp.move_ids[2]])),
	        	(new LearnedMove(Move.MOVES[temp.move_ids[3]])),
	        };
	    }
    }
	public override string ToString() {
		return String.Format("{0} lv{1} ({2})", nickname, level, species.name);
	}

}
}