using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HueristicScript_nhk274 : HueristicScript {

	public override float Hueristic (int x, int y, Vector3 start, Vector3 goal, GridScript gridScript) //we are giving it a gridScript because we may want to use data from this script
	{
		//x and y are giving us the position of the node which we are checking (again, this hueristic is checking how well this node fits along the best possible path to the goal. It's finding that line.
		//start is just the position of our start at the begining of game
		//goal is, fucking obvious
		//gridScript is a declration so we can use shit from gridScript

		//this is going to find the line between the node we're currently checking and the goal. This helps us properly prioritize the current node. 

		float positionPriority;
		positionPriority = (Mathf.Abs(goal.x - x) + Mathf.Abs(goal.y - y)) * gridScript.costs[0]; 
		//in GridScript, the cost was just found in GetMovementCost. The node we are on has already been assigned to position 0, the first position. 
		return positionPriority;
	}
}
