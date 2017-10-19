using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//somewhere in here there is something that can lower the amount of nodes checked
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
	protected Dictionary<Vector3, Vector3> cameFrom = new Dictionary<Vector3, Vector3>();
	protected Dictionary<Vector3, float> costSoFar = new Dictionary<Vector3, float>();
	protected Vector3 current;

	List<Vector3> visited = new List<Vector3>();

	// Use this for initialization
	protected virtual void Start () {
		InitAstar();
	}

	protected virtual void InitAstar(){
		InitAstar(new Path(hueristic.gameObject.name, gridScript));
	}

	protected virtual void InitAstar(Path path){
		this.path = path;

		start = gridScript.start;
		goal = gridScript.goal;
		
		gridWidth = gridScript.gridWidth;
		gridHeight = gridScript.gridHeight;

		pos = gridScript.GetGrid();

		frontier = new PriorityQueue<Vector3>();
		frontier.Enqueue(start, 0);

		cameFrom.Add(start, start);
		costSoFar.Add(start, 0);

		int exploredNodes = 0;

		while(frontier.Count != 0){ 
			exploredNodes++;
			current = frontier.Dequeue(); 
            if (!visited.Contains(current))
            {
                if (current.Equals(goal))
                {
                    Debug.Log("GOOOAL!");
                    break;
                }
                visited.Add(current);

                #region funny diaganol code
                //if (!visited.Contains(current))
                //{
                //    visited.Add(current);
                //}
                //pos[(int)visited[visited.Count - 1].x, (int)visited[visited.Count - 1].y].transform.localScale =
                //    Vector3.Scale(pos[(int)visited[visited.Count - 1].x, (int)visited[visited.Count - 1].y].transform.localScale, new Vector3(.8f, .8f, .8f));

                //for (int x = -1; x < 2; x += 2)
                //{
                //    AddNodesToFrontier((int)visited[visited.Count - 1].x + x, (int)visited[visited.Count - 1].y);
                //}
                //for (int y = -1; y < 2; y += 2)
                //{
                //    AddNodesToFrontier((int)visited[visited.Count - 1].x, (int)visited[visited.Count - 1].y + y);
                //}
                #endregion
                pos[(int)current.x, (int)current.y].transform.localScale =
                    Vector3.Scale(pos[(int)current.x, (int)current.y].transform.localScale, new Vector3(.8f, .8f, .8f));

                for (int x = -1; x < 2; x += 2)
                {
                    AddNodesToFrontier((int)current.x + x, (int)current.y);
                }
                for (int y = -1; y < 2; y += 2)
                {
                    AddNodesToFrontier((int)current.x, (int)current.y + y);
                }
            }
            else Debug.Log("Tried to add the same node twice");
        }

		current = goal;

		LineRenderer line = GetComponent<LineRenderer>();

		int i = 0;
		float score = 0;

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

	void AddNodesToFrontier(int x, int y){
		if(x >=0 && x < gridWidth && 
		   y >=0 && y < gridHeight)
		{
			Vector3 next = new Vector3(x, y);
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

	// Update is called once per frame
	void Update () {
	
	}
}
