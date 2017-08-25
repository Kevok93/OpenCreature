using System.Collections.Generic;
using System.Linq;
public abstract class Battle {
    public enum BattleType {
        SingleBattle,DoubleBattle,MultiBattle
    }
    public struct Attack {
    	public Creature attacker;
    	public byte[] targets;
    	public LearnedMove usedMove;
    	public short speed;
    }
    public Trainer[] trainers;
    public Creature[] activeCreatures;
    public int turn = 0;
    public BattleType type;
    
	protected Battle() {}
	
	public abstract Attack queueAttack(Creature c, LearnedMove m, sbyte target = -1);
	public void switchCreature(int activeCreatureIndex, int trainerCreatureIndex) {
		activeCreatures[activeCreatureIndex] = trainers[activeCreatureIndex].creatures[trainerCreatureIndex];
	}
	public virtual void replaceCreature(int index) {
		activeCreatures[index] = trainers[index].getNextCreature();
	}
	public abstract int checkVictory();
	
	public bool executeAttacks(List<Attack> attacks) {
        attacks.OrderByDescending(x => x.speed);
		foreach (Attack a in attacks) {
			foreach (int index in a.targets) {
				Creature defender = activeCreatures[index];
				if (defender != null && attack(a.attacker, a.usedMove, defender)) {
					replaceCreature(index);
					if (checkVictory() != 0) return true;
				}
			}
		}
		return false;
	}
	
	public bool attack(Creature attacker, LearnedMove usedMove, Creature target) {
		float crit_chance = .85f; //TODO: Crit bonuses
		float power_mod = 1f; //TODO: mod power
		short damage = (short)( 
			    (
					usedMove.moveDef.power + 2
			) * (
					Type.getTypeBonus(usedMove.moveDef.type_id, target.species.type_id1)
			) * ( 
				    Type.getTypeBonus(usedMove.moveDef.type_id, target.species.type_id2)
			) * (
					(2 * attacker.level + 10f) / 250f
			) * (
					attacker.stats[(int)usedMove.moveDef.atkAffinity] / 
					target.stats[(int)usedMove.moveDef.defAffinity]
			) * (
					(
						usedMove.moveDef.movetype == attacker.species.type1 || 
						usedMove.moveDef.movetype == attacker.species.type2
					) ? 1.5f : 1f
			) * (
					Globals.RNG.NextDouble() > crit_chance ? 2f : 1f
			) * (
					Globals.RNG.NextDouble()*.15f +.85f
			) * (
					power_mod
			)
		);
			
		target.hp -= damage;
		return target.hp <= 0;
	}
}
