using System;
using System.Collections.Generic;
public class PlayerCreature : Creature {
    public static Dictionary<int,PlayerCreature> PLAYER_CREATURES;
    
    public int exp;
    public short ot, secret_ot;
<<<<<<< HEAD
    public short[] evs;
    public short[] ivs;
=======
	public BetterEnumArray<StatsType,short> evs;
	public BetterEnumArray<StatsType,short> ivs;
>>>>>>> ccffb5f... Replacing the 'BetterEnum' class with a 'BetterEnumArray' class, allowing arrays to be indexed by enums.
	private byte[] pp_cur, pp_max;
    public short happiness;
    
    protected PlayerCreature(){}
    public PlayerCreature(Creature caughtCreature) : base(caughtCreature) {
        //ot = Globals.CURRENT_PLAYER.ot; TODO
        //secret_ot = Globals.CURRENT_PLAYER.secret_ot; TODO
        evs = new short[] { 0, 0, 0, 0, 0, 0 };
        ivs = new short[] { 0, 0, 0, 0, 0, 0 };
        happiness = species.base_happiness;
        exp = 0; //TODO are we truncating or aggregating?
    }
	public static void init(List<Dictionary<string,string>> player_creature_defs) {
		PLAYER_CREATURES = new Dictionary<int, PlayerCreature> (player_creature_defs.Count);
		foreach (Dictionary<string,string> row in player_creature_defs) {
			PlayerCreature temp = new PlayerCreature();
			temp.id = Convert.ToInt32(row["id"]);
			temp.nickname = row["nickname"];
			temp.ot = Convert.ToInt16(row["ot"]);
			temp.secret_ot = Convert.ToInt16(row["secret_ot"]);
			temp.hp = Convert.ToInt16(row["hp_cur"]);
			temp.exp = Convert.ToByte(row["exp"]);
			temp.level = Convert.ToByte(row["level"]);
			temp.misc_info = SqliteConnection.getBitsFromBlob(row["misc_info"]);
			temp.stats = new short[] {
			    Convert.ToInt16(row["atk"]),
			    Convert.ToInt16(row["def"]),
			    Convert.ToInt16(row["spatk"]),
			    Convert.ToInt16(row["spdef"]),
			    Convert.ToInt16(row["hp"]),
			    Convert.ToInt16(row["speed"]),
			};
			temp.ivs = new short[] {
			    Convert.ToInt16(row["iv_atk"]),
			    Convert.ToInt16(row["iv_def"]),
			    Convert.ToInt16(row["iv_spatk"]),
			    Convert.ToInt16(row["iv_spdef"]),
			    Convert.ToInt16(row["iv_hp"]),
			    Convert.ToInt16(row["iv_speed"]),
			};
			temp.evs = new short[] {
			    Convert.ToInt16(row["ev_atk"]),
			    Convert.ToInt16(row["ev_def"]),
			    Convert.ToInt16(row["ev_spatk"]),
			    Convert.ToInt16(row["ev_spdef"]),
			    Convert.ToInt16(row["ev_hp"]),
			    Convert.ToInt16(row["ev_speed"]),
			};
			temp.move_ids = new int[] {
			    Convert.ToInt32(row["move1_id"]),
			    Convert.ToInt32(row["move2_id"]),
			    Convert.ToInt32(row["move3_id"]),
			    Convert.ToInt32(row["move4_id"]),
			};
			temp.pp_cur = new byte[] {
			    Convert.ToByte(row["move1_pp_cur"]),
			    Convert.ToByte(row["move2_pp_cur"]),
			    Convert.ToByte(row["move3_pp_cur"]),
			    Convert.ToByte(row["move4_pp_cur"]),
			};
			temp.pp_max = new byte[] {
			    Convert.ToByte(row["move1_pp_max"]),
			    Convert.ToByte(row["move2_pp_max"]),
			    Convert.ToByte(row["move3_pp_max"]),
			    Convert.ToByte(row["move4_pp_max"]),
			};
			temp.species_id = Convert.ToInt32(row["species"]);
			temp.ability_id = Convert.ToInt32(row["ability"]);
			temp.held_item_id = Convert.ToInt32(row["held_item"]);
			temp.nature_id = Convert.ToByte(row["nature"]);
			
			PLAYER_CREATURES[temp.id] = temp;
		}
	}
    public static void link() {
        foreach (PlayerCreature temp in PLAYER_CREATURES.Values) {
	        temp.species = Species.SPECIES[temp.species_id];
	        temp.held_item = Item.ITEMS[temp.held_item_id];
	        temp.ability = Ability.ABILITIES[temp.ability_id];
	        temp.nature = Nature.NATURES[temp.nature_id];
	        temp.moves = new LearnedMove[] {
	        	new LearnedMove(Move.MOVES[temp.move_ids[0]],temp.pp_max[0],temp.pp_cur[0]),
	        	new LearnedMove(Move.MOVES[temp.move_ids[1]],temp.pp_max[1],temp.pp_cur[1]),
	        	new LearnedMove(Move.MOVES[temp.move_ids[2]],temp.pp_max[2],temp.pp_cur[2]),
	        	new LearnedMove(Move.MOVES[temp.move_ids[3]],temp.pp_max[3],temp.pp_cur[3]),
	        };
	    }
    }
    public static string saveSQL() {
        int savefile = 3;
        //Playerdb.SAVE_FILE;
        string sql = "DELETE * FROM creatures WHERE save_slot = " + savefile + ";\n";
        foreach (PlayerCreature c in PLAYER_CREATURES.Values) {
            sql += String.Format(
                "INSERT INTO creatures VALUES ({0});\n",
                String.Join(",",new string[]{
                    ""+savefile,
                    ""+c.id,
                    ""+c.species_id,
                    "'"+c.nickname+"'",
                    ""+c.ability_id,
                    ""+c.nature_id,
                    ""+c.exp,
                    ""+c.level,
                    ""+c.move_ids[0],
                    ""+c.pp_cur[0],
                    ""+c.pp_max[0],
                    ""+c.move_ids[1],
                    ""+c.pp_cur[1],
                    ""+c.pp_max[1],
                    ""+c.move_ids[2],
                    ""+c.pp_cur[2],
                    ""+c.pp_max[2],
                    ""+c.move_ids[3],
                    ""+c.pp_cur[3],
                    ""+c.pp_max[3],
                    ""+c.hp,
                    ""+c.stats[StatsType.hp],
                    ""+c.stats[StatsType.atk],
                    ""+c.stats[StatsType.def],
                    ""+c.stats[StatsType.sp_atk],
                    ""+c.stats[StatsType.sp_def],
                    ""+c.stats[StatsType.speed],
                    ""+c.evs[StatsType.atk],
                    ""+c.evs[StatsType.def],
                    ""+c.evs[StatsType.sp_atk],
                    ""+c.evs[StatsType.sp_def],
                    ""+c.evs[StatsType.hp],
                    ""+c.evs[StatsType.speed],
                    ""+c.ivs[StatsType.atk],
                    ""+c.ivs[StatsType.def],
                    ""+c.ivs[StatsType.sp_atk],
                    ""+c.ivs[StatsType.sp_def],
                    ""+c.ivs[StatsType.hp],
                    ""+c.ivs[StatsType.speed],
<<<<<<< HEAD
>>>>>>> ccffb5f... Replacing the 'BetterEnum' class with a 'BetterEnumArray' class, allowing arrays to be indexed by enums.
=======
>>>>>>> b16250a... Missed a couple of diff tags
                    ""+c.happiness,
                    ""+SqliteConnection.getBlobFromBits(c.misc_info)
                }
            ));
        }
        return sql;
    }	
    public bool readyForLevelEvolution() {
        foreach (Evolution e in species.evolutions) {
            if (
                (e.type == EvolutionType.Level && e.value >= level) ||
                (e.type == EvolutionType.Happiness && e.value >= happiness)
               ) return true;
        }
	    return false;
    }
}
