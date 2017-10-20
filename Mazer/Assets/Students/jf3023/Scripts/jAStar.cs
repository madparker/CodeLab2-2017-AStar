using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jAStar : AStarScript
{
    public class Node
    {
        public readonly float x;
        public readonly float y;

        public Node(Vector2 position)
        {
            this.x = position.x;
            this.y = position.y;
        }

        public Node(int x, int y)
        {
            this.x = x;
            this.y = y;
        }


        public Node(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static readonly Node[] DIRS = new []
        {
            new Node(1,0), //RIGHT
            new Node(0,-1), //DOWN
            new Node(-1,0), //LEFT
            new Node(0,1), //UP

        };

        public IEnumerable<Node> Neighbors(Node id)
        {
            foreach(var dir in DIRS)
            {
                Node next = new Node(id.x + dir.x, id.y + dir.y);
                yield return next;
            }
        }
    }

    protected new Node start;
    protected new Node goal;

    protected new PriorityQueue<Node> frontier;
    protected new Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();
    protected new Dictionary<Node, float> costSoFar = new Dictionary<Node, float>();
    protected new Node current;

    GameObject[,] pos;
    List<Vector3> visited = new List<Vector3>();
    
    protected override void InitAstar(Path path)
    {
        this.path = path;

        start = new Node(gridScript.start);
        goal = new Node(gridScript.goal);

        gridWidth = gridScript.gridWidth;
        gridHeight = gridScript.gridHeight;

        pos = gridScript.GetGrid();

        frontier = new PriorityQueue<Node>();
        frontier.Enqueue(start, 0);

        cameFrom.Add(start, start);
        costSoFar.Add(start, 0);

        int exploredNodes = 0;

        while (frontier.Count != 0)
        {
            exploredNodes++;
            current = frontier.Dequeue();

            /*pos[(int)current.x, (int)current.y].transform.localScale =
                Vector3.Scale(pos[(int)current.x, (int)current.y].transform.localScale, new Vector3(.8f, .8f, .8f));*/

            if (current.Equals(goal))
            {
                Debug.Log("GOOOAL!");
                break;
            }

            foreach(var next in current.Neighbors(current))
            {
                AddNodesToFrontier(next);
            }
         
        }


    }


    void AddNodesToFrontier(Node next)
    {
        if (next.x >= 0 && next.x < gridWidth &&
           next.y >= 0 && next.y < gridHeight)
        {
            float new_cost = costSoFar[current] + gridScript.GetMovementCost(pos[(int)next.x,(int)next.y]);
            if (!costSoFar.ContainsKey(next) || new_cost < costSoFar[next])
            {
                costSoFar[next] = new_cost;
                float priority = new_cost + hueristic.Hueristic((int)next.x, (int)next.y, start, goal, gridScript);

                frontier.Enqueue(next, priority);
                cameFrom[next] = current;
            }
        }
    }
}