using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndrewHueristicScript : HueristicScript {

	public virtual float Hueristic(int x, int y, Vector3 start, Vector3 goal, GridScript gridScript)
	{
		Vector3 currentPos = new Vector3 (x, y, 0);
		float num = Mathf.Abs (Vector3.Distance (currentPos, goal)); 
		return num;
	}
}
