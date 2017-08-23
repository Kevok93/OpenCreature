public enum MoveAffinity {
    Physical	    = 1,
    Special		    = 2,
    Other 		    = 3
}
public enum StatsType {
    atk 	        = 0,
    def		        = 1,
    sp_atk	        = 2,
    sp_def 	        = 3,
    hp 		        = 4,
    speed	        = 5,
}
public enum EvolutionType {
    Level           = 1,
    Trade           = 2,
    Item            = 3,
    Happiness       = 4,
}
public enum EffectData {
    Heal            = 0,
    Target_stat     = 1,
    Protect         = 2,
    Stun            = 3,
    Trap            = 4,
    Doom            = 5,
    Target_player   = 6,
    Treatable_cond  = 7,
    Start_evolve    = 8,
}
public enum UniqueCreatureData {
    Catchable       = 0,
    Legendary       = 1,
    Shiny           = 2,
    IQ_feral        = 3,
    IQ_trainer      = 4,
    IQ_E4           = 5,
    IQ_Rival        = 6,
}
public enum ItemData {
    No_Held         = 0,
    No_Sell         = 1,
    Reusable        = 2,
}
public enum TrainerData {
    Rival           = 0,
    Leader_E4       = 1,
    Champion        = 2,
    Wild            = 3,
    Can_Rematch     = 4,
    Rematch_ready   = 5,
}
public enum MoveData {
    Target_All      = 0,
    Target_Both     = 1,
    Target_Self     = 2,
    Target_Ally     = 3,
    Two_Turns       = 4,
    Need_Recharge   = 5,
    No_Miss         = 6,
    Switch_user     = 7,
    Metronome       = 8,
    No_Metronome    = 9,
    Recoil          = 10,
    Fixed_Dmg       = 11,
    Go_first        = 12,
    Go_last         = 13,
    Curse           = 14,
    Multiple_Hits   = 15,
    HM              = 16,
    Bonus_Crit      = 17,
}
public enum TrainerStatsType {
    Repel           = 0,
    Bike            = 1,
    Surf            = 2,
    HM_Locked       = 3,
    
}
