

public enum BattleType {
    SingleBattle,DoubleBattle,MultiBattle
}
public class Battle {
    public Trainer[] team1_trainers, team2_trainers;
    public Creature[] team1_activeCreatures, team2_activeCreatures;
    public byte turn = 1;
    public BattleType type;
    
    private Battle();
    public static Battle SingleBattle(Trainer t1, Trainer t2) {
        Battle temp = new Battle();
        temp.type = BattleType.SingleBattle;
        temp.team1_trainers = new Trainer[] { t1 };
        temp.team2_trainers = new Trainer[] { t2 };
        
        Creature c1;
        c1 = t1.getNextCreature();
        if (c1 == null) throw new System.Exception
        temp.team1_activeCreatures = new Creature[] { c1 };
        c1 = t2.getNextCreature();
        temp.team1_activeCreatures = new Creature[] { c1 };
    }
    public static Battle DoubleBattle(Trainer t1, Trainer t2) {
        Battle temp = new Battle();
        temp.type = BattleType.SingleBattle;
        temp.team1_trainers = new Trainer[] { t1 };
        temp.team2_trainers = new Trainer[] { t2 };
        
        Creature c1,c2;
        c1 = t1.getNextCreature();
        c2 = t1.getNextCreature(c1);
        temp.team1_activeCreatures = new Creature[] { c1,c2 };
        c1 = t2.getNextCreature();
        c2 = t2.getNextCreature(c1);
        temp.team1_activeCreatures = new Creature[] { c1,c2 };
    }
    public static Battle MultiBattle(Trainer t1, Trainer t2, Trainer t3, Trainer t4) {
        Battle temp = new Battle();
        temp.type = BattleType.SingleBattle;
        temp.team1_trainers = new Trainer[] { t1, t2 };
        temp.team2_trainers = new Trainer[] { t3, t4 };
        
        Creature c1,c2;
        c1 = t1.getNextCreature(); 
        c2 = t2.getNextCreature();
        temp.team1_activeCreatures = new Creature[] { c1,c2 };
        c1 = t3.getNextCreature();
        c2 = t4.getNextCreature();
        temp.team1_activeCreatures = new Creature[] { c1,c2 };
    }
    
    public void attack(Creature c, Creature target = null) {
        
    }
}
