using System;
using System.Collections.Generic;

namespace opencreature {
	public class Attack : IComparable {
    	public Creature attacker;
    	public byte[] targets;
    	public LearnedMove usedMove;
    	public short speed;
    	public int CompareTo(object b) {return this.speed - (b as Attack).speed;}
    	
    	public void execute(Dictionary<int,Battle.BattleSlot> battleslots) {
			foreach (int index in targets) {
				Creature defender = battleslots[index].activeCreature;
				if (defender == null) continue;
				executeAgainstCreature(defender);
			}
		}
    	
		public void executeAgainstCreature(Creature target) {
			if (usedMove.moveDef.power > 0) {
				
				//ATTACK vs DEFENCE or SPATK vs SPDEF
				float atk_def_ratio = 0f;
				if (usedMove.moveDef.affinity != MoveAffinity.Other) 
					atk_def_ratio =
						(float)attacker.stats[usedMove.moveDef.atkAffinity] /
						(float)target.stats[usedMove.moveDef.defAffinity];
				
				float base_damage   = (
	                usedMove.moveDef.power
	                * atk_def_ratio
	                * ((2 * attacker.level + 10) / 250f)
	            ) + 2;
				
				float crit_chance   = .85f; //TODO: Crit bonuses
				float crit_bonus    = Globals.RNG.NextDouble() > crit_chance ? 2f : 1f;
				float type_bonus    = 
					Element.getTypeBonus(usedMove.moveDef.type_id, target.species.type_id1) *
					Element.getTypeBonus(usedMove.moveDef.type_id, target.species.type_id2);
				float power_mod     = 1f; //TODO: mod power
				float stab_bonus    = (
	                usedMove.moveDef.movetype == attacker.species.type1 ||
	                usedMove.moveDef.movetype == attacker.species.type2
	            ) ? 1.5f : 1f;
				
				float mod_damage    = 
					stab_bonus * type_bonus * crit_bonus * power_mod  *
					Globals.RNG.NextFloatBetween(0.70f, 1.00f);
				short damage = (short)(base_damage * mod_damage);
				target.hp -= damage;
			}
		}
    }
}
