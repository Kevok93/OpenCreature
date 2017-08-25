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
            Console.Out.WriteLine("CREATURE.DB:\tDeserializing creature.db");
            
			Type.init(
				db["SELECT * FROM types"][0],
				db["SELECT * FROM type_bonus"][0]
			);
            Console.Out.WriteLine("CREATURE.DB:\tTypes loaded");
            
			Species.init (
				db ["SELECT * FROM species;"][0],
				db ["SELECT * FROM level_moves"][0],
				db ["SELECT * FROM evolution"][0]
			);
            Console.Out.WriteLine("CREATURE.DB:\tSpecies loaded");
            
			Move.init (db ["SELECT * FROM moves"][0]);
            Console.Out.WriteLine("CREATURE.DB:\tMoves loaded");
            
			Ability.init (db ["SELECT * FROM abilities"] [0]);
            Console.Out.WriteLine("CREATURE.DB:\tAbilities loaded");
            
			Effect.init (db ["SELECT * FROM effects"] [0]);
            Console.Out.WriteLine("CREATURE.DB:\tEffects loaded");
            
			Nature.init (db ["SELECT * FROM natures"] [0]);
            Console.Out.WriteLine("CREATURE.DB:\tNatures loaded");
            
			EggGroup.init (db ["SELECT * FROM egg_groups"] [0]);
            Console.Out.WriteLine("CREATURE.DB:\tEgg Types loaded");
            
			ItemType.init (db ["SELECT * FROM item_type"] [0]);
            Console.Out.WriteLine("CREATURE.DB:\tItem Types loaded");
            
			PlotFlag.init (db ["SELECT * FROM plot_flag"] [0]);
            Console.Out.WriteLine("CREATURE.DB:\tPlot Flags loaded");
            
			Npc.init (db ["SELECT * FROM npc"] [0]);
            Console.Out.WriteLine("CREATURE.DB:\tNPCs loaded");
            
            init = true;
		} catch (Exception e) {
            Console.Error.WriteLine(
                string.Format(
                    "CREATURE.DB:\tError Serializing Creaturedb: \n{0}", 
                    string.Join("\n",new string[] { e.ToString(), e.HelpLink })
				)
			);
			return false;
		}
		return true;
	}
}