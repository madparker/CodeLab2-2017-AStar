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
    //protected List<Vector3> goal = new List<Vector3>();

	public Path path;
    public List<Path> paths = new List<Path>();

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

    protected virtual void InitAstar(Path path)
    {
        this.path = path;

        start = gridScript.start[hueristic.id];
        goal = gridScript.goal;
        /*for (int j = 0; j < gridScript.goal.Count; j++)
        {
            goal[j] = gridScript.goal[j];
        }*/

        gridWidth = gridScript.gridWidth;
        gridHeight = gridScript.gridHeight;

        pos = gridScript.GetGrid();

        frontier = new PriorityQueue<Vector3>();
        //for (int k = 0; k < goal.Count; k++)
        //{
            //this.paths[k] = path;
            frontier.Enqueue(start, 0);

            cameFrom.Add(start, start);
            costSoFar.Add(start, 0);

            int exploredNodes = 0;

            while (frontier.Count != 0)
            {
                exploredNodes++;
                current = frontier.Dequeue();

                visited.Add(current);

                pos[(int)current.x, (int)current.y].transform.localScale =
                    Vector3.Scale(pos[(int)current.x, (int)current.y].transform.localScale, new Vector3(.8f, .8f, .8f));

                if (current.Equals(goal))//goal[k]
                {
                    Debug.Log("GOOOAL!");
                    break;
                }

                for (int x = -1; x < 2; x += 2)
                {
                    //if(!visited.Contains(new Vector3((int)current.x + x, (int)current.y)))
                    AddNodesToFrontier((int)current.x + x, (int)current.y);
                }
                for (int y = -1; y < 2; y += 2)
                {
                    //if(!visited.Contains(new Vector3((int)current.x, (int)current.y + y)))
                    AddNodesToFrontier((int)current.x, (int)current.y + y);
                }
            }

            current = goal;//goal[k]

            LineRenderer line = GetComponent<LineRenderer>();

            int i = 0;
            float score = 0;

            while (!current.Equals(start))
            {
                line.positionCount++;

                GameObject go = pos[(int)current.x, (int)current.y];
                path.Insert(0, go, new Vector3((int)current.x, (int)current.y));//paths[k]

                current = cameFrom[current];

                Vector3 vec = Util.clone(go.transform.position);
                vec.z = -1;

                line.SetPosition(i, vec);
                score += gridScript.GetMovementCost(go);
                i++;
            }

            path.Insert(0, pos[(int)current.x, (int)current.y]);//paths[k]
            path.nodeInspected = exploredNodes;//paths[k]


            //this.path = paths[k];

            Debug.Log(path.pathName + " Terrian Score: " + score);
            Debug.Log(path.pathName + " Nodes Checked: " + exploredNodes);
            Debug.Log(path.pathName + " Total Score: " + (score + exploredNodes));
        //}

  
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


    /*
    void AddNodesToFrontier(int x, int y, int index)
    {
        if (x >= 0 && x < gridWidth &&
           y >= 0 && y < gridHeight)
        {
            Vector3 next = new Vector3(x, y);
            float new_cost = costSoFar[current] + gridScript.GetMovementCost(pos[x, y]);
            if (!costSoFar.ContainsKey(next) || new_cost < costSoFar[next])
            {
                costSoFar[next] = new_cost;
                float priority = new_cost + hueristic.Hueristic(x, y, start, goal[index], gridScript);

                frontier.Enqueue(next, priority);
                cameFrom[next] = current;
            }
        }
    }
    */
    // Update is called once per frame
    void Update () {
	
	}
}
