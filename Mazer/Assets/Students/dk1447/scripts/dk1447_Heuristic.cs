﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dk1447_Heuristic : HueristicScript {

	public override float Hueristic(int x, int y, Vector3 start, Vector3 goal, GridScript gridScript)
	{

		float priority;
		float topSum = 0;
		float bottomSum = 0;
		float rightSum = 0;
		float leftSum = 0;
		float nodesCheck = 0;
        GameObject[,] pos = gridScript.GetGrid();

		if (x > 0 && x < gridScript.gridWidth && y + 1 > 0 && y + 1 < gridScript.gridHeight){
			topSum = gridScript.GetMovementCost(pos[x, y + 1]);
			nodesCheck++;
			}
		if (x > 0 && x < gridScript.gridWidth && y - 1 > 0 && y - 1 < gridScript.gridHeight){
			bottomSum = gridScript.GetMovementCost(pos[x, y - 1]);
			nodesCheck++;
			}
		if (x - 1 > 0 && x - 1 < gridScript.gridWidth && y > 0 && y < gridScript.gridHeight){
			leftSum = gridScript.GetMovementCost(pos[x - 1, y]);
			nodesCheck++;
			}
		if (x + 1 > 0 && x + 1 < gridScript.gridWidth && y > 0 && y < gridScript.gridHeight){
			rightSum = gridScript.GetMovementCost(pos[x + 1, y]);
			nodesCheck++;
			}

		priority = (Mathf.Abs(start.x - goal.x)) + (Mathf.Abs(start.y - goal.y)) * gridScript.costs[0];
		float otherSum = (topSum + bottomSum + leftSum + rightSum) / (nodesCheck * 5);
		priority += (otherSum + gridScript.GetMovementCost(pos[x, y]));
//		Debug.Log("priority = " + priority);
		       
		return priority;
	}

}
