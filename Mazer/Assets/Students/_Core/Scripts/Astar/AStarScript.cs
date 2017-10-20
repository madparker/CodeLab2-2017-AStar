using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStarScript : MonoBehaviour {

	public bool check = true;

	public GridScript gridScript;
	public HueristicScript hueristic;

	protected int gridWidth;
	protected int gridHeight;

	GameObject[,] pos;

	//A Star stuff
	protected Vector3 start;
	protected Vector3 goal;

	public Path path;

	protected PriorityQueue<Vector3> frontier;
	protected Dictionary<Vector3, Vector3> cameFrom = new Dictionary<Vector3, Vector3>(); //maybe storing where we are now, and where we came from
	protected Dictionary<Vector3, float> costSoFar = new Dictionary<Vector3, float>(); //adds the current position and the cost so far, and has a list of all the positions we've checked and cost at that point
	protected Vector3 current; 

	List<Vector3> visited = new List<Vector3>();

	// Use this for initialization
	protected virtual void Start () {
		InitAstar();
	}

	protected virtual void InitAstar(){ //this functions runs only at start, so the first time it runs, we DO NOT take any inputs because we don't need to check anything yet
		InitAstar(new Path(hueristic.gameObject.name, gridScript)); 
	}

	protected virtual void InitAstar(Path path){
		this.path = path;

		start = gridScript.start; //start point
		goal = gridScript.goal; //goal point
		
		gridWidth = gridScript.gridWidth;
		gridHeight = gridScript.gridHeight;

		pos = gridScript.GetGrid();

		frontier = new PriorityQueue<Vector3>(); //frontier is a NEW PRIORITY QUEUE YAYAYAYAYAYY
		frontier.Enqueue(start, 0); //enter into queue 

		cameFrom.Add(start, start); //first time we're running this. It's start, and start because we didn't come from anywhere. It's taking in two Vector3's. 
		costSoFar.Add(start, 0);

		int exploredNodes = 0; //haven't explored any nodes yet

		while(frontier.Count != 0){ //as long as the frontier isn't empty, which would mean we were done
			exploredNodes++; //start exploring nodes
			current = frontier.Dequeue(); //taking current OUT of the priority queue

			visited.Add(current); //we add current to the our visited to our visited list

			pos[(int)current.x, (int)current.y].transform.localScale =  //this is what's running to help us visualize where it's checking. Not actually important to the A* algorithm 
				Vector3.Scale(pos[(int)current.x, (int)current.y].transform.localScale, new Vector3(.8f, .8f, .8f));

			if(current.Equals(goal)){ //if we are at the goal, we made it! DONE.
				Debug.Log("GOOOAL!");
				break;
			}
			
			for(int x = -1; x < 2; x+=2){ //we're using these for loops to check to the left, right, above, and below the point we're at
				AddNodesToFrontier((int)current.x + x, (int)current.y); //then we add these x and y positions into the priority queue if we decide that they are useful
			}
			for(int y = -1; y < 2; y+=2){
				AddNodesToFrontier((int)current.x, (int)current.y + y);
			}
		}

		current = goal;

		LineRenderer line = GetComponent<LineRenderer>();

		int i = 0;
		float score = 0; //score is a sum of all grid tiles along the path. We want that to be as low as possible. 

		while(!current.Equals(start)){
			line.positionCount++;
			
			GameObject go = pos[(int)current.x, (int)current.y];
			path.Insert(0, go, new Vector3((int)current.x, (int)current.y));

			current = cameFrom[current];

			Vector3 vec = Util.clone(go.transform.position);
			vec.z = -1;

			line.SetPosition(i, vec);
			score += gridScript.GetMovementCost(go);
			i++;
		}

		path.Insert(0, pos[(int)current.x, (int)current.y]);
		path.nodeInspected = exploredNodes;
		
		Debug.Log(path.pathName + " Terrian Score: " + score);
		Debug.Log(path.pathName + " Nodes Checked: " + exploredNodes);
		Debug.Log(path.pathName + " Total Score: " + (score + exploredNodes));
	}

	void AddNodesToFrontier(int x, int y){ //we're adding the x and y of each node in the frontier into the priority queue
		if(x >=0 && x < gridWidth && 
		   y >=0 && y < gridHeight)
		{ //this whole thing is checking if the new cost is useful basically, so if we should go to the next node
			Vector3 next = new Vector3(x, y);
			float new_cost = costSoFar[current] + gridScript.GetMovementCost(pos[x, y]);
			if(!costSoFar.ContainsKey(next) || new_cost < costSoFar[next]) //if we DO want to use the new node
			{
				costSoFar[next] = new_cost; //set our cost so far to the new general cost so we can move forward
				float priority = new_cost + hueristic.Hueristic(x, y, start, goal, gridScript); //priority is central to determining the best path
				//new cost is terrain speed, hueristic helps us determine if this node will follow our rule of thumb (in our hueristic) or not. We want our hueristic to prioritize nodes along the best possible path. 
				frontier.Enqueue(next, priority); //our frontier queue is ordered based on priority, so if we give a node a priority, it will REORDER THE QUEUE BASED ON THE NEW PRIORITY!
				cameFrom[next] = current;
			}
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
