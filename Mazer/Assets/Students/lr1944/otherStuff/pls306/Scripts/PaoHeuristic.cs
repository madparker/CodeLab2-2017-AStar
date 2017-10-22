using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaoHeuristic : HueristicScript {
	public override float Hueristic(int x, int y, Vector3 start, Vector3 goal, GridScript gridScript){
		float heuristic = gridScript.costs[0] * Mathf.Abs(x - goal.x) + Mathf.Abs(y - goal.y); 
 		return heuristic;
	}
}


