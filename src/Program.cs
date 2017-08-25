using System;
using System.IO;
using System.Linq;
using System.Reflection;

class Program {
	const string PREFIX = "MAIN";
	public static void Main(string[] args) {
		Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
		debug();
        Creaturedb.initialize();
	}
	
    public static void debug() {
        string directory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
		Console.Out.WriteWithPrefix("Running in "+directory, PREFIX);
        //CheckAssembly.FindConflictingReferences(directory);
        Console.Out.WriteWithPrefix("CWD: "+ Directory.GetCurrentDirectory(), PREFIX);
    }
}