
using System.Collections.Generic;
public struct LevelMove {
    public byte level;
    public int move_id;
    public Move move;
}
public struct Evolution {
    public int value; //can be level, happiness, or item id
    public int species_id;
    public Species species;
    public EvolutionType type;
    //public Item item;
}
public struct EggGroup {
	public static Dictionary<int,EggGroup> EGG_GROUPS;
	public int id;
	public string name;
}
public struct Nature {
	public static Dictionary<int,Nature> NATURES;
	public int id;
	public string name;
	public sbyte[] stats_mod;
}
public struct ItemType {
	public Dictionary<int,ItemType> ITEM_TYPES;
	public int id;
	public string name;
	public string description;
}
public struct TrainerStyle {
	public static Dictionary<int,TrainerStyle> TRAINER_STYLES;
	public int id;
	public string name;
	public string sprite_path;
	/*	public Sprite sprite;
	 * public music bgm;*/
}
public struct NPCStyle {
	public static Dictionary<int,NPCStyle> NPC_STYLES;
	public int id;
	public string name;
	public string sprite_path;
	/*	public Sprite sprite;*/
}
public struct PlotFlag {
	public static Dictionary<string,PlotFlag> PLOT_FLAG_NAME;
	public static Dictionary<int,PlotFlag> PLOT_FLAG_ID;
	public int id;
	public string name;
	public sbyte value;
}