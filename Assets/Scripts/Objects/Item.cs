using System;
using System.Collections.Generic;

public class Item {
	public static Dictionary<int,Item> ITEMS;
	public int id;
	public string name;
	public short price;
	public sbyte misc_val1, misc_val2;
	public bool[] misc_info;
	public string sprite_path;
	
	public byte item_type_id;
	public int battle_effect_id, world_effect_id, held_effect_id;
	public Effect world_effect, battle_effect, held_effect;
	public ItemType item_type;
	
	private Item(){}
	public static void init(List<Dictionary<string,string>> item_defs) {
		//var results = db["Select * from abilities"][0];
		ITEMS = new Dictionary<int, Item> (item_defs.Count);
		foreach (Dictionary<string,string> row in item_defs) {
		    Item temp = new Item();
			temp.id = Convert.ToInt32(row["id"]);
			temp.name = row["name"];
			temp.price = Convert.ToInt16(row["price"]);
			temp.misc_val1 = Convert.ToSByte(row["misc_val1"]);
			temp.misc_val2 = Convert.ToSByte(row["misc_val2"]);
			temp.misc_info = SqliteConnection.getBitsFromBlob(row["misc_info"]);
			temp.battle_effect_id = Convert.ToInt32(row["battle_effect"]);
			temp.world_effect_id = Convert.ToInt32(row["world_effect"]);
			temp.held_effect_id = Convert.ToInt32(row["held_effect"]);
			temp.item_type_id = Convert.ToByte(row["item_type"]);
			temp.sprite_path = row["sprite_path"];
			ITEMS[temp.id] = temp;
		}
	}
	public static void link() {
	    foreach (Item temp in ITEMS.Values) {
	        temp.world_effect = Effect.EFFECTS[temp.world_effect_id];
	        temp.battle_effect = Effect.EFFECTS[temp.battle_effect_id];
	        temp.held_effect = Effect.EFFECTS[temp.held_effect_id];
	        temp.item_type = ItemType.ITEM_TYPES[temp.item_type_id];
	    }
	}
}