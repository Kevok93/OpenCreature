using System;
class Program {
	public static void Main(string[] args) {
		System.IO.Directory.SetCurrentDirectory("../../..");
		string directory = System.IO.Directory.GetCurrentDirectory();
		Console.Out.WriteLine("Running in "+directory);
        //Sqlite db = new Sqlite("creature.db", SqliteOpenOpts.SQLITE_OPEN_READONLY);
        //Console.Out.WriteLine(Sqlite.printResult(db.getTable("Select * from moves;")));
        Creaturedb.initialize();
	}
}