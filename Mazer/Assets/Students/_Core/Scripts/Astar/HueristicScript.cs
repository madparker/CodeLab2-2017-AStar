using UnityEngine;
using System.Collections;

public class HueristicScript : MonoBehaviour {
		
	public virtual float Hueristic(int x, int y, Vector3 start, Vector3 goal, GridScript gridScript){
		return Random.Range(0, 500);
	}
}
