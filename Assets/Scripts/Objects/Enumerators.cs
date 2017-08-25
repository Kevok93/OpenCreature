using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

public enum MoveAffinity  {
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
    Target_Single   = 4,
    Two_Turns       = 5,
    Need_Recharge   = 6,
    No_Miss         = 7,
    Switch_user     = 8,
    Metronome       = 9,
    No_Metronome    = 10,
    Recoil          = 11,
    Fixed_Dmg       = 12,
    Go_first        = 13,
    Go_last         = 14,
    Curse           = 15,
    Multiple_Hits   = 16,
    HM              = 17,
    Bonus_Crit      = 18,
}
public enum TrainerStatsType {
    Repel           = 0,
    Bike            = 1,
    Surf            = 2,
    HM_Locked       = 3,
    
}
