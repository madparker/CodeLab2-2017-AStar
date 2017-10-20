using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreyBadHeuristicScript : HueristicScript {
	//THIS IS THE WORST HEURISTIC EVAR
	public override float Hueristic(int x, int y, Vector3 start, Vector3 goal, GridScript gridScript) {
		Debug.Log("fucked if I know");
        //return (Mathf.Abs(x - goal.x) + Mathf.Abs(y - goal.y)) * gridScript.costs[0];
                                                                           //* -9999999999999999999999999999999999999f;
		return 0;

	}
}
