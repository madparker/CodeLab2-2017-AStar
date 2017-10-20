using UnityEngine;
using System.Collections;

public class HueristicScript : MonoBehaviour {
		
	public virtual float Hueristic(int x, int y, Vector3 start, Vector3 goal, GridScript gridScript){ //odd that this takes in a bunch of variables and doesn't seem to use any of them
		return Vector3.Distance(new Vector3(x, y, 0), new Vector3(goal.x, goal.y));
//		return Random.Range(0, 500); //heuristic is a rule of thumb when you're faced with uncertainty to help you make informed decisions
	} //in this case, heuristic returns a number. the lower the number, the better it thinks the move is. the higher, the less good the move is... right now we're just returning a random number hahate
}
