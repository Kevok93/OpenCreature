
public class DoubleBattle : MultiBattle {
    public DoubleBattle(Trainer t1, Trainer t2) {
		trainers = new Trainer[] {t1,t1,t2,t2};
        
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
	public override void replaceCreature(int index) {
		switch (index) {
				case 0: trainers[index].getNextCreature(activeCreatures[1]); break;
				case 1: trainers[index].getNextCreature(activeCreatures[0]); break;
				case 2: trainers[index].getNextCreature(activeCreatures[3]); break;
				case 3: trainers[index].getNextCreature(activeCreatures[2]); break;
		}
	}

}

