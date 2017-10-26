using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_dk1447 : MonoBehaviour {
	
	public Vector2 gridWorldSize; //size of grid in world space
	public float nodeRadius; //amount of space each node covers

	public LayerMask untraversableMask; //mask for checking traversibility

	Node_dk1447[,] grid; //2d array of nodes

	float nodeDiameter;
	int gridSizeX, gridSizeY;

	void Start(){
		nodeDiameter = nodeRadius*2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter); //sets the x and y sizes of the grid in terms of node count
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
		CreateGrid(); //calls method to create grid
	}


	void CreateGrid(){
		grid = new Node_dk1447[gridSizeX,gridSizeY];
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x/2 - Vector3.forward * gridWorldSize.y/2; //gets bottom left corner of world; we use forward because the y axis of our grid is along the z axis in world space

		for (int x = 0; x < gridSizeX; x++) { //cycles through x and y of grid
			for (int y = 0; y < gridSizeY; y++) {
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius); //gets coords for each point a node will occupy in world space
				bool traversable = !(Physics.CheckSphere(worldPoint, nodeRadius, untraversableMask)); //checks for collision with layermask, allows for traversal if there isn't one
				grid[x,y] = new Node_dk1447 (traversable, worldPoint); //creates new node in the array
			}
		}
	}

	void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y)); //draws a wire cube around the entire grid

		if(grid != null){ //if there is a grid
			foreach (Node_dk1447 n in grid){ //for each node in the grid
				Gizmos.color = (n.traversable)?Color.white:Color.red; //sets the gizmo color based on whether the node is traversible (white) or not (red)
				Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.1f)); //draw a cube at the node position
			}
		}
	}
}
