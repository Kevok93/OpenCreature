using System.Collections.Generic;
namespace opencreature {
public class DoubleBattle : MultiBattle {
    public DoubleBattle(Trainer t1, Trainer t2) {
        
        Creature c1,c2,c3,c4;
		c1 = t1.getNextCreature();
		if (c1 == null)
			throw new System.InvalidOperationException ("Trainer 1 entered a battle with no valid creatures!");
        c2 = t1.getNextCreature(c1);
        c3 = t2.getNextCreature();
		if (c3 == null)
			throw new System.InvalidOperationException ("Trainer 2 entered a battle with no valid creatures!");
        c4 = t2.getNextCreature(c3);
        
		battleSlots = new Dictionary<int, BattleSlot> { 
			{1, new BattleSlot{slotOwner = t1, activeCreature = c1}},
			{2, new BattleSlot{slotOwner = t1, activeCreature = c2}},
			{3, new BattleSlot{slotOwner = t2, activeCreature = c3}},
			{4, new BattleSlot{slotOwner = t2, activeCreature = c4}},
		};
		teamAssignment = new Dictionary<int, List<BattleSlot>> {
			{1, new List<BattleSlot>{battleSlots[1],battleSlots[2]}},
			{2, new List<BattleSlot>{battleSlots[3],battleSlots[4]}},
		};
    }
}
}
