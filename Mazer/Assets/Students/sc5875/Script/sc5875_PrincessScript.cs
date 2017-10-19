using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc5875_PrincessScript : HueristicScript {
	float ManHattanHeuristic(Vector3 a, Vector3 b){
		//Manhattan distance on a square grid
		return (Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y));
	}
	
	public override float Hueristic(int x, int y, Vector3 start, Vector3 goal, GridScript gridScript){
		// GameObject[,] grid = gridScript.GetGrid();
		return  gridScript.costs[0] * ManHattanHeuristic(new Vector3(x, y), goal);
		//比起"ManHattanHeuristic(new Vector3(x, y), goal)"更接近於真實的最短路徑

		// GameObject[,] grid = gridScript.GetGrid();

		// Vector3 vec = grid[x, y].transform.position;

		// float score = 0;

		// for(int dx = -1; dx <= 1; dx++){
		// 	for(int dy = -1; dy <= 1; dy++){
		// 		int nx = dx + x;
		// 		int ny = dy + y;

		// 		if(nx < 0 || nx >= grid.GetLength(0) ||
		// 		   	ny < 0 || ny >= grid.GetLength(1)){
		// 			score += 0;
		// 		} else {
		// 			score += gridScript.costs[0] * gridScript.GetMovementCost(grid[nx, ny])/
		// 				((ManHattanHeuristic(grid[nx, ny].transform.position, goal)));
		// 		}
		// 	}
		// }

		// return score;
	}
}
