using System.Collections.Generic;
using System.Linq;
public abstract class Battle {
    public enum BattleType {
        SingleBattle,DoubleBattle,MultiBattle
    }
    public struct Attack {
    	public Creature attacker;
    	public byte[] targets;
    	public Move usedMove;
    	public short speed;
    }
    public Trainer[] trainers;
    public Creature[] activeCreatures;
    public int turn = 0;
    public BattleType type;
    
	protected Battle() {}
	public abstract Attack queueAttack(Creature c, Move m, sbyte target = -1);
	public abstract Creature getCreature(int index);
	public bool executeAttacks(List<Attack> attacks) {
        attacks.OrderByDescending(x => x.speed);
        foreach (Attack a in attacks) {
            Creature attacker = a.attacker;
            Move move = a.usedMove;
            StatsType atkAffinity, defAffinity;
            switch (move.affinity) {
                case MoveAffinity.Physical:
                    atkAffinity = StatsType.atk;
                    defAffinity = StatsType.def;
                    break;
                case MoveAffinity.Special:
                    atkAffinity = StatsType.sp_atk;
                    defAffinity = StatsType.sp_def;
                    break;
            }
            foreach(int index in a.targets) {
                Creature defender = getCreature(index);
                if (defender == null) return false;
            }
        }
	}
}
