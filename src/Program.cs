using System;
using System.IO;
using System.Linq;
using System.Reflection;

class Program {
	const string PREFIX = "MAIN";
	public static void Main(string[] args) {
		Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
		debugInfo();
        Creaturedb.initialize();
	    debugCLIBattle();
	}
	
    public static void debugInfo() {
        string directory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
		Console.Out.WriteWithPrefix("Running in "+directory, PREFIX);
        //CheckAssembly.FindConflictingReferences(directory);
        Console.Out.WriteWithPrefix("CWD: "+ Directory.GetCurrentDirectory(), PREFIX);
    }

    public static void debugTests() {
        var sbt = new Tests.SingleBattleTest();
        sbt.Initialize();
        sbt.Attack();
    }

    public static void debugCLIBattle() {
        Creaturedb.initialize();
        var ash = new Trainer(
            new Creature[6] {
					new Creature (Species.SPECIES [1],10),
					new Creature (Species.SPECIES [1],10),
					null,null,null,null
				},
            new Item[6],
            null,//TrainerStyle.TRAINER_STYLES[0],
            0, "victory_ash", "ash",
            new bool[6]
        );
        var gary = new Trainer(
            new Creature[6] {
					new Creature (Species.SPECIES [2],30),
					new Creature (Species.SPECIES [2],30),
					null,null,null,null
				},
            new Item[6],
            null,//TrainerStyle.TRAINER_STYLES[0],
            10, "victory_gary", "gary",
            new bool[6]
        );
        var battle = new SingleBattle(ash, gary);

        do {
            var creature1 = battle.activeCreatures[0];
            var creature2 = battle.activeCreatures[1];
            Console.WriteLine(
                String.Format(
                    "{0}\n{1}/{2}\n",
                    creature2.nickname,
                    creature2.hp,
                    creature2.stats[(int)StatsType.hp]
                )
            );
        } while (false);//battle.executeAttacks());
    }
}