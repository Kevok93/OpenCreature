
public class SingleBattle : Battle {
    public SingleBattle(Trainer t1, Trainer t2) {
		attacks = new System.Collections.Generic.List<Attack>();
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
	
	public override int checkVictory() {
		if (activeCreatures[1] == null) return 1;
		if (activeCreatures[0] == null) return 2;
		return 0;
	}

	public override Attack queueAttack(Creature c, LearnedMove m, sbyte target = -1) {
		Attack attack = base.queueAttack(c, m, target);
		bool targetSelf = m.moveDef.misc_info [(int)MoveData.Target_Self];
		switch (turn) {
            case 0:
		        attack.targets = new byte[] {(byte) (targetSelf ? 0 : 1)};
                break;
		    case 1: 
                attack.targets = new byte[] {(byte) (targetSelf ? 1 : 0)};
                break;
    		default: 
    		    throw new System.InvalidOperationException("Invalid turn number for a single battle: "+turn);
		}
		turn++;
		return attack;
	}
}

