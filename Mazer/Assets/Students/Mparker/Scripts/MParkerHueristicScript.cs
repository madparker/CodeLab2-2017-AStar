using UnityEngine;
using System.Collections;

public class MParkerHueristicScript : HueristicScript {

	float ManHattanHeuristic(Vector3 a, Vector3 b){
		//Manhattan distance on a square grid
		return (Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y));
	}
	
	public override float Hueristic(int x, int y, Vector3 start, Vector3 goal, GridScript gridScript){

		return ManHattanHeuristic(new Vector3(x, y), goal);

		GameObject[,] grid = gridScript.GetGrid();

		Vector3 vec = grid[x, y].transform.position;

		float score = 0;

		for(int dx = -1; dx <= 1; dx++){
			for(int dy = -1; dy <= 1; dy++){
				int nx = dx + x;
				int ny = dy + y;

				if(nx < 0 || nx >= grid.GetLength(0) ||
				   ny < 0 || ny >= grid.GetLength(1)){
					score += 0;
				} else {
					score += gridScript.GetMovementCost(grid[nx, ny])/
						((ManHattanHeuristic(grid[nx, ny].transform.position, goal)));
				}
			}
		}

		return score;

	}
}
