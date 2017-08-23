using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Creaturedb {
	private static bool init = false;
	private static Sqlite db;
	
	private Creaturedb(){}
	public static bool initialize() {
		if (init) return false;
		
		db = new Sqlite("creature.db", SqliteOpenOpts.SQLITE_OPEN_READONLY);
		Type.initTypes(
			db["Select * from types"][0],
			db["Select * from type_bonus"][0]
		);
		Creaturedb.initAbilities();
		Creaturedb.initSpecies ();
		
		db.Close();
		return true;
	}
	private static void initAbilities() {
		var results = db["Select * from abilities"][0];
		abilities = new Ability[results.Count];
		foreach (Dictionary<string,string> row in results) {
			int id = System.Convert.ToInt32(row["id"]);
			string name = row["name"];
			string desc = row["description"];
			try {abilities[id] = new Ability(id, name, desc);}
			catch (System.IndexOutOfRangeException e) {
				Debug.LogError("Ability id out of range! " + id + " " + name + " " + results.Count);
			}
		}
	}
	
	private static void initSpecies() {
		var results = db["Select * from species"][0];
		species = new Species[results.Count];
		foreach (Dictionary<string,string> row in results) {
			int id 				= System.Convert.ToInt32(row["id"]) - 1;
			string name 		= row["name"];
			int ab1 			= System.Convert.ToInt32(row["ability1"]);
			int ab2 			= System.Convert.ToInt32(row["ability2"]);
			int type1 			= System.Convert.ToInt32(row["type1"]);
			int type2 			= System.Convert.ToInt32(row["type2"]);
			byte gender_ratio	= System.Convert.ToByte(row["gender_ratio"]);
			byte capture_rate	= System.Convert.ToByte(row["capture_rate"]);
			byte ev_type		= System.Convert.ToByte(row["ev_type"]);
			byte ev_val			= System.Convert.ToByte(row["ev_val"]);
			int max_exp			= System.Convert.ToInt32(row["max_exp"]);
			short base_atk		= System.Convert.ToInt16(row["base_atk"]);
			short base_def		= System.Convert.ToInt16(row["base_def"]);
			short base_spatk	= System.Convert.ToInt16(row["base_spatk"]);
			short base_spdef	= System.Convert.ToInt16(row["base_spdef"]);
			short base_hp		= System.Convert.ToInt16(row["base_hp"]);
			short base_speed	= System.Convert.ToInt16(row["base_speed"]);
			short max_atk		= System.Convert.ToInt16(row["max_atk"]);
			short max_def		= System.Convert.ToInt16(row["max_def"]);
			short max_spatk		= System.Convert.ToInt16(row["max_spatk"]);
			short max_spdef		= System.Convert.ToInt16(row["max_spdef"]);
			short max_hp		= System.Convert.ToInt16(row["max_hp"]);
			short max_speed		= System.Convert.ToInt16(row["max_speed"]);
			string tm_list		= row["tm_list"];
			try {species[id] = new Species(
				id, name,
				gender_ratio,
				capture_rate,
				ev_type,
				ev_val,
				max_exp,
				base_atk,
				base_def,
				base_spatk,
				base_spdef,
				base_hp,
				base_speed,
				max_atk,
				max_def,
				max_spatk,
				max_spdef,
				max_hp,
				max_speed,
				ab1,ab2,
				type1,type2,
				tm_list
			);}
			catch (System.IndexOutOfRangeException e) {
				Debug.LogError("Pokemon Id out of range! " + id + " " + name + " " + results.Count);
			}
		}
	}
}