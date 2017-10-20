using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStarScript : MonoBehaviour {

	public GridScript gridScript;
	public HueristicScript hueristic;
	protected int gridWidth;
	protected int gridHeight;

	GameObject[,] pos;
	protected Vector2 start;
	protected Vector2 goal;
	public Path path;

	protected PriorityQueue<Vector2> frontier;
	protected Dictionary<Vector2, Vector2> cameFrom = new Dictionary<Vector2, Vector2>();
	protected Dictionary<Vector2, float> costSoFar = new Dictionary<Vector2, float>();
	protected Vector2 current;

	// Use this for initialization
	protected virtual void Start () {
		InitAstar(new Path(hueristic.gameObject.name, gridScript));
	}

	protected virtual void InitAstar(Path path){
		
		this.path = path;
		//get start, goal , width and height from the gridscript
		start = gridScript.start;
		goal = gridScript.goal;
		gridWidth = gridScript.gridWidth;
		gridHeight = gridScript.gridHeight;
		//the array of the grid
		pos = gridScript.GetGrid();

		frontier = new PriorityQueue<Vector2>();
		frontier.Enqueue(start, 0);
		cameFrom.Add(start, start);
		costSoFar.Add(start, 0);

		int exploredNodes = 0;

		//get frontier nodes
		while(frontier.Count != 0){

			exploredNodes++;
			//get the first one from frontier
			current = frontier.Dequeue();

			if(current.Equals(goal)){
				break;
			}

			AddNodesToFrontier((int)current.x - 1, (int)current.y);
			AddNodesToFrontier((int)current.x + 1, (int)current.y);
			AddNodesToFrontier((int)current.x, (int)current.y - 1);
			AddNodesToFrontier((int)current.x, (int)current.y + 1);

		}

		current = goal;

		float score = 0;

		while(!current.Equals(start)){
			
			GameObject go = pos[(int)current.x, (int)current.y];
			path.Insert(0, pos[(int)current.x, (int)current.y]);
			current = cameFrom[current];

			score += gridScript.GetMovementCost(go);
		}
			
		path.nodeInspected = exploredNodes;
		
		Debug.Log(path.pathName + " Terrian Score: " + score);
		Debug.Log(path.pathName + " Nodes Checked: " + exploredNodes);
		Debug.Log(path.pathName + " Total Score: " + (score + exploredNodes));
	}

	void AddNodesToFrontier(int x, int y){
		if(x >=0 && x < gridWidth && 
		   y >=0 && y < gridHeight)
		{
			Vector2 next = new Vector2(x, y);
			float new_cost = costSoFar[current] + gridScript.GetMovementCost(pos[x, y]);
			if(!costSoFar.ContainsKey(next) || new_cost < costSoFar[next])
			{
				costSoFar[next] = new_cost;
				float priority = new_cost + hueristic.Hueristic(x, y, start, goal, gridScript);

				frontier.Enqueue(next, priority);
				cameFrom[next] = current;
			}
		}
	}
}
