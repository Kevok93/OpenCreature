using System;
class Program {
	public static void Main(string[] args) {
		System.IO.Directory.SetCurrentDirectory("../../..");
		string directory = System.IO.Directory.GetCurrentDirectory();
		try {
		    SqliteModel mesh = SqliteModelReader.readMesh("testmodel.db");
		} catch (System.Security.SecurityException e) {
            Console.Error.WriteLine(e);
		}
		Console.Out.WriteLine(directory);
		Console.In.ReadLine();
	}
}