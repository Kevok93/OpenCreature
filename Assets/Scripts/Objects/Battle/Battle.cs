public enum BattleType {
    SingleBattle,DoubleBattle,MultiBattle
}
public struct Attack {
	public Creature attacker;
	public byte[] targets;
	public Move usedMove;
	public short speed;
}
public abstract class Battle {
    public Trainer[] trainers;
    public Creature[] activeCreatures;
    public int turn = 0;
    public BattleType type;
    
	protected Battle() {}
	public abstract Attack queueAttack(Creature c, Move m, Creature target = null);
}

public class SingleBattle : Battle {
    public SingleBattle(Trainer t1, Trainer t2) {
        type = BattleType.SingleBattle;
		trainers = new Trainer[] { t1,t2 };
        
        Creature c1,c2;
        c1 = t1.getNextCreature();
		if (c1 == null)
			throw new System.InvalidOperationException ("Trainer 1 entered a battle with no valid creatures!");
		c2 = t2.getNextCreature();
		if (c2 == null)
			throw new System.InvalidOperationException ("Trainer 2 entered a battle with no valid creatures!");
		activeCreatures = new Creature[] { c1,c2 };
	}

	public override Attack queueAttack(Creature c, Move m, Creature target = null) {
		Attack attack;
		attack.attacker = c;
		attack.usedMove = m;
		attack.speed = c.stats [(int)StatsType.speed];

		bool targetSelf = m.misc_info [(int)MoveData.Target_Self];
		switch (turn) {
		case 0: attack.targets = { targetSelf ? 0 : 1};
		case 1: attack.targets = { targetSelf ? 1 : 0};
		default: throw new System.InvalidOperationException("Invalid turn number for a single battle: "+turn); 
		}
		return attack;
	}
}
public class DoubleBattle : Battle {
    public DoubleBattle(Trainer t1, Trainer t2) {
        type = BattleType.SingleBattle;
		trainers = new Trainer[] {t1,t2};
        
        Creature c1,c2,c3,c4;
		c1 = t1.getNextCreature();
		if (c1 == null)
			throw new System.InvalidOperationException ("Trainer 1 entered a battle with no valid creatures!");
        c2 = t1.getNextCreature(c1);
        c3 = t2.getNextCreature();
		if (c3 == null)
			throw new System.InvalidOperationException ("Trainer 2 entered a battle with no valid creatures!");
        c4 = t2.getNextCreature(c3);
		activeCreatures = new Creature[] {c1,c2,c3,c4};
    }
}
public class MultiBattle : Battle {
    public MultiBattle(Trainer t1, Trainer t2, Trainer t3, Trainer t4) {
        type = BattleType.SingleBattle;
		trainers = new Trainer[] { t1, t2,t3,t4};
		
		Creature c1,c2,c3,c4;

        c1 = t1.getNextCreature(); 
		if (c1 == null)
			throw new System.InvalidOperationException ("Trainer 1 entered a battle with no valid creatures!");
        c2 = t2.getNextCreature();
		if (c2 == null)
			throw new System.InvalidOperationException ("Trainer 2 entered a battle with no valid creatures!");

        c3 = t3.getNextCreature();
		if (c3 == null)
			throw new System.InvalidOperationException ("Trainer 3 entered a battle with no valid creatures!");
        c4 = t4.getNextCreature();
		if (c4 == null)
			throw new System.InvalidOperationException ("Trainer 4 entered a battle with no valid creatures!");
		activeCreatures = new Creature[] {c1,c2,c3,c4};
    }
	public override Attack queueAttack(Creature c, Move m, Creature target = null) {
		short speed = c.stats [(int)StatsType.speed];
		if (type == BattleType.SingleBattle) {
			if (m.misc_info [(int)MoveData.Target_Self])
				return Attack (c, new byte[0], m, speed);
			else 
				return Attack (c, new byte[1], m, speed);
		} else {
			if (m.misc_info [(int)MoveData.Target_Single] && target == null)
				throw new System.InvalidOperationException ("Using a single target move without a valid target!");
			int[] targets;
			if (m.misc_info [(int)MoveData.Target_All])
				switch (turn) {
				case 0:
				targets = new int[] { 1, 2, 3 };
			}
		}
	}
}
