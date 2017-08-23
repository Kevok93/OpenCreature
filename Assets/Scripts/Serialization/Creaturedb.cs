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
            System.Console.Out.WriteLine("DB open");
            var asdf = db ["Select * from moves"][0];
			/*Type.init(
				db["Select * from types"][0],
				db["Select * from type_bonus"][0]
			);
            System.Console.Out.WriteLine("Types written");
			Species.init (
				db ["Select * from species"][0],
				db ["Select * from level_moves"][0],
				db ["Select * from evolution"][0]
			);
            System.Console.Out.WriteLine("Species written");
			Move.init (db ["Select * from moves"][0]);
            System.Console.Out.WriteLine("Moves written");
			Ability.init (db ["Select * from abilities"] [0]);
            System.Console.Out.WriteLine("Abilities written");
			Effect.init (db ["Select * from effects"] [0]);
            System.Console.Out.WriteLine("Effects written");
			Nature.init (db ["Select * from natures"] [0]);
            System.Console.Out.WriteLine("Natures written");
			EggGroup.init (db ["Select * from egg_groups"] [0]);
            System.Console.Out.WriteLine("Egg Types written");
			ItemType.init (db ["Select * from item_type"] [0]);
            System.Console.Out.WriteLine("Item Types written");
			PlotFlag.init (db ["Select * from plot_flag"] [0]);
            System.Console.Out.WriteLine("Plot Flags written");
*/
            init = true;
			db.Close();


		} catch (System.Exception e) {
            System.Console.Error.WriteLine(

                string.Format(
                    "Error Serializing Creaturedb: \n{0}", 
                    string.Join("\n",new string[] { e.ToString(), e.HelpLink })
				)
			);
		    if (db != null) db.Close();
			return false;
		}
		return true;
	}
}