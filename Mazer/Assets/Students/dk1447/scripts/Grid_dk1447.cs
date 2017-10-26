using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_dk1447 : MonoBehaviour {

	public Transform player; //players transform

	public Vector2 gridWorldSize; //size of grid in world space
	public float nodeRadius; //amount of space each node covers

	public LayerMask untraversableMask; //mask for checking traversibility

	Node_dk1447[,] grid; //2d array of nodes

	float nodeDiameter; //diameter of nodes
	int gridSizeX, gridSizeY; //size of grid

	void Start(){
		nodeDiameter = nodeRadius*2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter); //sets the x and y sizes of the grid in terms of node count
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
		CreateGrid(); //calls method to create grid
	}


	void CreateGrid(){
		grid = new Node_dk1447[gridSizeX,gridSizeY]; //creates a new 2D grid array
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x/2 - Vector3.forward * gridWorldSize.y/2; //gets bottom left corner of world; we use forward because the y axis of our grid is along the z axis in world space

		for (int x = 0; x < gridSizeX; x++) { //cycles through x and y of grid
			for (int y = 0; y < gridSizeY; y++) {
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius); //gets coords for each point a node will occupy in world space
				bool traversable = !(Physics.CheckSphere(worldPoint, nodeRadius, untraversableMask)); //checks for collision with layermask, allows for traversal if there isn't one
				grid[x,y] = new Node_dk1447 (traversable, worldPoint, x, y); //creates new node in the array
			}
		}
	}

	public List<Node_dk1447> GetNeighborNodes(Node_dk1447 node){ //method to get the neighboring nodes of any given node
		List<Node_dk1447> neighbors = new List<Node_dk1447>(); //list to hold neighboring nodes

		for (int x = -1; x <= 1; x++) { //we iterate through a three by three grid centered on our node (0,0)
			for (int y = -1; y <= 1; y++) {
				if (x == 0 && y == 0){ //we don't want to include the center node
					continue; //so we skip it
				}
				int checkX = node.gridX + x; //get the position of our node on the grid and add x to it, getting the neighboring nodes position in the grid
				int checkY = node.gridY + y; //same for y

				if (checkX >= 0 && checkX < gridSizeX && checkY >=0 && checkY < gridSizeY ){ //confirm that these positions don't fall outside of the world grid
					neighbors.Add(grid[checkX,checkY]); //if they are within the grid, add them to neighbor list
				}
			}
		}
		return neighbors; //return the list of neighbor nodes
	}

	public Node_dk1447 NodeFromWorldPoint(Vector3 worldPosition){ //gets a node from a world position
		float percentX = (worldPosition.x + gridWorldSize.x/2) / gridWorldSize.x; //uses world position and size of grid to determine percent along axis
		float percentY = (worldPosition.z + gridWorldSize.y/2) / gridWorldSize.y; //remember, we use z in world position to represent the grids y axis
		percentX = Mathf.Clamp01(percentX); //clamps values between 0 and 1
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.RoundToInt((gridSizeX - 1) * percentX); //gets grid coords out of percentages
		int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
		return grid [x,y]; //returns grid coord
 	}

	public List<Node_dk1447> path; //list of nodes in our determined path

	void OnDrawGizmos(){ //scene view visualization method
		Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y)); //draws a wire cube around the entire grid

		if(grid != null){ //if there is a grid
			Node_dk1447 playerNode = NodeFromWorldPoint(player.position); //player node is equal to the node at the player position
			foreach (Node_dk1447 n in grid){ //for each node in the grid
				Gizmos.color = (n.traversable)?Color.white:Color.red; //sets the gizmo color based on whether the node is traversible (white) or not (red)
				if (playerNode == n){ //if the player is on the node
					Gizmos.color = Color.blue; //change color to blue
				}
				if (path != null){ //if there is a determined path
					if (path.Contains(n)){ //if the node we are looking at is in the path
						Gizmos.color = Color.black; //change color to black
					}
				}
				Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.1f)); //draw a cube at the node position
			}
		}
	}
}
