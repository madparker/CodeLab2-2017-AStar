using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HR_AStar : MonoBehaviour {

	private HR_Grid gridScript;
	public HR_FollowAStarScript followAStar;

	protected int gridWidth;
	protected int gridHeight;

	//A Star stuff
	protected Vector3 start;
	protected Vector3 goal;

	public HR_Path path;

	protected PriorityQueue<Vector3> frontier;
	protected Dictionary<Vector3, Vector3> cameFrom = new Dictionary<Vector3, Vector3>();
	protected Dictionary<Vector3, float> costSoFar = new Dictionary<Vector3, float>();
	protected Vector3 current;

	List<Vector3> visited = new List<Vector3>();

	private static HR_AStar instance = null;

	public static HR_AStar Instance {
		get { 
			return instance;
		}
	}

	void Awake () {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
		} else {
			instance = this;
		}
		followAStar = this.GetComponent<HR_FollowAStarScript> ();
	}

	// Use this for initialization
	protected virtual void Start () {
		gridScript = HR_Grid.Instance;

		gridWidth = gridScript.myGridWidth;
		gridHeight = gridScript.myGridHeight;

		InitAstar();
	}

	public virtual void InitAstar(){
		InitAstar (new HR_Path ("?"));
	}

	public virtual void InitAstar(HR_Path path){
		if (followAStar.GetMove () == true)
			return;

		//clean up
		cameFrom.Clear ();
		costSoFar.Clear ();
		visited.Clear ();


		this.path = path;

		start = gridScript.myCurrentPos;
		goal = gridScript.myTargetPos;



		frontier = new PriorityQueue<Vector3>();
		frontier.Enqueue(start, 0);

		cameFrom.Add(start, start);
		costSoFar.Add(start, 0);

		int exploredNodes = 0;

		while(frontier.Count != 0){
			exploredNodes++;

			current = frontier.Dequeue();

			if (visited.Contains (current))
				continue;

			visited.Add(current);

			//early exit
			if(current.Equals(goal)){
				Debug.Log("GOOOAL!");
				break;
			}


			//			if (cameFrom.ContainsValue (current))
			//				continue;

			//showing how many times the grid has been searched 
//			StartCoroutine(Scale( (int)current.x, (int)current.y, exploredNodes/20f));

			//Check Neighbors
			for(int x = -1; x < 2; x+=2){
				AddNodesToFrontier((int)current.x + x, (int)current.y);
			}
			for(int y = -1; y < 2; y+=2){
				AddNodesToFrontier((int)current.x, (int)current.y + y);
			}
		}

		current = goal;

		LineRenderer line = GetComponent<LineRenderer>();

		line.positionCount = 0;

		int i = 0;
//		float score = 0;

		//find the right path and draw it using line renderer
		while(!current.Equals(start)){
			line.positionCount++;

			HR_Block t_block = gridScript.myGridArray [(int)current.x, (int)current.y];
			path.Insert(0, t_block, new Vector3((int)current.x, (int)current.y));

			current = cameFrom[current];

			Vector3 vec = Util.clone (t_block.transform.position);
			vec.z = -1;

			line.SetPosition(i, vec);
//			score += gridScript.GetMovementCost(go);
			i++;
		}

		line.positionCount++;
		Vector3 t_beginningPos = gridScript.myGridArray [(int)start.x, (int)start.y].transform.position;
		line.SetPosition (i, new Vector3 (t_beginningPos.x, t_beginningPos.y, -1));

		path.Insert (0, gridScript.myGridArray [(int)current.x, (int)current.y]);
		path.nodeInspected = exploredNodes;

//		Debug.Log(path.pathName + " Terrian Score: " + score);
//		Debug.Log(path.pathName + " Nodes Checked: " + exploredNodes);
//		Debug.Log(path.pathName + " Total Score: " + (score + exploredNodes));

		followAStar.Move ();
	}

//	IEnumerator Scale (int x, int y, float g_delay) {
//		yield return new WaitForSeconds (g_delay);
//		pos[x, y].transform.localScale = 
//			Vector3.Scale(pos[x, y].transform.localScale, new Vector3(.8f, .8f, .8f));
//	}

	void AddNodesToFrontier(int x, int y){
		if(x >=0 && x < gridWidth && 
			y >=0 && y < gridHeight)
		{
			Vector3 next = new Vector3(x, y);
			float new_cost = costSoFar [current] + gridScript.myGridArray [x, y].myBlockSet.myCost;
			if(!costSoFar.ContainsKey(next) || new_cost < costSoFar[next])
			{
				costSoFar[next] = new_cost;

				float priority = new_cost + Heuristic (new Vector3 (x, y), goal);

				frontier.Enqueue(next, priority);
				cameFrom[next] = current;
			}
		}
	}

	float Heuristic(Vector3 a, Vector3 b){
//		return Vector3.Distance (a, b) * gridScript.GetMyBlockSet (HR_BlockSet.BlockType.Empty).myCost;
		//Manhattan distance on a square grid
		return (Mathf.Abs (a.x - b.x) + Mathf.Abs (a.y - b.y)) * gridScript.GetMyBlockSet (HR_BlockSet.BlockType.Empty).myCost;
	}

	private float TieBreakingScaling (int g_wid, int g_hei) {
		return 1 + 1.0f / (g_wid * g_hei);
	}


	// Update is called once per frame
	void Update () {

	}
}
