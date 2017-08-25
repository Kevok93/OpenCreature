using System.Collections;
using System.Collections.Generic;
using System;

public class Creaturedb {
	const string PREFIX = "CREATURE.DB";
	private static bool init = false;
	private static SqliteConnection db;
	
	private Creaturedb(){}
	public static bool initialize() {
		if (init) return false;
		try {
			db = new SqliteConnection("creature.db", SqliteOpenOpts.SQLITE_OPEN_READONLY);
            Console.Out.WriteWithPrefix("Deserializing creature.db",PREFIX);
            
			Type.init(
				db["SELECT * FROM types"][0],
				db["SELECT * FROM type_bonus"][0]
			);
            Console.Out.WriteWithPrefix("Types loaded",PREFIX);
            
			Species.init (
				db ["SELECT * FROM species;"][0],
				db ["SELECT * FROM level_moves"][0],
				db ["SELECT * FROM evolution"][0]
			);
            Console.Out.WriteWithPrefix("Species loaded",PREFIX);
            
			Move.init (db ["SELECT * FROM moves"][0]);
            Console.Out.WriteWithPrefix("Moves loaded",PREFIX);
            
			Ability.init (db ["SELECT * FROM abilities"] [0]);
            Console.Out.WriteWithPrefix("Abilities loaded",PREFIX);
            
			Effect.init (db ["SELECT * FROM effects"] [0]);
            Console.Out.WriteWithPrefix("Effects loaded",PREFIX);
            
			Nature.init (db ["SELECT * FROM natures"] [0]);
            Console.Out.WriteWithPrefix("Natures loaded",PREFIX);
            
			EggGroup.init (db ["SELECT * FROM egg_groups"] [0]);
            Console.Out.WriteWithPrefix("Egg Types loaded",PREFIX);
            
			ItemType.init (db ["SELECT * FROM item_type"] [0]);
            Console.Out.WriteWithPrefix("Item Types loaded",PREFIX);
            
			PlotFlag.init (db ["SELECT * FROM plot_flag"] [0]);
            Console.Out.WriteWithPrefix("Plot Flags loaded",PREFIX);
            
			Npc.init (db ["SELECT * FROM npc"] [0]);
            Console.Out.WriteWithPrefix("NPCs loaded",PREFIX);
            
            init = true;
			Console.Out.WriteWithPrefix("Creature.db fully deserialized!",PREFIX);
		} catch (Exception e) {
            Console.Error.WriteWithPrefix(
                string.Format(
                    "Error Deserializing Creaturedb: \n{0}", 
                    string.Join("\n",new string[] { e.ToString(), e.HelpLink })
				), PREFIX
			);
			return false;
		}
		return true;
	}
}