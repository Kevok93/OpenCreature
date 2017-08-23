//using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Creaturedb {
	private static bool init = false;
	private static Sqlite db;
	
	private Creaturedb(){}
	public static bool initialize() {
		if (init) return false;
		try {
			db = new Sqlite("creature.db", SqliteOpenOpts.SQLITE_OPEN_READONLY);

			Type.init(
				db["Select * from types"][0],
				db["Select * from type_bonus"][0]
			);
			Species.init (
				db ["Select * from species"][0],
				db ["Select * from level_moves"][0],
				db ["Select * from evolution"][0]
			);
			Move.init (db ["Select * from moves"][0]);
			Ability.init (db ["Select * from abilities"] [0]);
			Effect.init (db ["Select * form effects"] [0]);
			Nature.init (db ["Select * from natures"] [0]);
			EggGroup.init (db ["Select * from egg_groups"] [0]);
			ItemType.init (db ["Select * from item_type"] [0]);
			PlotFlag.init (db ["Select * from plot_flag"] [0]);

			db.Close();


		} catch (System.Exception e) {
			System.Console.Error.WriteLine (

				string.Format (
					"Error Serializing Creaturedb: %s\n%s\n%s\n%s", 
					e.ToString, 
					e.StackTrace, 
					e.Message, 
					e.HelpLink
				)
			);
			db.Close();
			return false;
		}
		return true;
	}
}