using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndrewHueristicScript : HueristicScript {

	public override float Hueristic(int x, int y, Vector3 start, Vector3 goal, GridScript gridScript)
	{
		float num;
		num = (Mathf.Abs(x - goal.x)) + (Mathf.Abs(y - goal.y));
		return num;
	}
}
