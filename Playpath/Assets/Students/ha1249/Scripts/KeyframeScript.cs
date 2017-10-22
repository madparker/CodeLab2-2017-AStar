using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyframeScript : MonoBehaviour {

	[SerializeField] GameObject nodePrefab;
	[SerializeField] GameObject attkNodePrefab;
	[SerializeField] GameObject blockPrefab;


	GameObject key;
	GameObject area;
	GameObject node;
	GameObject block; 
	GameObject attackObj;
	public Transform keyFrameHolder;

	GameManager gameManager;
	MainPlayer player;
	Recording record;


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

	int playerNum;

	void Start(){
		gameManager = GameObject.Find ("[GameManager]").GetComponent<GameManager> ();
		player = GetComponent<MainPlayer> ();
		record = GetComponent<Recording> ();
		playerNum = player.playerNum;

		for (int i = 0; i < perimeterPoints.Length; i++) {
			perimeterPoints [i] = Vector3.zero;
		}


	}

	public void AreaNodeAssignment(Vector3 pos){
//		
		keyFrames.Add (GridPosition());

		if (!areaNodes.Contains(pos)) {
			Vector3 areaPos = new Vector3 (pos.x, pos.y + yOffset, pos.z);
			area = Instantiate (nodePrefab, areaPos, Quaternion.identity, keyFrameHolder);
			area.transform.localEulerAngles = new Vector3 (90f, 0f, 0f);
			area.transform.localScale = new Vector3 (3.4f, 3.4f, 3.4f);
			areaNodes.Add (pos);


		}
	}

	public void KeyFrameAssignment(){
		if (!keyFrames.Contains (GridPosition())) {
	
			keyFrames.Add (GridPosition());
		}
		
	}

//	public void ActionFrameAssignment(Vector3 pos, bool action){
//
////		print (Vector3.Distance(pos, temp));
//
//		if (!action) {
//			if ((pos != temp || actionFrames.Count == 0) && Vector3.Distance(pos, temp) > 2f) {
//				Debug.Log (" Added");
//				actionFrames.Add (pos);
//				Vector3 keyPos = new Vector3 (pos.x, pos.y + yOffset, pos.z);
//
//
//				CreateBlocker (keyPos);
//
//
//				temp = pos;
//			}
//		} else {
//
//			if (pos != temp) {
//
//				actionFrames.Add (pos);
//				Vector3 keyPos = new Vector3 (pos.x, pos.y + yOffset, pos.z);
//				node = Instantiate (attkNodePrefab, keyPos, Quaternion.identity, keyFrameHolder);
//				node.transform.localEulerAngles = new Vector3 (90f, 0f, 0f);
//				node.transform.localScale = new Vector3 (3.4f, 3.4f, 3.4f);
//				nodes.Add (node);
//
//				temp = pos;
//			}
//		}
//	}

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

//			GameObject currentAttack = activeAttacks [attackIndex];

//			currentAttack.GetComponent<AttackBlocker> ().EnableBlock ();
			player.PlayerAction();
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
			block.GetComponent<Blocker> ().DisableBlock();
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

//
//	public bool KeyFrameDistanceCheck(Vector3 pos){
//		bool hasSpace = true;
//		foreach (Vector3 key in actionFrames) {
//			float buffer = 0.02f;
//			if (Vector3.Distance (key, pos) >= gameManager.gridSize - buffer) {
//				hasSpace =  true;
//			} else{
//				hasSpace = false;
//				break;
//			}
//		}
//		return hasSpace;
//	}
		

	public IntVector2 GridPosition(){
		return new IntVector2 (player.X, player.Z);
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


		AreaNodeAssignment (pos);
//		nodes.Add (pos);
//		ready.Add (isReady);
		isReady = false;
	}





}
