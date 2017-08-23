using System;
class Program {
	public static void Main(string[] args) {
		System.IO.Directory.SetCurrentDirectory("../../..");
		string directory = System.IO.Directory.GetCurrentDirectory();
		UnityEngine.Mesh mesh = SqliteModelReader.readMesh("testmesh.db");
		Console.Out.WriteLine(directory);
		Console.In.ReadLine();
	}
}