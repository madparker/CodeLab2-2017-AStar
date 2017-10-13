using UnityEngine;
using System.Collections;

public class AdjustHueristic : HueristicScript {

	public float avoidDamage;
	public float shortestRoute;

	public override float Hueristic(int x, int y, Vector3 start, Vector3 goal, GridScript gridScript){
		
		GameObject[,] grid = gridScript.GetGrid();
		
		float movementCost = gridScript.GetMovementCost(grid[x,y]);
		float damage = ((DamageGridScript)gridScript).GetDamageCost(grid[x,y]);
		
		float distance = Vector2.Distance (start, goal);

		float cost = damage * avoidDamage + movementCost * shortestRoute;
		
		return cost;
		
	}
}
