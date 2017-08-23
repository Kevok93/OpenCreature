using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OverworldMovement : MonoBehaviour {
	public static byte
		movement_count = 16,	//Number of steps per tile
		movement_delay_ms = 100;//Delay between steps, in ms
	public static float
		movement_thresh = .6f,	//Percentage difference between axes needed to move
		turn_thresh = .3f;		//Percentage difference between axes needed to turn

	public sbyte
		direction, 			//1-4 NESW
		movement_progress=0, 	//1-movement_count
		movement_countdown; //1-movement_delay_ms
	public short
		x_loc,
		y_loc;
	
	public GameObject model;
	// Use this for initialization
	void Start () {
		model.GetComponent<MeshFilter>().mesh =  SqliteModelReader.readMesh("testmodel.db");
	}

	// Update is called once per frame
	void Update () {
		#region movement
		float
			horiz 	 	= Input.GetAxis("Horizontal"),
			vert	 	= Input.GetAxis("Vertical"),
			move_diff	= Mathf.Abs(horiz) - Mathf.Abs(vert), //neg if vert, pos if horiz
			step_distance = 1f/movement_count;
			
		if (Mathf.Abs(move_diff) > turn_thresh && movement_progress == 0) {
			if (move_diff > 0) { //horiz
				if (horiz > 0) direction=2;
						  else direction=4;
			} else { //vert
				if (vert > 0) direction=1;
						 else direction=3;
			}
		}
		if (movement_progress==15) {
			movement_progress=0;
			switch (direction) {
				case 1: y_loc++; break;
				case 2: x_loc++; break;
				case 3: y_loc--; break;
				case 4: x_loc--; break;
			}
		} else if (movement_progress>0) { //todo: add sleep timer
			movement_progress++;
		} else if (Mathf.Abs(move_diff) > movement_thresh){
			movement_progress=1;
		}
		
		switch (direction) {
			case 1:
				transform.position = new Vector3(x_loc,0,y_loc+(step_distance * movement_progress));
				model.transform.rotation = Quaternion.Euler(0,0,0);
				break;
			case 2:
				transform.position = new Vector3(x_loc+(step_distance * movement_progress),0,y_loc);
				model.transform.rotation = Quaternion.Euler(0,90,0);
				break;
			case 3:
				transform.position = new Vector3(x_loc,0,y_loc-(step_distance * movement_progress));
				model.transform.rotation = Quaternion.Euler(0,180,0);
				break;
			case 4:
				transform.position = new Vector3(x_loc-(step_distance * movement_progress),0,y_loc);
				model.transform.rotation = Quaternion.Euler(0,270,0);
				break;
		}
		#endregion

		
	}
}
