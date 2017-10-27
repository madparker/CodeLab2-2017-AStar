using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour {


	GameManager gameManager;

	public float currentSpeed;

	public int bulletsMax = 2;

	public float maxRotationLeft;
	public float maxRotationRight;
	[SerializeField] float defaultRotation;

	public GameObject prefab;
	public List <GameObject> bullets = new List<GameObject> ();

	TeamAssignment input;
	KeyframeScript key;
	Recording record;
	Playback play;
	public int playerNum;

	public Vector3 gridPos;

	//Used for initilizing player position on grid
	public int startIndexX, startIndexZ;


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

	public bool hasFired = false;
	bool isPressed = false;
	bool usingAxis = false;
	bool frameAdded = false;
	float t;

	//Controller delay 
	float delay = 0.15f;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("[GameManager]").GetComponent<GameManager> ();
		input = GetComponent<TeamAssignment> ();
		record = GetComponent<Recording> ();
		key = GetComponent<KeyframeScript> ();
		play = GetComponent<Playback> ();

		//Starts each player in the default position
		startIndexX = xCord;
		startIndexZ = zCord;
		ResetPlayerPos ();
	
	}
	
	// Update is called once per frame
	void Update () {
//		PlayerControl ();

		PlayerGridControls();
//		print (X + "  " + Z);



	

	}

//	void PlayerControl(){
//
//		float controllerVertical = Input.GetAxis (input.vertical);
//		float controllerHorizontal = Input.GetAxis (input.horizontal);
//
//		float horizontalPos = transform.position.x + controllerHorizontal * Time.deltaTime * currentSpeed;
//		float verticalPos = transform.position.z + controllerVertical * Time.deltaTime * currentSpeed;
//
//		Debug.Log (controllerHorizontal + " - " + controllerVertical);
//
//
//
//
//		transform.position = new Vector3 (horizontalPos, transform.position.y, verticalPos);
//
////		Vector3 movement = new Vector3 (controllerHorizontal, 0f, controllerVertical);
////		rb.AddForce (movement * currentSpeed * Time.deltaTime * 10f, ForceMode.Impulse);
//
//		float playerRotation = Util.remapRange (Input.GetAxis (input.rStick), -1, 1, maxRotationLeft, maxRotationRight);
//		if (transform.eulerAngles.y <= maxRotationRight && transform.eulerAngles.y >= maxRotationLeft) {
//			//			transform.Rotate (0f, turnAngle, 0f);
//			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, playerRotation, transform.eulerAngles.z);
//		} else {
//			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, defaultRotation, transform.eulerAngles.z);
//		}
//
//	}

	public void PlayerAction(){
		Debug.Log ("Maybe bullet count issue?");

		//If bullets fired are less than the max bullet amount, allow firing

		if (bullets.Count < bulletsMax) {

			Debug.Log ("Reached here");

			if (!hasFired) {
				//Creates projectile from prefab and sets its Velocity and which player fired it. Also adds it to a list for bullet amount check
				GameObject projectileInstance = Instantiate (prefab, key.attackPoints[key.attackIndex] + transform.forward * 2f, Quaternion.Euler (transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z));
				ProjectileScript ps = projectileInstance.GetComponent<ProjectileScript> ();

				ps.initialVelocity = transform.forward * 20f;

				ps.player = this;
				ps.pNum = playerNum;
				bullets.Add (projectileInstance);

//				key.attackIndex++;
				hasFired = true;
			}
		}
		

	}

	void GridMovement(){

		// If game isnt paused and the player is not at GridPos, the player automatically moves to GridPos at the set speed
		if (!play.isPaused) {
			transform.position = Vector3.MoveTowards (transform.position, gridPos, currentSpeed * Time.deltaTime);

			if (!play.isPlaying) {

				record.lr.enabled = true;
				record.PathDrawing ();

				// Records Player position and button presses
				record.recPos.Add (transform.position);	
				record.recButton.Add (false);
				record.recDefense.Add (false);

				//Sets 'single-use' button bool checks to false for later use
				isPressed = false;
//				record.isPressed = false;
				hasFired = false;
				frameAdded = false;


			}
		}
	}

	void PlayerGridControls(){
		// buffer for controller sensitivity
		float buffer = 0.2f;
		float controllerVertical = Input.GetAxis (input.vertical);
		float controllerHorizontal = Input.GetAxis (input.horizontal);


		// When player is in the set position...
		if (transform.position == gridPos) {

		

			record.segmentRemoved = false;
				
//			record.RecordAction ();

			gridPos = gameManager.gridArray [X, Z].position;



			if (record.Segments > 0 ) {
				ControllerInput (controllerVertical, controllerHorizontal, buffer);
			}

			if (!frameAdded) {

				key.StorePerimeterPoints (gridPos);
				record.PathCheck ();
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
			record.SegmentCheck (gridPos);

			if (record.segmentRemoved) {
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

//	public void PlaybackAction(){
//		if (Input.GetButtonDown (input.fire)) {
//			if (!isPressed && play.isPlaying) {
//
//				if (key.KeyFrameDistanceCheck (transform.position)) {
////					record.recPos.Add (transform.position);
////					record.recButton.Add (true);
//					PlayerAction ();
//
////					key.ActionFrameAssignment (transform.position, true);
//					isPressed = true;
//				}
//			}
//		}
//
//	}


}
