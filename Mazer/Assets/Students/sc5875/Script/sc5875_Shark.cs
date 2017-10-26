using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc5875_Shark : HueristicScript {
	public override float Hueristic(int x, int y, Vector3 start, Vector3 goal, GridScript gridScript){
		return Random.Range(0,500);
	}
}
