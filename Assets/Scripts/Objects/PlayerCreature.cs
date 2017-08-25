using System;
using System.Collections.Generic;
public class PlayerCreature : Creature {
    public static Dictionary<int,PlayerCreature> PLAYER_CREATURES;
    
    public int exp;
    public short ot, secret_ot;
    public byte[] pp_max;
    public short[] evs;
    public short[] ivs;
    public short happiness;
    
    protected PlayerCreature(){}
	public static new void init(List<Dictionary<string,string>> player_creature_defs) {
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
    public static new void link() {
        foreach (PlayerCreature temp in PLAYER_CREATURES.Values) {
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
                    ""+c.stats[(int)StatsType.hp],
                    ""+c.stats[(int)StatsType.atk],
                    ""+c.stats[(int)StatsType.def],
                    ""+c.stats[(int)StatsType.sp_atk],
                    ""+c.stats[(int)StatsType.sp_def],
                    ""+c.stats[(int)StatsType.speed],
                    ""+c.evs[(int)StatsType.atk],
                    ""+c.evs[(int)StatsType.def],
                    ""+c.evs[(int)StatsType.sp_atk],
                    ""+c.evs[(int)StatsType.sp_def],
                    ""+c.evs[(int)StatsType.hp],
                    ""+c.evs[(int)StatsType.speed],
                    ""+c.ivs[(int)StatsType.atk],
                    ""+c.ivs[(int)StatsType.def],
                    ""+c.ivs[(int)StatsType.sp_atk],
                    ""+c.ivs[(int)StatsType.sp_def],
                    ""+c.ivs[(int)StatsType.hp],
                    ""+c.ivs[(int)StatsType.speed],
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
