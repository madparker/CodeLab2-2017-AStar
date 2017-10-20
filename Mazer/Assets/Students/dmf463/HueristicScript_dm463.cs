using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HueristicScript_dm463 : HueristicScript {

    public override float Hueristic(int x, int y, Vector3 start, Vector3 goal, GridScript gridScript)
    {
        //we want low heuristic for best possible score
        //exploring lowest priority first gets you there quicker
        /*
         * x = x pos for node being checked
         * y = y pos for node being checked
         * start = startPos
         * goal = goalPos
         * gridScript = gridScript
         * abs(a.x - b.x) + abs(a.y - b.y)
         * (Mathf.Abs(start.x - goal.x)) + (Mathf.Abs(start.y - goal.y))
         * 
         */

        float priority;
        float topSum = 0;
        float bottomSum = 0;
        float rightSum = 0;
        float leftSum = 0;
        float nodesCheck = 0;
        GameObject[,] pos = gridScript.GetGrid();

        //get the movementCost for each node above, below, left, and right of the main node
        //add those together and then divide that by the nodes checked to get the average
        //but also divide the nodesChecked by 5 because that's the number that gave me the lowest nodes
        if (x > 0 && x < gridScript.gridWidth && y + 1 > 0 && y + 1 < gridScript.gridHeight)
        {
            topSum = gridScript.GetMovementCost(pos[x, y + 1]);
            nodesCheck++;
        }
        if (x > 0 && x < gridScript.gridWidth && y - 1 > 0 && y - 1 < gridScript.gridHeight)
        {
            bottomSum = gridScript.GetMovementCost(pos[x, y - 1]);
            nodesCheck++;
        }
        if (x - 1 > 0 && x - 1 < gridScript.gridWidth && y > 0 && y < gridScript.gridHeight)
        {
            leftSum = gridScript.GetMovementCost(pos[x - 1, y]);
            nodesCheck++;
        }
        if (x + 1 > 0 && x + 1 < gridScript.gridWidth && y > 0 && y < gridScript.gridHeight)
        {
            rightSum = gridScript.GetMovementCost(pos[x + 1, y]);
            nodesCheck++;
        }

        priority = (Mathf.Abs(start.x - goal.x)) + (Mathf.Abs(start.y - goal.y)) * gridScript.costs[0];
        float otherSum = (topSum + bottomSum + leftSum + rightSum) / (nodesCheck * 5);
        priority += (otherSum + gridScript.GetMovementCost(pos[x, y]));
        //Debug.Log("priority = " + priority);
        
        return priority;
    }

}
