
public class MultiBattle : Battle {
	protected MultiBattle() {}
    public MultiBattle(Trainer t1, Trainer t2, Trainer t3, Trainer t4) {
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
	public override int checkVictory() {
		if (activeCreatures[2] == null && activeCreatures[3] == null) return 1;
		if (activeCreatures[0] == null && activeCreatures[1] == null) return 2;
		return 0;
	}
	public override Attack queueAttack(Creature c, LearnedMove m, sbyte target = -1) {
		Attack newAttack = base.queueAttack(c, m, target);
		if (m.moveDef.misc_info [(int)MoveData.Target_Single]) {
		    if (target >= 0) throw new System.InvalidOperationException ("Using a single target move without a valid target!");
		    else newAttack.targets = new byte[] {(byte)target};
		} else if (m.moveDef.misc_info [(int)MoveData.Target_All])
			switch (turn) {
			    case 0:
			        newAttack.targets = new byte[] { 1, 2, 3 };
                    break;
			    case 1:
			        newAttack.targets = new byte[] { 0, 2, 3 };
                    break;
			    case 2:
			        newAttack.targets = new byte[] { 1, 0, 3 };
                    break;
			    case 3:
			        newAttack.targets = new byte[] { 1, 2, 0 };
                    break;
        		default: 
        		    throw new System.InvalidOperationException("Invalid turn number for a single battle: "+turn);
		} else if (m.moveDef.misc_info [(int)MoveData.Target_Both])
            switch (turn) {
                case 0:
                case 1:
                    newAttack.targets = new byte[] { 2, 3 };
                    break;
                case 2:
                case 3:
                    newAttack.targets = new byte[] { 0, 1 };
                    break;
                default: 
                    throw new System.InvalidOperationException("Invalid turn number for a single battle: " + turn);
		} else if (m.moveDef.misc_info[(int)MoveData.Target_Self]) {
		  newAttack.targets = new byte[] { (byte) turn };
		} else if (m.moveDef.misc_info[(int)MoveData.Target_Ally])
			switch (turn) {
			    case 0:
			        newAttack.targets = new byte[] { 1 };
                    break;
			    case 1:
			        newAttack.targets = new byte[] { 0 };
                    break;
			    case 2:
			        newAttack.targets = new byte[] { 3 };
                    break;
			    case 3:
			        newAttack.targets = new byte[] { 2 };
                    break;
        		default: 
        		    throw new System.InvalidOperationException("Invalid turn number for a single battle: "+turn);
		} else throw new System.InvalidOperationException ("Move does not have a valid target flag: " + m.moveDef.name + m.moveDef.id);
		turn++;
        return newAttack;
	}
}


