using System.Collections.Generic;
namespace opencreature {
public class SingleBattle : Battle {
	public enum BattleSlotName {
		PLAYER1=1,
		PLAYER2=2,
	};
		
    public SingleBattle(Trainer t1, Trainer t2) {
		attackQueue = new SortedSet<Attack>();
        
        Creature c1,c2;
        c1 = t1.getNextCreature();
		if (c1 == null)
			throw new System.InvalidOperationException ("Trainer 1 entered a battle with no valid creatures!");
		c2 = t2.getNextCreature();
		if (c2 == null)
			throw new System.InvalidOperationException ("Trainer 2 entered a battle with no valid creatures!");
		battleSlots = new Dictionary<int, BattleSlot> { 
			{1, new BattleSlot{slotOwner = t1, activeCreature = c1}},
			{2, new BattleSlot{slotOwner = t2, activeCreature = c2}},
		};
		teamAssignment = new Dictionary<int, List<BattleSlot>> {
			{1, new List<BattleSlot>{battleSlots[1]}},
			{2, new List<BattleSlot>{battleSlots[2]}},
		};
	}

	public override Attack queueAttack(Creature c, LearnedMove m, sbyte target = -1) {
		Attack attack = base.queueAttack(c, m, target);
		bool targetSelf = m.moveDef.misc_info [MoveData.Target_Self];
		BattleSlotName battleSlotName = (BattleSlotName) getBattleSlotFromCreature(c);
		switch (battleSlotName) {
            case BattleSlotName.PLAYER1:
		        attack.targets = new byte[] {(byte) (targetSelf ? 1 : 2)};
                break;
		    case BattleSlotName.PLAYER2: 
                attack.targets = new byte[] {(byte) (targetSelf ? 2 : 1)};
                break;
		}
		return attack;
	}
}
}
