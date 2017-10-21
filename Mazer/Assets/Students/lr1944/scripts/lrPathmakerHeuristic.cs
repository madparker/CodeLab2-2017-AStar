using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lrPathmakerHeuristic : HueristicScript {

    public override float Hueristic(int x, int y, Vector3 start, Vector3 goal, GridScript gridScript) {
        
        float heur;
        // complete random pseudo math
        heur = (Mathf.Abs(goal.x - y* Random.Range(-900000000000f, 900000000000f)) +(goal.y * x)) * gridScript.costs[0]* Random.Range(-900000000000f, 900000000000f);

        return heur;

        }
}
