using NUnit.Framework;
using System;

namespace Tests
{
	[TestFixture]
	public class SingleBattleTest
	{
		Trainer ash,gary,oak;
		SingleBattle single_battle;
		//DoubleBattle double_battle;
		
		[SetUp]
		[Test]
		public void Initialize ()
		{
			Creaturedb.initialize ();
			ash = new Trainer (
				new Creature[6] {
					new Creature (Species.SPECIES [1],10),
					new Creature (Species.SPECIES [1],10),
					null,null,null,null
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
					null,null,null,null
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
		[Test]
		public void Attack() {
			var t1_c = single_battle.activeCreatures[0];
			var t2_c = single_battle.activeCreatures[1];
			single_battle.queueAttack(t1_c, t1_c.moves[0]);
			single_battle.queueAttack(t2_c, t2_c.moves[0]);
			Assert.Throws<InvalidOperationException>(delegate {single_battle.queueAttack(t2_c, t2_c.moves[0]);});
			
			single_battle.executeAttacks();
			Assert.Throws<InvalidOperationException>(delegate {single_battle.executeAttacks();});
		}
		
	}
}

