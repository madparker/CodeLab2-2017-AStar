using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_dk1447{

	public bool traversable; //can the node be traversed or not?
	public Vector3 worldPosition; //where is the node positioned

	public Node_dk1447(bool _traversable, Vector3 _worldPos){ //constructor for node, assigning traversability and work position
		traversable = _traversable;
		worldPosition = _worldPos;
	}
}
