using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hang;

namespace Hang {

	public class HR_HueristicScript : HueristicScript {

		public override float Hueristic(int x, int y, Vector3 start, Vector3 goal, GridScript gridScript){
//			return Mathf.Abs (x - start.x) + Mathf.Abs (y - start.y); //16.72, 401
//			return (new Vector2 (x, y) - (Vector2)goal).magnitude; //16.72, 330
//			return ((new Vector2 (x, y) - (Vector2)goal).magnitude + (new Vector2 (x, y) - (Vector2)start).magnitude)/2; //16.72, 308

			//fastest?
			return Mathf.Sqrt((new Vector2 (x, y) - (Vector2)goal).magnitude * (new Vector2 (x, y) - (Vector2)start).magnitude); //16.23, 308



			float t_cost = gridScript.GetMovementCost (gridScript.GetGrid () [x, y]);
			return (new Vector2 (x, y) - (Vector2)goal).magnitude * t_cost; //19.47501, 237
//			return (new Vector2 (x, y) - (Vector2)start).magnitude * t_cost; //19.47501, 276


//			float t_cost = gridScript.GetMovementCost (gridScript.GetGrid () [x, y]);
//			return (Mathf.Abs (x - goal.x) + Mathf.Abs (y - goal.y)) * (new Vector2 (x, y) - (Vector2)goal).magnitude; //60.65499, 30
		}
	}
}
