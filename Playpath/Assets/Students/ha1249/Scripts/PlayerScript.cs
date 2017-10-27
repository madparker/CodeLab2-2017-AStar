using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	GameManager gameManager;
	TeamAssignment input;
	KeyframeScript key;

	public float currentSpeed;

	[SerializeField] float defaultRotation;
	public int playerNum;

	public Vector3 gridPos;

	//Used for initilizing player position on grid
	public int startIndexX, startIndexZ;

	public bool hasFired = false;
	bool isPressed = false;
	bool usingAxis = false;
	bool frameAdded = false;
	float t;

	//Controller delay 
	float delay = 0.15f;


	// Coordinate properties for Grid incrementing movement
	[SerializeField] private int xCord;
	public int X{
		get { return xCord; }
		private set
		{
			xCord = value;
			if(xCord <= 0)
			{
				xCord = 0;
			}
			else if (xCord > gameManager.rowLength-1)
			{
				xCord = gameManager.rowLength-1;
			}
		}
	}

	[SerializeField] private int zCord;
	public int Z{
		get { return zCord; }
		private set
		{
			zCord = value;
			if(zCord <= 0)
			{
				zCord = 0;
			}
			else if (zCord > gameManager.columnLength-1)
			{
				zCord = gameManager.columnLength-1;
			}
		}
	}



	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("[GameManager]").GetComponent<GameManager> ();
		input = GetComponent<TeamAssignment> ();
		key = GetComponent<KeyframeScript> ();

		//Starts each player in the default position
		startIndexX = xCord;
		startIndexZ = zCord;
		ResetPlayerPos ();

	}

	void Update () {
		PlayerGridControls();
	}

	void GridMovement(){

		transform.position = Vector3.MoveTowards (transform.position, gridPos, currentSpeed * Time.deltaTime);

		//Used to Draw Line
//		key.lr.enabled = true;
		key.PathDrawing ();

		// Records Player position
		key.recPos.Add (transform.position);	


		//Sets 'single-use' button bool checks to false for later use
		isPressed = false;
//		key.isPressed = false;
		hasFired = false;
		frameAdded = false;
	}

	void PlayerGridControls(){
		// buffer for controller sensitivity
		float buffer = 0.2f;
		float controllerVertical = Input.GetAxis (input.vertical);
		float controllerHorizontal = Input.GetAxis (input.horizontal);


		// When player is in the set position...
		if (transform.position == gridPos) {



			key.segmentRemoved = false;

			gridPos = gameManager.gridArray [X, Z].position;



			if (key.Segments > 0 ) {
				ControllerInput (controllerVertical, controllerHorizontal, buffer);
			}

			if (!frameAdded) {

				key.StorePerimeterPoints (gridPos);

				//Used to make shapes
//				key.PathCheck ();


				key.KeyFrameSettings (gridPos);

				frameAdded = true;

			}

			// Implements delay to Coord incrementing

			if (usingAxis) {
				t += Time.deltaTime;

				if (t >= delay) {
					t = 0;
					usingAxis = false;
				}
			}


		} else {
			key.SegmentCheck (gridPos);

			if (key.segmentRemoved) {
				GridMovement ();
			}
		}

	}

	void ControllerInput(float controllerVertical, float controllerHorizontal, float buffer ){

		//This function handles user Grid Movement Input

		if (controllerVertical > buffer  && Mathf.Approximately(controllerHorizontal, 0)) {
			if (!usingAxis) {
				Z++;
				usingAxis = true;
			}

		}


		if (controllerVertical < -buffer && Mathf.Approximately(controllerHorizontal, 0)) {
			if (!usingAxis) {
				Z--;
				usingAxis = true;
			}

		}

		if (controllerHorizontal >  buffer && Mathf.Approximately(controllerVertical, 0)) {
			if (!usingAxis) {
				X++;
				usingAxis = true;
			}
		}

		if (controllerHorizontal < -buffer  && Mathf.Approximately(controllerVertical, 0) ) {
			if (!usingAxis) {
				X--;
				usingAxis = true;
			}
		}

		if (controllerHorizontal > buffer && controllerVertical > buffer) {
			if (!usingAxis) {
				X++;
				Z++;
				usingAxis = true;
			}
		}

		if (controllerHorizontal < -buffer && controllerVertical < -buffer) {
			if (!usingAxis) {
				X--;
				Z--;
				usingAxis = true;
			}
		}

		if (controllerHorizontal < -buffer && controllerVertical > buffer) {
			if (!usingAxis) {
				X--;
				Z++;
				usingAxis = true;
			}
		}

		if (controllerHorizontal > buffer && controllerVertical < -buffer) {
			if (!usingAxis) {
				X++;
				Z--;
				usingAxis = true;
			}
		}
	}


	public void ResetPlayerPos(){

		X = startIndexX;
		Z = startIndexZ;

		transform.position = gameManager.gridArray [startIndexX, startIndexZ].position;
		gridPos = transform.position;
	}

}
