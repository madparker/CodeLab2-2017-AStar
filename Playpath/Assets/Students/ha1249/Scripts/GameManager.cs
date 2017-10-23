﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject player1;
	public GameObject player2;

	public Transform [,] gridArray;
	GameObject grid;
	public int rowLength;
	public int columnLength;

	public float zOffset = -7f;
	public float xOffset = -30f;

	public float gridSize;

	public Transform dotPrefab;



	void Awake () {
		grid = new GameObject ("Grid");
		gridArray = new Transform[rowLength, columnLength];

		for (int c = 0; c < columnLength; c++) {
			for (int r = 0; r < rowLength; r++) {
				Vector3 pos = new Vector3 (r * gridSize + xOffset,-1f,c * gridSize + zOffset);
				gridArray[r,c] = Instantiate (dotPrefab, pos, Quaternion.identity, grid.transform) as Transform;
//				print (gridArray [r, c]);

			}
		}

	}

	void Start(){
		AssignDefaultKeyframes ();
	}
		
	public Vector3 GetWorldPosition(int x, int z){
		return gridArray[x,z].position;
	}



	void AssignDefaultKeyframes(){
		for (int i = 0; i < columnLength; i++) {
			player1.GetComponent<KeyframeScript> ().AreaNodeAssignment (gridArray[0,i].position);
			player1.GetComponent<KeyframeScript> ().keyFrames.Clear ();

			player2.GetComponent<KeyframeScript> ().AreaNodeAssignment (gridArray[rowLength-1,i].position);
			player2.GetComponent<KeyframeScript> ().keyFrames.Clear ();

		}

	
	}

	public float GetMovementCost(GameObject go){
//		Material mat = go.GetComponent<MeshRenderer>().sharedMaterial;
//		int i;

//		for(i = 0; i < mats.Length; i++){
//			if(mat.name.StartsWith(mats[i].name)){
//				break;
//			}
//		}

		return 0;
	}

	public Vector2 Pos2d(MainPlayer target){

		return new Vector2 (GridPosition(target).x, GridPosition(target).y);
	}

	public IntVector2 GridPosition(MainPlayer target){
		return new IntVector2 (target.X, target.Z);
	}

}
