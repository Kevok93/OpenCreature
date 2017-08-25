using System.Collections.Generic;
using System.Linq;

namespace opencreature {
public abstract class Battle {
    public class Attack {
    	public Creature attacker;
    	public byte[] targets;
    	public LearnedMove usedMove;
    	public short speed;
    }
    public Trainer[] trainers;
    public Creature[] activeCreatures;
    public int turn = 0;
	public List<Attack> attacks;
	protected Battle() {
	}
	
	public virtual Attack queueAttack(Creature c, LearnedMove m, sbyte target = -1) {
		if (turn >= activeCreatures.Count()) throw new System.InvalidOperationException("Too many moves queued!");
		if (activeCreatures[turn] != c) throw new System.InvalidOperationException("Move is queued out of turn!");
		foreach (Attack a in attacks) {
			if (a.attacker == c) throw new System.InvalidOperationException("Creature has already queued an attack!");
		}
		Attack q_attack = new Attack();
		q_attack.attacker = c;
		q_attack.usedMove = m;
		q_attack.speed = c.stats [StatsType.speed];
		attacks.Add(q_attack);
		return q_attack;
	}
	
	public void switchCreature(int activeCreatureIndex, int trainerCreatureIndex) {
		activeCreatures[activeCreatureIndex] = trainers[activeCreatureIndex].creatures[trainerCreatureIndex];
	}
	public virtual void replaceCreature(int index) {
		activeCreatures[index] = trainers[index].getNextCreature();
	}
	public abstract int checkVictory();
	
	public bool executeAttacks() {
		if (attacks.Count() != activeCreatures.Count()) throw new System.InvalidOperationException("Executing attacks before all creatures are ready!");
		turn = 0;
		var sorted_attacks = attacks.OrderByDescending(x => x.speed).ToArray();
		for (int i = 0; i < sorted_attacks.Count(); i++) {
        	Attack a = sorted_attacks[i];
        	if (a == null) continue;
			foreach (int index in a.targets) {
				Creature defender = activeCreatures[index];
				if (defender != null && attack(a.attacker, a.usedMove, defender)) {
					System.Console.WriteLine("Creature died!: " + defender);
					for (int j = i; j < sorted_attacks.Count(); j++)
						if (sorted_attacks[j].attacker == defender) {
							attacks.Remove(sorted_attacks[j]);
							sorted_attacks[j] = null;
						}
					replaceCreature(index);
					if (checkVictory() != 0) return true;
				}
			}
			attacks.Remove(a);
		}
		return false;
	}
	
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
	}
}
}