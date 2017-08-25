using NUnit.Framework;
using System;
using opencreature;

namespace Tests
{
	[TestFixture]
	public class SingleBattleTest
	{
		Trainer ash,gary,oak;
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
		}
		[Test]
		public void Setup_Correct() {
			new SingleBattle(ash, gary);
		}
		
		[Test]
		public void Setup_NoCreatures() {
			Assert.Throws<InvalidOperationException>(delegate {new SingleBattle(ash, oak);});
		}
		
		[Test]
		public void Attack_Correct() {
			var single_battle = new SingleBattle(ash, gary);
			var t1_c = single_battle.activeCreatures[0];
			var t2_c = single_battle.activeCreatures[1];
			
			single_battle.queueAttack(t1_c, t1_c.moves[0]);
			single_battle.queueAttack(t2_c, t2_c.moves[0]);
			single_battle.executeAttacks();
		}
		
		//todo: order shouldn't matter for queue		
		[Test]
		public void Attack_TooManyQueued() {
			var single_battle = new SingleBattle(ash, gary);
			var t1_c = single_battle.activeCreatures[0];
			single_battle.queueAttack(t1_c, t1_c.moves[0]);
			Assert.Throws<InvalidOperationException>(delegate {single_battle.queueAttack(t1_c, t1_c.moves[0]);});
		}
		
		[Test]
		public void Attack_OrderDoesntMatter() {
			var single_battle = new SingleBattle(ash, gary);
			var t1_c = single_battle.activeCreatures[0];
			var t2_c = single_battle.activeCreatures[1];
			
			single_battle.queueAttack(t1_c, t1_c.moves[0]);
			single_battle.queueAttack(t2_c, t2_c.moves[0]);
			single_battle.executeAttacks();
			
			single_battle.queueAttack(t2_c, t2_c.moves[0]);
			single_battle.queueAttack(t1_c, t1_c.moves[0]);
			single_battle.executeAttacks();
		}
				
		[Test]
		public void Attack_InvalidQueue() {
			var single_battle = new SingleBattle(ash, gary);
			var t1_c = single_battle.activeCreatures[0];
			var t2_c = single_battle.activeCreatures[1];
			
			//queue empty
			Assert.Throws<InvalidOperationException>(delegate {single_battle.executeAttacks();});
			
			//queue half-full
			single_battle.queueAttack(t1_c, t1_c.moves[0]);
			Assert.Throws<InvalidOperationException>(delegate {single_battle.executeAttacks();});
			
			//queue full: should succeed
			single_battle.queueAttack(t2_c, t2_c.moves[0]);
			single_battle.executeAttacks();
			
			//queue empty again
			Assert.Throws<InvalidOperationException>(delegate {single_battle.executeAttacks();});
		}
		
		
	}
}

