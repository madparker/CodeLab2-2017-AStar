using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstarAI : AStarScript {


	public PlayerScript player;
//	public PlayerScript goalPlayer;
	private GameManager gameManager;

	protected override void Start (){
		//base.Start ();
		gameManager = GameObject.Find ("[GameManager]").GetComponent<GameManager> ();
		InitAstar ();
	}

	protected override void InitAstar(){
		Debug.Log ("InitAstar");
		InitAstar(new Path(hueristic.gameObject.name, gameManager));
	}

	protected override void InitAstar (Path path){
		this.path = path;
		player = GetComponent<PlayerScript> ();
		start = gameManager.Pos2d(player);
		goal = gameManager.Pos2d(GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerScript> ());

		gridWidth = gameManager.rowLength;
		gridHeight = gameManager.columnLength;

		arrayPos = gameManager.gridArray;

		frontier = new PriorityQueue<Vector3>();
		frontier.Enqueue(start, 0);

		Debug.Log (start + ", " + start);
		cameFrom.Add(start, start);
		costSoFar.Add(start, 0);


		int exploredNodes = 0;

		while(frontier.Count != 0){
			exploredNodes++;
			current = frontier.Dequeue();

			visited.Add(current);


			if(current.Equals(goal)){
				Debug.Log("GOOOAL!");
				break;
			}

			for(int x = -1; x < 2; x+=2){
				AddNodesToFrontier((int)current.x + x, (int)current.y);
			}
			for(int y = -1; y < 2; y+=2){
				AddNodesToFrontier((int)current.x, (int)current.y + y);
			}
		}

		current = goal;

//		LineRenderer line = GetComponent<LineRenderer>();

		int i = 0;
		float score = 0;

		while(!current.Equals(start)){
//			line.positionCount++;


			Transform go = arrayPos[(int)current.x, (int)current.y];
			Vector3 worldPos = gameManager.GetWorldPosition((int)current.x, (int)current.y);
			path.Insert(0, go.gameObject, worldPos);

			current = cameFrom[current];

			Vector3 vec = Util.clone(go.position);
			vec.y = -1;

//			line.SetPosition(i,vec  );
			score += gameManager.GetMovementCost(go.gameObject);
			i++;
		}

		path.Insert(0, arrayPos[(int)current.x, (int)current.y].gameObject );
		path.nodeInspected = exploredNodes;


		Debug.Log(path.pathName + " Terrian Score: " + score);
		Debug.Log(path.pathName + " Nodes Checked: " + exploredNodes);
		Debug.Log(path.pathName + " Total Score: " + (score + exploredNodes));
	}

	protected override void AddNodesToFrontier(int x, int y){
		if(x >=0 && x < gridWidth && 
			y >=0 && y < gridHeight)
		{
			Vector3 next = new Vector3(x, y);
			float new_cost = costSoFar[current] + gameManager.GetMovementCost(arrayPos[x, y].gameObject);
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
