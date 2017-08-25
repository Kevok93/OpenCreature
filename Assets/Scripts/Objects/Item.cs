using System;
using System.Collections.Generic;

namespace opencreature {
public class Item : DeserializedElement {
	public static Dictionary<int,Item> ITEMS;
	public string name, description;
	public short price;
	public sbyte misc_val1, misc_val2;
<<<<<<< HEAD
	public bool[] misc_info;
=======
	public BetterEnumArray<ItemData,bool> misc_info;
>>>>>>> ccffb5f... Replacing the 'BetterEnum' class with a 'BetterEnumArray' class, allowing arrays to be indexed by enums.
	public string sprite_path;
	
	public byte item_type_id;
	public int battle_effect_id, world_effect_id, held_effect_id;
	public Effect world_effect, battle_effect, held_effect;
	public ItemType item_type;
	
	private Item(){}
	public static long init(List<Dictionary<string,string>> item_defs) {
		long count = 0;
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
			temp.item_type_id = Convert.ToByte(row["type"]);
			temp.sprite_path = row["sprite_path"];
			temp.description = row["description"];
			ITEMS[temp.id] = temp;
			count++;
		}
		return count;
	}
	public static void link() {
	    foreach (Item temp in ITEMS.Values) {
	        if (temp.world_effect_id != 0)		temp.world_effect = Effect.EFFECTS[temp.world_effect_id];
	        if (temp.battle_effect_id != 0)		temp.battle_effect = Effect.EFFECTS[temp.battle_effect_id];
	        if (temp.held_effect_id != 0)		temp.held_effect = Effect.EFFECTS[temp.held_effect_id];
	        if (temp.item_type_id != 0)			temp.item_type = ItemType.ITEM_TYPES[temp.item_type_id];
	    }
	}
	public override string ToString() {
		return String.Format("{0} (${1}): {2}", name, price, description);
	}
}
}