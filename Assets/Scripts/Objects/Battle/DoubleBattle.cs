
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
	public override Attack queueAttack(Creature c, Move m, sbyte target = -1) {
        Attack attack;
        attack.attacker = c;
        attack.usedMove = m;
		attack.speed = c.stats [(int)StatsType.speed];
		if (m.misc_info [(int)MoveData.Target_Single]) {
		    if (target >= 0) throw new System.InvalidOperationException ("Using a single target move without a valid target!");
		    else attack.targets = new byte[] {(byte)target};
		} else if (m.misc_info [(int)MoveData.Target_All])
			switch (turn) {
			    case 0:
			        attack.targets = new byte[] { 1, 2, 3 };
                    break;
			    case 1:
			        attack.targets = new byte[] { 0, 2, 3 };
                    break;
			    case 2:
			        attack.targets = new byte[] { 1, 0, 3 };
                    break;
			    case 3:
			        attack.targets = new byte[] { 1, 2, 0 };
                    break;
        		default: 
        		    throw new System.InvalidOperationException("Invalid turn number for a single battle: "+turn);
		} else if (m.misc_info [(int)MoveData.Target_Both])
            switch (turn) {
                case 0:
                case 1:
                    attack.targets = new byte[] { 2, 3 };
                    break;
                case 2:
                case 3:
                    attack.targets = new byte[] { 0, 1 };
                    break;
                default: 
                    throw new System.InvalidOperationException("Invalid turn number for a single battle: " + turn);
		} else if (m.misc_info[(int)MoveData.Target_Self]) {
		  attack.targets = new byte[] { (byte) turn };
		} else if (m.misc_info[(int)MoveData.Target_Ally])
			switch (turn) {
			    case 0:
			        attack.targets = new byte[] { 1 };
                    break;
			    case 1:
			        attack.targets = new byte[] { 0 };
                    break;
			    case 2:
			        attack.targets = new byte[] { 3 };
                    break;
			    case 3:
			        attack.targets = new byte[] { 2 };
                    break;
        		default: 
        		    throw new System.InvalidOperationException("Invalid turn number for a single battle: "+turn);
		} else throw new System.InvalidOperationException ("Move does not have a valid target flag: " + m.name + m.id);
        return attack;
	}
}

