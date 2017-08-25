using System;
using System.IO;
using System.Linq;
using System.Reflection;

class Program {
	public static void Main(string[] args) {
        //cwd should be where the files are
        string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "/../../../";
        Console.Out.WriteLine("cd "+path);
        Directory.SetCurrentDirectory(path);
        Creaturedb.initialize();
	}
	
    public static void debug() {
        string directory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
		Console.Out.WriteLine("Running in "+directory);
        CheckAssembly.FindConflictingReferences(directory);
        Console.Out.WriteLine("CWD: "+ Directory.GetCurrentDirectory());
    }
}