using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreyHeuristic : HueristicScript {
    //THIS IS THE BEST HEURISTIC EVAR
    public override float Hueristic(int x, int y, Vector3 start, Vector3 goal, GridScript gridScript) {
        Debug.Log("fucked if I know");
        return (Mathf.Abs(x - goal.x) + Mathf.Abs(y - goal.y)) * gridScript.costs[0] * 10000000000000000000000000000000000000f;
        //return 0;

	}
}
