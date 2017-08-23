using System;
class Program {
	public static void Main(string[] args) {
	    //cwd should be where the files are
	    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "/../../../";
        Console.Out.WriteLine("cd "+path);
        System.IO.Directory.SetCurrentDirectory(path);
		string directory = System.IO.Directory.GetCurrentDirectory();
		Console.Out.WriteLine("Running in "+directory);
		
		
        //Sqlite db = new Sqlite("creature.db", SqliteOpenOpts.SQLITE_OPEN_READONLY);
        //Console.Out.WriteLine(Sqlite.printResult(db.getTable("Select * from moves;")));
        Creaturedb.initialize();
	}
}