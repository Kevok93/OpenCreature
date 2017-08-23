using System;

public class Moves
{
	int id;
	string name;
	byte pp, power, accuracy;
	Type movetype;
	Effect world_effect, battle_effect;
	MoveAffinity affinity;
	bool[] misc_info;
	byte misc_value;
	string description;

	public Moves(
		int id,
		string name,
		byte pp, 
		byte power, 
		byte accuracy,
		Type movetype,
		Effect world_effect, 
		Effect battle_effect,
		MoveAffinity affinity,
		bool[] misc_info,
		byte misc_value,
		string description
	) { 
		this.id = id;
		this.name = name;
		this.pp = pp;
		this.power = power;
		this.accuracy = accuracy;
		this.movetype = movetype;
		this.world_effect = world_effect;
		this.battle_effect = battle_effect;
		this.affinity = affinity;
		this.misc_info = misc_info;
		this.misc_value = misc_value;
		this.description = description;
	}


}

