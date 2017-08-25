using NUnit.Framework;
using System;

namespace Tests
{
	[TestFixture()]
	public class SingleBattleTest
	{
		Trainer ash,gary,oak;
		SingleBattle single_battle;
		DoubleBattle double_battle;
		
		[SetUp()]
		[Test()]
		public void Initialize ()
		{
			Creaturedb.initialize ();
			ash = new Trainer (
				new Creature[6] {
					new Creature (Species.SPECIES [1],10),
					new Creature (Species.SPECIES [1],10),
					null,
					null,
					null,
					null
				}, 
				new Item[6], 
				null,//TrainerStyle.TRAINER_STYLES[0],
				0,"victory_ash","ash",
				new bool[6]
			);
			gary = new Trainer (
				new Creature[6] {
					new Creature (Species.SPECIES [2],30),
					new Creature (Species.SPECIES [2],30),
					null,
					null,
					null,
					null
				},  
				new Item[6], 
				null,//TrainerStyle.TRAINER_STYLES[0],
				10,"victory_gary","gary",
				new bool[6]
			);
			oak = new Trainer (
				new Creature[6], 
				new Item[6], 
				null,//TrainerStyle.TRAINER_STYLES[0],
				10,"victory_oak","oak",
				new bool[6]
			);
			single_battle = new SingleBattle(ash, gary);
			Assert.Throws<InvalidOperationException>(delegate {new SingleBattle(ash, oak);});

		}
	}
}

