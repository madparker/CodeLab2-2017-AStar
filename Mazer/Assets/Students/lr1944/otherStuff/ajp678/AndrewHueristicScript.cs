using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndrewHueristicScript : HueristicScript {

	public bool control;

	public override float Hueristic(int x, int y, Vector3 start, Vector3 goal, GridScript gridScript)
	{
		float num;
		if (control) {
			num = 0;
		} else {
			//Manhattan
			num = gridScript.costs[0] * Mathf.Abs (x - goal.x) + Mathf.Abs (y - goal.y);
//			float distFromStart = gridScript.costs [0] * Mathf.Abs (x - start.x) + Mathf.Abs (y - start.y);
//			float distFromGoal = gridScript.costs [0] * Mathf.Abs (x - goal.x) + Mathf.Abs (y - goal.y);
//			if (distFromStart < distFromGoal) {
//				num = distFromStart;
//			} else {
//				num = distFromGoal;
//			}

		}
		return num;
	}
}
