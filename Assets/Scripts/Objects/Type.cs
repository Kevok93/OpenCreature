using System.Collections;

public class Type {
	public int id;
	public string name;
	public float[] bonus;
	public Type(int id, string name, int type_count) {
		this.name 			= name;
		this.id				= id;
		
		bonus = new float[type_count];
		for (int i = 0; i < type_count; i++)
			bonus[i] = 1f;
	}
}
