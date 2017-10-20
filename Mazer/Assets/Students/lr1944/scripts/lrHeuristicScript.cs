using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lrHeuristicScript : HueristicScript {

	public override float Hueristic(int x, int y, Vector3 start, Vector3 goal, GridScript gridScript){
//		return 0;

		float heur;
		float currentHeur;

		heur = (Mathf.Abs (goal.x - x) + Mathf.Abs (goal.y - y)) * gridScript.costs [0];

//		Debug.Log (heur);
		return heur;


	}

}
