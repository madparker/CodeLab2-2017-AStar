using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recording : MonoBehaviour {

	public List<Vector3> recPos = new List <Vector3> ();
	public List<Vector3> recRot = new List <Vector3> ();
	public List<bool> recButton = new List <bool> ();
	public List <bool> recDefense = new List<bool> ();

	int [] moveDirections = new int [3] ;


	int lastPositionX;
	int lastPositionY;

	int firstDir, secondDir, thirdDir;


	Vector3 lastPos;
	Vector3 currentPos;

	public LineRenderer lr;
	public Material mat;

	public Vector3 nextPos;
	public Vector3 nextRot;

	public bool isPressed;
	public bool nextButton;
	 
	public int rotIndex = 0;
	public int posIndex = 0;
	public int buttonIndex = 0;

//	public float maxRecTime = 30f;
	float maxplayTime = 0f;
	public float  recTime = 0f;
	public float playTime = 0f;
	float recButtonStart = 0f;
	float recButtonEnd = 0f;
	float playButtonStart = 0f;
	float playButtonEnd = 0f;

	GameManager gameManager;
//	MainPlayer player;
	PlayerScript player;
	TeamAssignment input;
	PlayerStats stats;
	KeyframeScript key;
	Playback play;
//
//	public Image recUI, playUI;
	public Text segmentText;

	public int maxSegments;
	public bool segmentRemoved = false;

	[SerializeField] private int segments;
	public int Segments{
		get { return segments; }
		private set
		{
			segments = value;
			if(segments <= 0)
			{
				segments = 0;
			}
			else if (segments > maxSegments)
			{
				segments = maxSegments;
			}
		}
	}

	private int moveCount = 0;
	public int MoveCount{
		get { return moveCount; }
		private set
		{
			moveCount = value;
			if(moveCount <= 0)
			{
				moveCount = 0;
			}
			else if (moveCount > 3)
			{
				moveCount = 0;
			}
		}
	}

	void Awake(){
		
		player = GetComponent<PlayerScript> ();
		input = GetComponent<TeamAssignment> ();
		key = GetComponent<KeyframeScript> ();
		stats = GetComponent<PlayerStats> ();
		play = GetComponent<Playback> ();

		gameManager = GameObject.Find ("[GameManager]").GetComponent<GameManager> ();
	

//		recUI.enabled = false;
//		playUI.enabled = false;


		// Set up Line Renderer 

		lr = player.gameObject.GetComponentInChildren<LineRenderer> ();
		lr.enabled = false;
		lr.widthMultiplier = 0.2f;
		lr.material = mat;
		lr.receiveShadows = true;
		lr.alignment = LineAlignment.View;
		lr.textureMode = LineTextureMode.Tile;


	}

	void Start(){
		player.enabled = true;

	
		Segments = maxSegments;


		lastPositionX = gameManager.GridPosition(player).x;
		lastPositionY = gameManager.GridPosition(player).y;

//		print (gameObject.name + " : " + lastPositionX + " " + lastPositionY); 
//

	}

	void FixedUpdate () {
		currentPos = transform.position;


	}

	void Update(){
//		RecordingPlayer ();
		play.Play ();


		ClearPath ();

		float recText = stats.maxRecTime - recTime;
		segmentText.text = Segments.ToString () + "/" + maxSegments.ToString ();

	
	}
		


//	public void RecordingPlayer(){
//
//		float recAmount = stats.maxRecTime - recTime;
//		float recFill = Util.remapRange (recAmount, stats.maxRecTime, 0, 1, 0);
////		recUI.fillAmount = recFill;
//
//
//		if (recAmount > 0) {
//
//			if (!play.isPlaying){
////
////				if (Input.GetButtonDown (input.recording)) {
////					key.KeyFrameAssignment (transform.position, false);
////				}
////
//				if (Input.GetButton (input.recording)) {
//					lr.enabled = true;
//					player.enabled = true;
////					recUI.enabled = true;
//					recPos.Add (transform.position);
//					recRot.Add (transform.eulerAngles);
//					PathDrawing ();
//					recTime += Time.deltaTime;
//
//
////					if (!Input.GetButtonUp (input.fire)) {
////						recButton.Add (false);
////					}
//
//					if (Input.GetButtonDown (input.fire)) {
//						recButtonStart = recTime;
//
//
//					}
//					if (Input.GetButtonUp (input.fire)) {
//
//						if (!isPressed) {
//							recButtonEnd = recTime;
//							recButton.Add (true);
//							key.ActionFrameAssignment (transform.position, true);
//							isPressed = true;
//						}
//
//						decimal duration = System.Math.Round ((decimal)(recButtonEnd - recButtonStart), 2);
//						print (duration);
//
//					} else {
//						recButton.Add (false);
//						isPressed = false;
//					}
//
//					// Places Blocks in Recording
//
//					if (Input.GetButtonUp (input.block)) {
//
//						if (!isPressed) {
//							key.ActionFrameAssignment (transform.position, false);
//							isPressed = true;
//						}
//
//					} else {
//						
//						isPressed = false;
//					}
//
//				
//				}
//
//				if (Input.GetButtonUp (input.recording)) {
////					maxplayTime =  maxplayTime + recTime;
//
//					//				recTime = 0;
//					player.enabled = false;
////					recUI.enabled = false;
//			}
//
//
//			
//			}
//		} else {
//
//
//			player.enabled = false; 
////			recUI.enabled = false;
//
//			if (Input.GetButtonUp (input.recording) && !play.isPlaying) {
//
////				key.KeyFrameAssignment (transform.position, false);
////				recTime = 0;
//			}
//		}
//	}
		


	public void PathDrawing(){

		lr.positionCount = recPos.Count;

		float yOffset = -1f;

		for (int i = 0; i < recPos.Count; i++) {
			Vector3 path = new Vector3 (recPos [i].x, recPos [i].y + yOffset, recPos [i].z);
			lr.SetPosition (i, path);
		}
	}

	public void PathCheck(){


		int dir = 9;
		//Debug.Log(key
		//Debug.Log(gameObject.name +" " + key.GridPosition().x +", "+lastPositionX);
		//Debug.Log(gameObject.name +" " + key.GridPosition().x +", "+lastPositionX);
		if (Mathf.Abs (gameManager.GridPosition(player).x - lastPositionX) == 1 && gameManager.GridPosition(player).y - lastPositionY == 0 ) {


			if (gameManager.GridPosition(player).x - lastPositionX > 0) {
				//Debug.Log (gameObject.name+"Moved Right");
				dir = 1;
				TrackMoves (dir);
				MoveCount++;
					
			} else {
				//Debug.Log (gameObject.name+"Moved Left");
				dir = -1;
				TrackMoves (dir);
				MoveCount++;
			}
			lastPositionX = gameManager.GridPosition(player).x;
		}

		if (Mathf.Abs (gameManager.GridPosition(player).y - lastPositionY) == 1 && gameManager.GridPosition(player).x - lastPositionX == 0) {

			if (gameManager.GridPosition(player).y - lastPositionY > 0) {
				//Debug.Log (gameObject.name+"Moved Up");
				dir = 2 ;
				TrackMoves (dir);
				MoveCount++;

			} else {
				//Debug.Log (gameObject.name+"Moved Down");
				dir = -2 ;
				TrackMoves (dir);
				MoveCount++;
			}
			lastPositionY = gameManager.GridPosition(player).y;
		}

		if (Mathf.Abs (gameManager.GridPosition(player).y - lastPositionY) == 1 &&  Mathf.Abs(gameManager.GridPosition(player).x - lastPositionX) == 1) {

			if (gameManager.GridPosition(player).y - lastPositionY > 0 && gameManager.GridPosition(player).x - lastPositionX > 0) {
//				Debug.Log ("Moved UpRight");
				dir = 3;
				TrackMoves (dir);
				MoveCount++;

			} else if (gameManager.GridPosition(player).y - lastPositionY > 0 && gameManager.GridPosition(player).x - lastPositionX < 0) {
//				Debug.Log ("Moved UpLeft");
				dir = 3 ;
				TrackMoves (dir);
				MoveCount++;

			} else if (gameManager.GridPosition(player).y - lastPositionY < 0 && gameManager.GridPosition(player).x - lastPositionX > 0) {
//				Debug.Log ("Moved DownRight");
				dir = -3;
				TrackMoves (dir);
				MoveCount++;
			} else {
//				Debug.Log ("Moved DownLeft");
				dir = -3;
				TrackMoves (dir);
				MoveCount++;
			}
			lastPositionY = gameManager.GridPosition(player).y;
			lastPositionX = gameManager.GridPosition(player).x;
		}






	} 

	void TrackMoves(int dir){
		bool isPlaced = false;

		
		for (int i = 0; i < moveDirections.Length; i++) {
			if (moveDirections [i] == 0) {
				moveDirections [i] = dir;
				isPlaced = true;

				break;

			}
		}


		if (!isPlaced) {
			moveDirections [0] = moveDirections [1];
			moveDirections [1] = moveDirections [2];
			moveDirections [2] = dir;
		}

		if (AttackPatternCheck()) {
			Debug.Log (gameObject.name + " Attack Formation Created");
			key.QueueAttack ();
		}
			
		if (DefensePatternCheck()) {
			Debug.Log (gameObject.name + "Defensive Formation Created");
			key.QueueBlocker ();
		}



//		Debug.Log (moveDirections[0] + " " + moveDirections[1] + " " + moveDirections[2]);

		
	}
		


	void ClearPath(){
		if (!play.isPlaying && !Input.GetButton(input.recording)){
			if (Input.GetButtonDown (input.cancel)) {

//				for (int x = 0; x < key.activeBlocks.Count; x++) {
//					if (!key.activeBlocks [x].GetComponent<Collider> ().enabled) {
//						Destroy (key.activeBlocks [x]);
//						key.blockPoints.Remove (key.blockPoints [x]);
//						key.activeBlocks.Remove (key.activeBlocks [x]);
//					}
//				}
//

				
				for (int i = 0; i < key.perimeterPoints.Length; i ++){
					key.perimeterPoints [i] = Vector3.zero;
				}
				key.ClearKeyFrames ();


				moveDirections[0] = 0;
				moveDirections [1] = 0;
				moveDirections [2] = 0;


				recPos.Clear ();
				recButton.Clear ();
				recRot.Clear ();
				lr.enabled = false;
		

				recTime = 0;
				player.ResetPlayerPos ();

				ReplenishSegments ();


				lastPositionX = player.X;

				lastPositionY = player.Z;

				MoveCount = 0;

				key.StorePerimeterPoints (player.gridPos);

		

		

			}
		}
	}

//	public void RecordAction(){
//		if (Input.GetButtonDown (input.fire)) {
//			if (!isPressed) {
//
//				if (key.KeyFrameDistanceCheck (transform.position)) {
//					recPos.Add (transform.position);
//					recButton.Add (true);
//					key.ActionFrameAssignment (transform.position, true);
//					isPressed = true;
//				}
//			}
//		}
//
//		if (Input.GetButtonDown (input.block)) {
//			if (!isPressed) {
//
//				if (key.KeyFrameDistanceCheck (transform.position)) {
//					recPos.Add (transform.position);
//					recDefense.Add (true);
//					key.ActionFrameAssignment (transform.position, false);
//					isPressed = true;
//				}
//			}
//
//		}
//	}
		

	public void UseSegments(){

//		Debug.Log ("Being called");
		Segments--;
		segmentRemoved = true;

		
	}


	public void SegmentCheck(Vector3 pos){
		if (!key.areaNodes.Contains (pos)) {
			if (!segmentRemoved && Segments > 0) {

				UseSegments ();
			} 
		} else {
			segmentRemoved = true;
		}
	
	}


	public void ReplenishSegments(){
		Segments = maxSegments;

	}

	public bool DefensePatternCheck(){
		bool patternMade = false;

		int [] defeseFormation = new int [3] ;

		defeseFormation [0] = 1;
		defeseFormation [1] = 2;
		defeseFormation [2] = -1;

		for (int index = 0; index < moveDirections.Length; index++) {

			if (Mathf.Abs (moveDirections [index]) != Mathf.Abs (defeseFormation [index])) {


				patternMade = false;
				break;

			} else {
				patternMade = true;
			}

		}


		if (moveDirections [2] != moveDirections[0] * -1 ) {

			patternMade = false;

		} 


		return patternMade;
	}

	public bool AttackPatternCheck(){
		bool patternMade = false;

		int [] attackFormation = new int [3] ;

		attackFormation [0] = 3;

		if (moveDirections [0] > 0) {
			attackFormation [1] = -2;
			
		} else {
			attackFormation [1] = 2;
		}

		attackFormation [2] = -3;

	

		for (int index = 0; index < moveDirections.Length; index++) {

			if (Mathf.Abs (moveDirections [index]) != Mathf.Abs (attackFormation [index])) {


				patternMade = false;
				break;

			} else {
				patternMade = true;
//				Debug.Log ("pattern true");
			}

		}

		if (moveDirections [1] != attackFormation[1] ) {
			patternMade = false;
		} 


		if (moveDirections [2] != moveDirections[0] ) {
			patternMade = false;
		} 


		return patternMade;
	}





}
