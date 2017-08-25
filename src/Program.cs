using System;
using System.IO;
using System.Linq;
using System.Reflection;

class Program {
	static log4net.ILog log;
	public static void Main(string[] args) {
		//Globals.SetupLogging();
		Globals.RNG.NextFloat();
		log = log4net.LogManager.GetLogger("Main");
		
		Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
		debugInfo();
        Creaturedb.initialize();
	    //debugCLIBattle();
	}
	
    public static void debugInfo() {
        string directory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
		log.Debug("Running in "+directory);
        //CheckAssembly.FindConflictingReferences(directory);
        log.Debug("CWD: "+ Directory.GetCurrentDirectory());
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
        			new Creature(Creature.UNIQUE_CREATURES[0]),
					new Creature(Creature.UNIQUE_CREATURES[0]),
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
<<<<<<< HEAD
                    "{0}\n{1}/{2}\n\n\t\t\t\t\t{3}\n\t\t\t\t\t{4}/{5}\n",
                    creature2.nickname,
                    creature2.hp,
                    creature2.stats[(int)StatsType.hp],
                    creature1.nickname,
                    creature1.hp,
                    creature1.stats[(int)StatsType.hp]
                ) + 
            	"\n".PadLeft(74,'=') +
                String.Format(
                    "A: {0,30}\tB: {1,30}\nC: {2,30}\tD: {3,30}\n",
                    creature1.moves[0],
                    creature1.moves[1],
                    creature1.moves[2],
                    creature1.moves[3]
                )
            );
            bad_choice:
            char getchar = (char)0;
            	
            while (!new char[] {'a','b','c','d','A','B','C','D','1','2','3','4','q','Q'}.Contains(getchar)) {
				int i = Console.In.Read();
				getchar = (char)((i==-1) ? 0 : i);
            }
            switch (getchar) {
            	case 'a':
            	case 'A':
            	case '1':
            		if (creature1.moves[0].moveDef != null) {
						battle.queueAttack(creature1, creature1.moves[0]);
						break;
            		} else  {
						Console.WriteLine("Move not defined");
						goto bad_choice;
            		}
            	case 'b':
            	case 'B':
            	case '2':
            		if (creature1.moves[1].moveDef != null) {
						battle.queueAttack(creature1, creature1.moves[1]);
						break;
            		} else  {
						Console.WriteLine("Move not defined");
						goto bad_choice;
            		}
            	case 'c':
            	case 'C':
            	case '3':
            		if (creature1.moves[2].moveDef != null) {
						battle.queueAttack(creature1, creature1.moves[2]);
						break;
            		} else  {
						Console.WriteLine("Move not defined");
						goto bad_choice;
            		}
            	case 'd':
            	case 'D':
            	case '4':
            		if (creature1.moves[3].moveDef != null) {
						battle.queueAttack(creature1, creature1.moves[3]);
						break;
            		} else  {
						Console.WriteLine("Move not defined");
						goto bad_choice;
            		}
				case 'q':
				case 'Q':
					return;
            }
			battle.queueAttack(creature2, creature2.moves[0]);
            Console.WriteLine("asdf");
        } while (!battle.executeAttacks());
        
        
        
=======
                    "{0}\n{1}/{2}\n",
                    creature2.nickname,
                    creature2.hp,
                    creature2.stats[(int)StatsType.hp]
                )
            );
        } while (false);//battle.executeAttacks());
>>>>>>> ccffb5f... Replacing the 'BetterEnum' class with a 'BetterEnumArray' class, allowing arrays to be indexed by enums.
    }
}