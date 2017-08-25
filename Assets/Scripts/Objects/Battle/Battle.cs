using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;
using UnityEditor;

namespace opencreature {
public abstract class Battle {
    public Dictionary<int,BattleSlot> battleSlots;
    public Dictionary<int,List<BattleSlot>> teamAssignment;
	public SortedSet<Attack> attackQueue;
	protected Battle() {
	}
	
	public class BattleSlot {
		public Creature activeCreature;
		public Trainer slotOwner;
	}
	
		
	public int checkVictory() {
		foreach (int teamId in teamAssignment.Keys) {
			bool enemyTeamAlive = teamAssignment
				.Where(pair => pair.Key != teamId)
				.Select(pair => checkTeamAlive(pair.Key))
				.Any(x => x);
			if (!enemyTeamAlive) return teamId;
		}
		return 0;
	}
	
	public bool startupCheck() {
		foreach (KeyValuePair<int,BattleSlot> slot in battleSlots.AsEnumerable()) {
			if (slot.Value == null) {
				throw new System.InvalidOperationException(String.Format(
					"Trainer {0} entered a battle with no valid creatures!",slot.Key
				));
			}
		}
		return true;
	}
	
	public bool checkTeamAlive(int teamId) {
		return teamAssignment[teamId].Any(slot=>slot.activeCreature != null);
	}
	
	public virtual Attack queueAttack(Creature c, LearnedMove m, sbyte target = -1) {
			
		if (attackQueue.Any(attack => attack.attacker == c))
			throw new System.InvalidOperationException("Creature has already queued an attack!");
		
		Attack q_attack = new Attack() {
			attacker = c,
			usedMove = m,
			speed = c.stats [StatsType.speed],
		};
		attackQueue.Add(q_attack);
		return q_attack;
	}
	
//	public void switchCreature(int activeCreatureIndex, int trainerCreatureIndex) {
//		activeCreatures[activeCreatureIndex].Item2 = activeCreatures[activeCreatureIndex].Item1.getNextCreature();
//	}
	
	public virtual void replaceCreature(int index) {
		//activeCreatures[index] = trainers[index].getNextCreature();
	}
	
	public bool executeAttackQueue() {
		if (attackQueue.Count() != battleSlots.Values.Count(slot => slot.activeCreature != null))
			throw new System.InvalidOperationException("Executing attacks before all creatures are ready!");
		
		Attack attack;
		while ((attack = attackQueue.Dequeue()) != null) {
        	attack.execute(battleSlots);
        	checkForDeath();
		}
		return false;
	}
	
<<<<<<< HEAD
	public bool attack(Creature attacker, LearnedMove usedMove, Creature target) {
<<<<<<< HEAD
		if (usedMove.moveDef.power > 0) {
			float crit_chance = .85f; //TODO: Crit bonuses
			float crit_bonus = Globals.RNG.NextDouble() > crit_chance ? 2f : 1f;
			float type_bonus = 
				Element.getTypeBonus(usedMove.moveDef.type_id, target.species.type_id1) *
				Element.getTypeBonus(usedMove.moveDef.type_id, target.species.type_id2);
			float power_mod = 1f; //TODO: mod power
			float stab_bonus = (
                usedMove.moveDef.movetype == attacker.species.type1 ||
                usedMove.moveDef.movetype == attacker.species.type2
            ) ? 1.5f : 1f;
			float atk_def_ratio = 0f;
			if (usedMove.moveDef.affinity != MoveAffinity.Other) atk_def_ratio = 
				(float)attacker.stats[usedMove.moveDef.atkAffinity] /
				(float)target.stats[usedMove.moveDef.defAffinity];
			float base_damage = (
                usedMove.moveDef.power
                * atk_def_ratio
                * ((2 * attacker.level + 10) / 250f)
            ) + 2;
			float mod_damage = 
				stab_bonus *
				type_bonus *
				crit_bonus *
				power_mod *
				(Globals.RNG.NextFloat() * .15f + .85f);
			short damage = (short)(base_damage * mod_damage);
			System.Console.WriteLine(System.String.Format("{0} did {1} damage to {2}", attacker, damage, target));
			target.hp -= damage;
		}
=======
		float crit_chance = .85f; //TODO: Crit bonuses
		float crit_bonus = Globals.RNG.NextDouble() > crit_chance ? 2f : 1f;
		float type_bonus = 
			Type.getTypeBonus(usedMove.moveDef.type_id, target.species.type_id1) *
			Type.getTypeBonus(usedMove.moveDef.type_id, target.species.type_id2);
		float power_mod = 1f; //TODO: mod power
		float stab_bonus = (
			usedMove.moveDef.movetype == attacker.species.type1 || 
			usedMove.moveDef.movetype == attacker.species.type2
		) ? 1.5f : 1f;
		float atk_def_ratio = 
			(float)(attacker.stats[usedMove.moveDef.atkAffinity]) /
			(float)(target.stats[usedMove.moveDef.defAffinity]);
			
		float base_damage = (
            usedMove.moveDef.power
            * atk_def_ratio
            * ((2 * attacker.level + 10) / 250f)
        ) + 2;
        
		float mod_damage = 
			stab_bonus *
			type_bonus *
			crit_bonus *
			power_mod *
			(Globals.RNG.NextFloat() * .15f + .85f);
		short damage = (short)( base_damage * mod_damage);
			
		target.hp -= damage;
>>>>>>> ccffb5f... Replacing the 'BetterEnum' class with a 'BetterEnumArray' class, allowing arrays to be indexed by enums.
		return target.hp <= 0;
=======
	public void checkForDeath() {
    	foreach (int j in Enumerable.Range(1,attackQueue.Count))
			if (battleSlots[j].activeCreature.hp <= 0) 
				killCreatureAtIndex(j);
	}
	
	public void killCreatureAtIndex(int index) {
    	Creature deadCreature = battleSlots[index].activeCreature;
		System.Console.WriteLine("Creature died!: " + deadCreature);
		removeCreatureAttacks(deadCreature);
		replaceCreature(index);
	}
	
	public void removeCreatureAttacks(Creature removedCreature) {
		attackQueue.RemoveWhere(attack=>attack.attacker == removedCreature);
		
	}
	
	public int getBattleSlotFromCreature(Creature c) {
		return battleSlots.First(pair => pair.Value.activeCreature == c).Key;
>>>>>>> 0d244c8... Rewrite of battle class. Move attack into separate file.
	}
}
}