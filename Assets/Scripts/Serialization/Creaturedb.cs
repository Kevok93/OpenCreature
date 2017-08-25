using System.Collections;
using System.Collections.Generic;
using System;

public class Creaturedb {
	private static bool init = false;
	private static SqliteConnection db;
	
	private Creaturedb(){}
	public static bool initialize() {
		if (init) return false;
		try {
			db = new SqliteConnection("creature.db", SqliteOpenOpts.SQLITE_OPEN_READONLY);
            Console.Out.WriteLine("Deserializing creature.db");
            
			Type.init(
				db["Select * from types"][0],
				db["Select * from type_bonus"][0]
			);
            Console.Out.WriteLine("Types loaded");
            
			Species.init (
				db ["Select * from species"][0],
				db ["Select * from level_moves"][0],
				db ["Select * from evolution"][0]
			);
            Console.Out.WriteLine("Species loaded");
            
			Move.init (db ["Select * from moves"][0]);
            Console.Out.WriteLine("Moves loaded");
            
			Ability.init (db ["Select * from abilities"] [0]);
            Console.Out.WriteLine("Abilities loaded");
            
			Effect.init (db ["Select * from effects"] [0]);
            Console.Out.WriteLine("Effects loaded");
            
			Nature.init (db ["Select * from natures"] [0]);
            Console.Out.WriteLine("Natures loaded");
            
			EggGroup.init (db ["Select * from egg_groups"] [0]);
            Console.Out.WriteLine("Egg Types loaded");
            
			ItemType.init (db ["Select * from item_type"] [0]);
            Console.Out.WriteLine("Item Types loaded");
            
			PlotFlag.init (db ["Select * from plot_flag"] [0]);
            Console.Out.WriteLine("Plot Flags loaded");
            
			Npc.init (db ["Select * from npc"] [0]);
            Console.Out.WriteLine("NPCs loaded");
            
            init = true;
		} catch (Exception e) {
            Console.Error.WriteLine(
                string.Format(
                    "Error Serializing Creaturedb: \n{0}", 
                    string.Join("\n",new string[] { e.ToString(), e.HelpLink })
				)
			);
			return false;
		}
		return true;
	}
}