using System;
using System.Collections.Generic;

public class Npc {
	public static Dictionary<int,Npc> NPCS;
	public int id;
    public byte style_id;
    public short map_id;
    public short x, y;
    public string text;
    public int trainer_id;
    public short plotflag_id;
    public bool[] misc_info;
    public NPCStyle style;
    //public Map map;
    public Trainer trainer;
    
	private Npc(){}
	public static void init(List<Dictionary<string,string>> npc_defs) {
		NPCS = new Dictionary<int, Npc> (npc_defs.Count);
		foreach (Dictionary<string,string> row in npc_defs) {
		    Npc temp = new Npc();
			temp.id = Convert.ToInt32(row["id"]);
			temp.text = row["text"];
			temp.style_id = Convert.ToByte(row["style"]);
			temp.map_id = Convert.ToInt16(row["map"]);
			temp.x = Convert.ToInt16(row["x"]);
			temp.y = Convert.ToInt16(row["y"]);
			temp.plotflag_id = Convert.ToInt16(row["plot_flag"]);
			temp.trainer_id = Convert.ToInt32(row["trainer"]);
			temp.misc_info = Sqlite.getBitsFromBlob(row["misc_info"]);
			NPCS[temp.id] = temp;
		}
	}
	public static void link() {
	    foreach (Npc temp in NPCS.Values) {
	        temp.trainer = Trainer.TRAINERS[temp.trainer_id];
	        temp.style = NPCStyle.NPC_STYLES[temp.style_id];
	        
	    }
	}
}
