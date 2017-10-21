using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lrAstarPathmaker : AStarScript {

    protected override void InitAstar(Path path) {

        lrGridScript lrGridS = GameObject.Find("Grid").GetComponent<lrGridScript>();
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

        while (frontier.Count != 0) {
            exploredNodes++;
            current = frontier.Dequeue();
            if (!visited.Contains(current)) {
                if (current.Equals(goal)) {
                    //Debug.Log("GOOOAL!");
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
                //                pos[(int)current.x, (int)current.y].transform.localScale =
                //                    Vector3.Scale(pos[(int)current.x, (int)current.y].transform.localScale, new Vector3(.8f, .8f, .8f));

                for (int x = -1; x < 2; x += 2) {
                    AddNodesToFrontier((int)current.x + x, (int)current.y);
                    }
                for (int y = -1; y < 2; y += 2) {
                    AddNodesToFrontier((int)current.x, (int)current.y + y);
                    }
                }
            //else Debug.Log("Tried to add the same node twice");
            }

        current = goal;

        //LineRenderer line = GetComponent<LineRenderer>();

        int i = 0;
        float score = 0;

        while (!current.Equals(start)) {
            //line.positionCount++;

            GameObject go = pos[(int)current.x, (int)current.y];
            path.Insert(0, go, new Vector3((int)current.x, (int)current.y));

            lrGridS.SetTileToPath((int)current.x, (int)current.y);

            current = cameFrom[current];

            Vector3 vec = Util.clone(go.transform.position);
            vec.z = -1;

            //line.SetPosition(i, vec);
            score += gridScript.GetMovementCost(go);
            i++;
            }

        path.Insert(0, pos[(int)current.x, (int)current.y]);
        lrGridS.SetTileToPath((int)current.x, (int)current.y);
        path.nodeInspected = exploredNodes;

        //Debug.Log(path.pathName + " Terrian Score: " + score);
        //Debug.Log(path.pathName + " Nodes Checked: " + exploredNodes);
        //Debug.Log(path.pathName + " Total Score: " + (score + exploredNodes));
        }
	
}
