using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
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
			var t1_c = single_battle.battleSlots[1].activeCreature;
			var t2_c = single_battle.battleSlots[2].activeCreature;
			
			single_battle.queueAttack(t1_c, t1_c.moves[0]);
			single_battle.queueAttack(t2_c, t2_c.moves[0]);
			single_battle.executeAttackQueue();
			Assert.IsEmpty(single_battle.attackQueue);
		}
		
		//todo: order shouldn't matter for queue		
		[Test]
		public void Attack_TooManyQueued() {
			var single_battle = new SingleBattle(ash, gary);
			var t1_c = single_battle.battleSlots[1].activeCreature;
			single_battle.queueAttack(t1_c, t1_c.moves[0]);
			Assert.Throws<InvalidOperationException>(delegate {single_battle.queueAttack(t1_c, t1_c.moves[0]);});
		}
		
		[Test]
		public void Attack_OrderDoesntMatter() {
			var single_battle = new SingleBattle(ash, gary);
			var t1_c = single_battle.battleSlots[1].activeCreature;
			var t2_c = single_battle.battleSlots[2].activeCreature;
			
			single_battle.queueAttack(t1_c, t1_c.moves[0]);
			single_battle.queueAttack(t2_c, t2_c.moves[0]);
			single_battle.executeAttackQueue();
			
			single_battle.queueAttack(t2_c, t2_c.moves[0]);
			single_battle.queueAttack(t1_c, t1_c.moves[0]);
			single_battle.executeAttackQueue();
		}
				
		[Test]
		public void Attack_InvalidQueue() {
			var single_battle = new SingleBattle(ash, gary);
			var t1_c = single_battle.battleSlots[1].activeCreature;
			var t2_c = single_battle.battleSlots[2].activeCreature;
			
			//queue empty
			Assert.Throws<InvalidOperationException>(delegate {single_battle.executeAttackQueue();});
			
			//queue half-full
			single_battle.queueAttack(t1_c, t1_c.moves[0]);
			Assert.Throws<InvalidOperationException>(delegate {single_battle.executeAttackQueue();});
			
			//queue full: should succeed
			single_battle.queueAttack(t2_c, t2_c.moves[0]);
			single_battle.executeAttackQueue();
			
			
			//queue empty again
			Assert.Throws<InvalidOperationException>(delegate {single_battle.executeAttackQueue();});
		}
		
		
		[Test]
		public void Target_Test() {
			var targetList = new TargetList(
				"target_all",
				@"foreach team_slots in teams do
					foreach slot_id in team_slots do
						targets.Add(slot_id)
					end
				end"
			);
			
			List<List<Byte>> teams     = new List<List<byte>>{new List<Byte>{0}, new List<Byte>{1}};
			List<List<Byte>> alliances = new List<List<byte>>{new List<Byte>( ), new List<Byte>( )};
			List<Byte> targetSlotsMono = new List<Byte>();
			{
				List<List<byte>>.Enumerator teamEnum = teams.GetEnumerator();
				while (teamEnum.MoveNext()) {
					List<byte> slots = teamEnum.Current;
					List<byte>.Enumerator slotEnum = slots.GetEnumerator();
					while (slotEnum.MoveNext()) { targetSlotsMono.Add(slotEnum.Current); }
				}
			}
			List<Byte> targetSlotsLua = targetList.getTargets(0, teams, alliances).ToList();
			Assert.AreEqual(new List<Byte> {0, 1}, targetSlotsMono);
			Assert.AreEqual(targetSlotsMono, targetSlotsLua);
		}
		
		
	}
}

