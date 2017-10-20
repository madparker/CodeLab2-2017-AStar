using UnityEngine;
using System.Collections;

public class HA_HueristicScript : HueristicScript {

	public override float Hueristic(int x, int y, Vector3 start, Vector3 goal, GridScript gridScript){
		//		return Random.Range(0, 500);
		float xDistance = Mathf.Abs (x - goal.x);
		float yDistance = Mathf.Abs (y - goal.y);

		float cross = Mathf.Abs (((x - goal.x) * (start.y - goal.y)) - ((start.x - goal.x) * (y - goal.y)));

		float heuristic = gridScript.costs [0] * xDistance + yDistance;

		heuristic += cross * 0.005f;



		return heuristic;
	}
}

