using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_dk1447 : IHeapItem<Node_dk1447>{ //node implements IHeapItem interface

	public bool traversable; //can the node be traversed or not?
	public Vector3 worldPosition; //where is the node positioned

	public int gridX; //grid positions of node
	public int gridY;

	public int gCost; //distance from starting node
	public int hCost; //distance from target node
	public Node_dk1447 parent;
	int heapIndex;

	public Node_dk1447(bool _traversable, Vector3 _worldPos, int _gridX, int _gridY){ //constructor for node, assigning traversability and world and grid position
		traversable = _traversable;
		worldPosition = _worldPos;
		gridX = _gridX;
		gridY = _gridY;
	}

	public int fCost{ //the combined cost of gCost and hCost
		get{
			return gCost + hCost;
		}
	}

	public int HeapIndex{
		get{
			return heapIndex;
		}
		set{
			heapIndex = value;
		}
	}

	public int CompareTo(Node_dk1447 nodeToCompare){
		int compare = fCost.CompareTo(nodeToCompare.fCost);
		if (compare == 0) {
			compare = hCost.CompareTo(nodeToCompare.hCost);
		}
		return -compare;
	}
}
