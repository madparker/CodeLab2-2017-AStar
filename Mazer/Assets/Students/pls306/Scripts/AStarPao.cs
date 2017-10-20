using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPao : AStarScript {

	GameObject[,]m_pos;

  	List<Vector3> m_visited = new List<Vector3>();

	protected override void Start(){
		base.Start();
	}
	protected override void InitAstar(Path path){
		this.path = path;

		start = gridScript.start;
		goal = gridScript.goal;
		
		gridWidth = gridScript.gridWidth;
		gridHeight = gridScript.gridHeight;

		m_pos = gridScript.GetGrid();

		frontier = new PriorityQueue<Vector3>();
		frontier.Enqueue(start, 0);

		cameFrom.Add(start, start);
		costSoFar.Add(start, 0);

		int exploredNodes = 0;

		while(frontier.Count != 0){ //while not frontier.empty();
			exploredNodes++; 
			current = frontier.Dequeue();

			m_visited.Add(current);

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

		LineRenderer line = GetComponent<LineRenderer>();

		int i = 0;
		float score = 0;

		while(!current.Equals(start)){
			line.positionCount++;
			
			GameObject go = m_pos[(int)current.x, (int)current.y];
			path.Insert(0, go, new Vector3((int)current.x, (int)current.y));

			current = cameFrom[current];

			Vector3 vec = Util.clone(go.transform.position);
			vec.z = -1;

			line.SetPosition(i, vec);
			score += gridScript.GetMovementCost(go);
			i++;
		}

		path.Insert(0, m_pos[(int)current.x, (int)current.y]);
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
			float new_cost = costSoFar[current] + gridScript.GetMovementCost(m_pos[x, y]);
			if(!costSoFar.ContainsKey(next) || new_cost < costSoFar[next])
			{
				costSoFar[next] = new_cost;
				float priority = new_cost + hueristic.Hueristic(x, y, start, goal, gridScript);
				//lower priority is better
				//lowest heuristic as possible

				frontier.Enqueue(next, priority);
				cameFrom[next] = current;
			}
		}
	}






}
