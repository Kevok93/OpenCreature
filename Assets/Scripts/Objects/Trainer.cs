using System;
using System.Collections.Generic;

public class Trainer {
	public static Dictionary<int,Trainer> TRAINERS;
	public int id;
	public string name;
	public byte trainer_style_id;
	public int[] creatures_id, items_id;
	public int reward;
	public int rematch_trainer_id;
	public string victory_quote;
	public bool[] misc_info;

	public Trainer rematch_trainer;
	public Creature[] creatures;
	public Item[] items;
	public TrainerStyle trainer_style;

	private Trainer(){}
	public static void init(List<Dictionary<string,string>> trainer_defs) {
		//var results = db["Select * from trainer"][0];
		TRAINERS = new Dictionary<int, Trainer> (trainer_defs.Count);
		foreach (Dictionary<string,string> row in trainer_defs) {
			Trainer temp = new Trainer();
			temp.id = Convert.ToInt32(row["id"]);
			temp.name = row["name"];
			temp.victory_quote = row["quote"];
			temp.trainer_style_id = Convert.ToByte(row["style"]);
			temp.reward = Convert.ToInt32(row["reward"]);
			temp.rematch_trainer_id = Convert.ToInt32(row["rematch_uid"]);
			temp.creatures_id = new int[] {
				Convert.ToInt32(row["poke1"]),
				Convert.ToInt32(row["poke2"]),
				Convert.ToInt32(row["poke3"]),
				Convert.ToInt32(row["poke4"]),
				Convert.ToInt32(row["poke5"]),
				Convert.ToInt32(row["poke6"]),
			};
			temp.items_id = new int[] {
				Convert.ToInt32(row["item1"]),
				Convert.ToInt32(row["item2"]),
				Convert.ToInt32(row["item3"]),
				Convert.ToInt32(row["item4"]),
				Convert.ToInt32(row["item5"]),
				Convert.ToInt32(row["item6"]),
			};
			temp.misc_info = SqliteConnection.getBitsFromBlob (row ["misc_info"]);
			TRAINERS[temp.id] = temp;
		}
	}
	public static void link() {
		foreach (Trainer temp in TRAINERS.Values) {
			temp.trainer_style = TrainerStyle.TRAINER_STYLES [temp.trainer_style_id];
			temp.rematch_trainer = TRAINERS [temp.rematch_trainer_id];
			temp.items = new Item[] {
				Item.ITEMS[temp.items_id[0]],
				Item.ITEMS[temp.items_id[1]],
				Item.ITEMS[temp.items_id[2]],
				Item.ITEMS[temp.items_id[3]],
				Item.ITEMS[temp.items_id[4]],
				Item.ITEMS[temp.items_id[5]],
			};
			temp.creatures = new Creature[] {
				Creature.CREATURES[temp.creatures_id[0]],
				Creature.CREATURES[temp.creatures_id[1]],
				Creature.CREATURES[temp.creatures_id[2]],
				Creature.CREATURES[temp.creatures_id[3]],
				Creature.CREATURES[temp.creatures_id[4]],
				Creature.CREATURES[temp.creatures_id[5]],
			};
		}
	}
}

