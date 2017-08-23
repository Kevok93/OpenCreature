using System.Collections;
using System.Collections.Generic;

public class Effect {
	public static Dictionary<int,Effect> EFFECTS;
	public int id;
	public string name, description;
	public bool[] misc_info;
	public byte misc_val1, misc_val2;
	public byte length;
	public string text;

	private Effect(){}

	public static Effect initEffects(List<Dictionary<string,string>> results) {
	}

}
