using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyframeScript : MonoBehaviour {

	[SerializeField] GameObject nodePrefab;
	[SerializeField] GameObject attkNodePrefab;
	[SerializeField] GameObject blockPrefab;

	Transform[] astarNodes;


	GameObject key;
	GameObject area;
	GameObject node;
	GameObject block; 
	GameObject attackObj;
	public Transform keyFrameHolder;

	GameManager gameManager;
	PlayerScript player;
	TeamAssignment input;
//	Recording record;

	public List<Vector3> recPos = new List <Vector3> ();
	public List<IntVector2> keyFrames = new List<IntVector2>();
	public List<Vector3> areaNodes = new List<Vector3>();
	public List <Vector3> nodes = new List<Vector3>();
	public List <Vector3> attkNode = new List<Vector3> ();
	public List <bool> ready = new List<bool>();
	public List <bool> attkReady = new List<bool> ();

	public Vector3 [] perimeterPoints = new Vector3 [4] ;

	public bool isReady = false;

	public int blockIndex = 0;
	public int attackIndex = 0;
	 

//	Dictionary <Vector3, GameObject> activeBlockers = new Dictionary <Vector3, GameObject>();

	public List <GameObject> activeBlocks = new List<GameObject> ();
	public List <GameObject> activeAttacks = new List <GameObject> ();
	public List <Vector3> blockPoints = new List <Vector3> ();

	public List <Vector3> attackPoints = new List <Vector3> ();

	float yOffset = -0.8f;

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

	int [] moveDirections = new int [3] ;


	int lastPositionX;
	int lastPositionY;

	public LineRenderer lr;
	public Material mat;

//	int playerNum;

	void Start(){
		gameManager = GameObject.Find ("[GameManager]").GetComponent<GameManager> ();
		player = GetComponent<PlayerScript> ();
		input = GetComponent<TeamAssignment> ();

		astarNodes = gameManager.grid.GetComponentsInChildren<Transform> ();


		for (int i = 0; i < perimeterPoints.Length; i++) {
			perimeterPoints [i] = Vector3.zero;
		}

//		AssignDefaultKeyframes ();

		// Set up Line Renderer 

		lr = player.gameObject.GetComponentInChildren<LineRenderer> ();
		lr.enabled = false;
		lr.widthMultiplier = 0.2f;
		lr.material = mat;
		lr.receiveShadows = true;
		lr.alignment = LineAlignment.View;
		lr.textureMode = LineTextureMode.Tile;

		player.enabled = true;


		Segments = maxSegments;


		lastPositionX = gameManager.GridPosition(player).x;
		lastPositionY = gameManager.GridPosition(player).y;

	}

	void Update(){
		ClearPath ();

		if (segmentText != null){
		segmentText.text = Segments.ToString () + "/" + maxSegments.ToString ();
		}
	}

	public void AreaNodeAssignment(Vector3 pos){
//		
		keyFrames.Add (gameManager.GridPosition(player));

		if (!areaNodes.Contains(pos)) {
			Vector3 areaPos = new Vector3 (pos.x, pos.y + yOffset, pos.z);
			area = Instantiate (nodePrefab, areaPos, Quaternion.identity, keyFrameHolder);
			area.transform.localEulerAngles = new Vector3 (90f, 0f, 0f);
			area.transform.localScale = new Vector3 (3.4f, 3.4f, 3.4f);
			areaNodes.Add (pos);


		}


	}

	void AssignDefaultKeyframes(){
		for (int i = 0; i < gameManager.columnLength; i++) {
			AreaNodeAssignment (gameManager.gridArray[player.X,i].position);
			keyFrames.Clear ();


		}


	}

	public void KeyFrameAssignment(){
		if (!keyFrames.Contains (gameManager.GridPosition(player))) {
	
			keyFrames.Add (gameManager.GridPosition(player));
		}
		
	}



	public void ClearKeyFrames(){

		nodes.Clear ();
		keyFrames.Clear ();
//		blockPoints.Clear ();

		activeBlocks.Clear ();
		ready.Clear ();
		for (int i = 0; i < activeAttacks.Count; i++) {
			Destroy (activeAttacks [i]);
		}
		attkNode.Clear ();
		activeAttacks.Clear ();
		attackPoints.Clear ();
		attkReady.Clear ();


	}

	public void ResetIndex(){
		blockIndex = 0;
		attackIndex = 0;
	}


	public void ActivateQueuedBlocker(){

		if (activeBlocks.Count > 0) {
//			Debug.Log ("Enabling bloc");

			GameObject currentBlock = activeBlocks [blockIndex];

			currentBlock.GetComponent<Blocker> ().EnableBlock ();
		}

		if (blockIndex < activeBlocks.Count - 1) {
			blockIndex++;
		}


	}

	public void ActivateQueuedAttack(){

		if (activeAttacks.Count > 0) {
			//			Debug.Log ("Enabling bloc");

//			player.PlayerAction();
		}

		if (attackIndex < activeAttacks.Count - 1) {
			attackIndex++;
		}
	}

	public void QueueBlocker(){
		float yOffset = -2f;

		Vector3 centroid = UtilScript.CalculateCentroid (perimeterPoints, yOffset);

		if (!blockPoints.Contains(centroid)){

			isReady = true;

			nodes.Add (player.gridPos);
			ready.Add (isReady);


			block = Instantiate (blockPrefab, centroid, Quaternion.identity) as GameObject;
			block.GetComponent<Blocker> ().keyAttached = this;
//			block.GetComponent<Blocker> ().DisableBlock();
			activeBlocks.Add (block);
//			activeBlockers.Add (centroid, block);
			blockPoints.Add (centroid);
		}
	}


	public void QueueAttack(){
		float yOffset = -2f;

		Vector3 centroid = UtilScript.CalculateCentroid (perimeterPoints, yOffset);

		if (!attackPoints.Contains(centroid)){

			isReady = true;

			attkNode.Add (player.gridPos);
			attkReady.Add (isReady);

			attackObj = Instantiate (attkNodePrefab, centroid, Quaternion.identity) as GameObject;
//			block.GetComponent<Blocker> ().DisableBlock();
			activeAttacks.Add (attackObj);
//			activeBlockers.Add (centroid, block);
			attackPoints.Add (centroid);

		}

	}


	public void StorePerimeterPoints(Vector3 pos){
		bool isPlaced = false;


		for (int i = 0; i < perimeterPoints.Length; i++) {
			if (perimeterPoints [i] == Vector3.zero) {
				perimeterPoints [i] = pos;
				isPlaced = true;

				break;

			}
		}
			
		if (!isPlaced) {
			perimeterPoints [0] = perimeterPoints [1];
			perimeterPoints [1] = perimeterPoints [2];
			perimeterPoints [2] = perimeterPoints [3];
			perimeterPoints [3] = pos;
		}


		
	}

	public void KeyFrameSettings(Vector3 pos){
		//Clears node spritre moved on

		if (gameObject.name != "Bullet") {
			for (int i = 0; i < astarNodes.Length; i++) {
				if (pos == astarNodes [i].position) {
					astarNodes [i].gameObject.GetComponent<SpriteRenderer> ().enabled = false;
				}
			}
		}



//		AreaNodeAssignment (pos);
		isReady = false;
	}

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

			} else {
				//Debug.Log (gameObject.name+"Moved Left");
				dir = -1;
				TrackMoves (dir);
			}
			lastPositionX = gameManager.GridPosition(player).x;
		}

		if (Mathf.Abs (gameManager.GridPosition(player).y - lastPositionY) == 1 && gameManager.GridPosition(player).x - lastPositionX == 0) {

			if (gameManager.GridPosition(player).y - lastPositionY > 0) {
				//Debug.Log (gameObject.name+"Moved Up");
				dir = 2 ;
				TrackMoves (dir);

			} else {
				//Debug.Log (gameObject.name+"Moved Down");
				dir = -2 ;
				TrackMoves (dir);
			}
			lastPositionY = gameManager.GridPosition(player).y;
		}

		if (Mathf.Abs (gameManager.GridPosition(player).y - lastPositionY) == 1 &&  Mathf.Abs(gameManager.GridPosition(player).x - lastPositionX) == 1) {

			if (gameManager.GridPosition(player).y - lastPositionY > 0 && gameManager.GridPosition(player).x - lastPositionX > 0) {
				//				Debug.Log ("Moved UpRight");
				dir = 3;
				TrackMoves (dir);

			} else if (gameManager.GridPosition(player).y - lastPositionY > 0 && gameManager.GridPosition(player).x - lastPositionX < 0) {
				//				Debug.Log ("Moved UpLeft");
				dir = 3 ;
				TrackMoves (dir);

			} else if (gameManager.GridPosition(player).y - lastPositionY < 0 && gameManager.GridPosition(player).x - lastPositionX > 0) {
				//				Debug.Log ("Moved DownRight");
				dir = -3;
				TrackMoves (dir);
			} else {
				//				Debug.Log ("Moved DownLeft");
				dir = -3;
				TrackMoves (dir);
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
			QueueAttack ();
		}

		if (DefensePatternCheck()) {
			Debug.Log (gameObject.name + "Defensive Formation Created");
			QueueBlocker ();
		}



		//		Debug.Log (moveDirections[0] + " " + moveDirections[1] + " " + moveDirections[2]);


	}



	void ClearPath(){
		if (Input.GetButtonDown (input.cancel)) {


			for (int i = 0; i < perimeterPoints.Length; i ++){
				perimeterPoints [i] = Vector3.zero;
			}
			ClearKeyFrames ();


			moveDirections[0] = 0;
			moveDirections [1] = 0;
			moveDirections [2] = 0;


			lr.enabled = false;
			recPos.Clear ();

			player.ResetPlayerPos ();

			ReplenishSegments ();


			lastPositionX = player.X;

			lastPositionY = player.Z;

			StorePerimeterPoints (player.gridPos);


		}
	}


	public void UseSegments(){

		//		Debug.Log ("Being called");
		Segments--;
		segmentRemoved = true;


	}


	public void SegmentCheck(Vector3 pos){
		if (!areaNodes.Contains (pos)) {
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
