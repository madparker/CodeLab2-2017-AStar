using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hang;

namespace Hang {

	public class HR_HueristicScript : HueristicScript {

		struct HR_PointAndCost {
			public Vector3 point;
			public float cost;
		}

		float ManHattanHeuristic(Vector3 a, Vector3 b){
			//Manhattan distance on a square grid
			return (Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y));
		}

		public override float Hueristic(int x, int y, Vector3 start, Vector3 goal, GridScript gridScript){
			//from matt
//			if (x == goal.x && y == goal.y) {
//				Debug.LogError ("0");
////				return 0;
//			}
//
//			GameObject[,] grid = gridScript.GetGrid();
//
//			Vector3 vec = grid[x, y].transform.position;
//
////			return gridScript.GetMovementCost (grid [x, y]) /
////			((ManHattanHeuristic (grid [x, y].transform.position, goal)));
//
//			float score = 0;
//
//			for(int dx = -1; dx <= 1; dx++){
//				for(int dy = -1; dy <= 1; dy++){
//					if (dx == 0 || dy == 0) {
//
//						int nx = dx + x;
//						int ny = dy + y;
//
//						if (nx < 0 || nx >= grid.GetLength (0) ||
//						  ny < 0 || ny >= grid.GetLength (1)) {
//							score += 0;
//						} else {
//							score += gridScript.GetMovementCost (grid [nx, ny]) /
//							((ManHattanHeuristic (grid [nx, ny].transform.position, goal)));
//						}
//					}
//				}
//			}
//			return score + 0f;

			//greedy best first
//			return (Mathf.Abs (x - goal.x) + Mathf.Abs (y - goal.y)) * gridScript.costs[0]; //290, (25,2)385
//			return (Mathf.Abs (x - goal.x) + Mathf.Abs (y - goal.y)) * gridScript.costs [0] + 0.000001f; //287


			//I did my best
			if (x == goal.x && y == goal.y) {
//				Debug.LogError ("0");
				return 0;
			}

			GameObject[,] grid = gridScript.GetGrid();

			List<Vector2> t_currentShortList = new List<Vector2> ();
			t_currentShortList.Add (new Vector2 (x - 1, y));
			t_currentShortList.Add (new Vector2 (x + 1, y));
			t_currentShortList.Add (new Vector2 (x, y - 1));
			t_currentShortList.Add (new Vector2 (x, y + 1));

			for (int i = 0; i < t_currentShortList.Count; i++) {
				if (t_currentShortList[i].x < 0 || t_currentShortList[i].x >= grid.GetLength (0) ||
					t_currentShortList[i].y < 0 || t_currentShortList[i].y >= grid.GetLength (1)) {
					t_currentShortList.RemoveAt (i);
					i--;
				}
			}

			float t_currentShortValue = GetCost ((Vector3)t_currentShortList [0], gridScript);
			for (int i = 1; i < t_currentShortList.Count; i++) {
				t_currentShortValue = Mathf.Min (t_currentShortValue, GetCost ((Vector3)t_currentShortList [i], gridScript));
			}

			for (int i = 0; i < t_currentShortList.Count; i++) {
				if (GetCost ((Vector3)t_currentShortList [i], gridScript) != t_currentShortValue) {
					t_currentShortList.RemoveAt (i);
					i--;
				}
			}

			List<Vector2> t_endShortList = new List<Vector2> ();
			t_endShortList.Add (new Vector2 (goal.x - 1, goal.y));
			t_endShortList.Add (new Vector2 (goal.x + 1, goal.y));
			t_endShortList.Add (new Vector2 (goal.x, goal.y - 1));
			t_endShortList.Add (new Vector2 (goal.x, goal.y + 1));

			for (int i = 0; i < t_endShortList.Count; i++) {
				if (t_endShortList[i].x < 0 || t_endShortList[i].x >= grid.GetLength (0) ||
					t_endShortList[i].y < 0 || t_endShortList[i].y >= grid.GetLength (1)) {
					t_endShortList.RemoveAt (i);
					i--;
				}
			}

			float t_endShortValue = GetCost ((Vector3)t_endShortList [0], gridScript);
			for (int i = 1; i < t_endShortList.Count; i++) {
				t_endShortValue = Mathf.Min (t_endShortValue, GetCost ((Vector3)t_endShortList [i], gridScript));
			}

			for (int i = 0; i < t_endShortList.Count; i++) {
				if (GetCost ((Vector3)t_endShortList [i], gridScript) != t_endShortValue) {
					t_endShortList.RemoveAt (i);
					i--;
				}
			}

			float t_ManHattanHeuristic = ManHattanHeuristic (t_currentShortList [0], t_endShortList [0]);
			for (int i = 0; i < t_currentShortList.Count; i++) {
				for (int j = 0; j < t_endShortList.Count; j++) {
					t_ManHattanHeuristic = Mathf.Min (t_ManHattanHeuristic, ManHattanHeuristic (t_currentShortList [0], t_endShortList [0]));
				}
			}

			return (t_ManHattanHeuristic * gridScript.costs [0] + t_currentShortValue + t_endShortValue + GetCost (goal, gridScript)); // 291
		}

		private float GetCost (Vector3 point, GridScript gridScript) {
			return gridScript.GetMovementCost (gridScript.GetGrid () [(int)point.x, (int)point.y]);
		}

		private float TieBreakingScaling (GameObject[,] g_grid) {
			return 1 + 1.0f / (g_grid.GetLength (0) * g_grid.GetLength (1));
		}

		private float TieBreakingCrossProduct (Vector3 g_cur, Vector3 g_start, Vector3 g_goal) {
			float dx1 = g_cur.x - g_goal.x;
			float dy1 = g_cur.y - g_goal.y;
			float dx2 = g_start.x - g_goal.x;
			float dy2 = g_start.y - g_goal.y;
			float cross = Mathf.Abs (dx1 * dy2 - dx2 * dy1);
			return cross * 0.001f;
		}
	}
}
