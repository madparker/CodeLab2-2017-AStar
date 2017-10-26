using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class AstarPathfinding_dk1447 : MonoBehaviour {

	Grid_dk1447 grid; //reference to our grid

	public Transform seeker, target;

	void Awake(){
		grid = GetComponent<Grid_dk1447>(); //initialize grid
	}

	void Update(){
		FindPath(seeker.position, target.position);
	}


	void FindPath (Vector3 startPos, Vector3 targetPos){ //takes vector 3 coords of our start and target positions

		Stopwatch sw = new Stopwatch();
		sw.Start();

		Node_dk1447 startNode = grid.NodeFromWorldPoint(startPos); //gets the correct nodes for these world points
		Node_dk1447 targetNode = grid.NodeFromWorldPoint(targetPos);

		Heap_dk1447<Node_dk1447> openNodeSet = new Heap_dk1447<Node_dk1447>(grid.MaxSize); //creates a list of nodes that have not been checked yet
		HashSet<Node_dk1447> closedNodeSet = new HashSet<Node_dk1447>(); //set of nodes that have been checked

		openNodeSet.Add(startNode);//adds starting node to open set

		while(openNodeSet.Count > 0){ //while there are nodes in our open set
			Node_dk1447 currentNode = openNodeSet.RemoveFirst(); //set current node as the first in the list
								/*for (int i = 1; i < openNodeSet.Count; i++) { //cycle through nodes in open set
									if(openNodeSet[i].fCost < currentNode.fCost || openNodeSet[i].fCost == currentNode.fCost && openNodeSet[i].hCost < currentNode.hCost){ //if the node's fcost is less than the current node's fcost
										currentNode = openNodeSet[i]; //reassign the new node as the current node
									}
								}

								openNodeSet.Remove(currentNode); //removes the current node from the open set*/
			closedNodeSet.Add(currentNode); //and adds it to the closed set

			if (currentNode == targetNode){ //if the current node is the target node
				sw.Stop();
				print ("Path found: " + sw.ElapsedMilliseconds + " ms");
				RetracePath(startNode, targetNode); //retrace our path back to start
				return;
			}

			foreach (Node_dk1447 neighbor in grid.GetNeighborNodes(currentNode)){ // for each of the neighbors of the current node
				if (!neighbor.traversable || closedNodeSet.Contains(neighbor)){ //if the neighbor node is not traversable or is already in the closed list
					continue; //ignore this node
				}

				int newMoveCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor); //the cost to get to a new neighbor node

				if(newMoveCostToNeighbor < neighbor.gCost || !openNodeSet.Contains(neighbor)){ //if the new path to this node is shorter, or if the node isn't already in the open list
					neighbor.gCost = newMoveCostToNeighbor; //sets (or updates) the gCost 
					neighbor.hCost = GetDistance(neighbor, targetNode); //set the hCost
					neighbor.parent = currentNode; //sets parent of neighbor to the current node

					if (!openNodeSet.Contains(neighbor)){ //if the neighbor is not in the open set
						openNodeSet.Add(neighbor); //add it
					} else{
						openNodeSet.UpdateItem(neighbor);
					}
				}
			}
		}
	}

	void RetracePath(Node_dk1447 startNode, Node_dk1447 endNode){ //method for tracing our path back from the end to the start
		List<Node_dk1447> path = new List<Node_dk1447>(); //list of nodes in our path
		Node_dk1447 currentNode = endNode; //starts us at the end node

		while (currentNode != startNode){ //until we get back to the start node
			path.Add(currentNode); // add the current node to the path
			currentNode = currentNode.parent; //set the current node as the parent of this node
		}

		path.Reverse(); //by retracing, our path is backwards, so we need to reverse it

		grid.path = path;
	}

	int GetDistance (Node_dk1447 nodeA, Node_dk1447 nodeB){ //get the distance between two nodes in steps
		int distanceX = Mathf.Abs(nodeA.gridX - nodeB.gridX); //absolute values of distance between nodes along x and y
		int distanceY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

		if (distanceX > distanceY){ //if the distance is greater along the x axis
			return 14 * distanceY + 10 * (distanceX - distanceY); //return the required diagonal moves (value 14) first, then add the remaining required orthagonal moves (value 10)
		} else { //if the distance is greater along the y axis
			return 14 * distanceX + 10 * (distanceY - distanceX); //same but with inverted x and y
		}
	}
}
