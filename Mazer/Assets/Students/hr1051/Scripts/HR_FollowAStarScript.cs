using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HR_FollowAStarScript : MonoBehaviour {

	[SerializeField] float mySpeed = 1;

	protected bool move = false;

	protected HR_Path path;
	public HR_AStar astar;
	public Step startPos;
	public Step destPos;

	protected int currentStep = 1;

	protected float lerpPer = 0;

//	protected float startTime;
//	protected float travelStartTime;

	void Awake () {
		astar = this.GetComponent<HR_AStar> ();
	}

	// Use this for initialization
	protected virtual void Start () {
		
	}

	public void Move () {
		path = astar.path;
		startPos = path.Get(0);
		if (path.path.Count <= 1) {
			currentStep = 0;
		}
		destPos = path.Get (currentStep);

		transform.position = startPos.gameObject.transform.position;

		//		Debug.Log(path.nodeInspected/100f);

//		Invoke("StartMove", path.nodeInspected/100f);

//		startTime = Time.realtimeSinceStartup;

		move = true;
	}


	// Update is called once per frame
	protected virtual void Update () {

		if(move){
			lerpPer += Time.deltaTime * mySpeed / (destPos.moveCost);

			transform.position = Vector3.Lerp(startPos.gameObject.transform.position, 
				destPos.gameObject.transform.position, 
				lerpPer);

			if(lerpPer >= 1){
				lerpPer = 0;

				currentStep++;

				if(currentStep >= path.steps){
					currentStep = 1;
					move = false;
//					Debug.Log(path.pathName + " got to the goal in: " + (Time.realtimeSinceStartup - startTime));
//					Debug.Log(path.pathName + " travel time: " + (Time.realtimeSinceStartup - travelStartTime));


					HR_Grid.Instance.myCurrentPos = destPos.gameObject.GetComponent<HR_Block> ().myGridPos;
					HR_Block t_block = destPos.gameObject.GetComponent<HR_Block> ();

					if (t_block.myBlockSet.myBlockType == HR_BlockSet.BlockType.Tree) {
						t_block.SetMyBlockSet (
							HR_Grid.Instance.GetMyBlockSet (HR_BlockSet.BlockType.Empty)
						);
					}

					if (t_block.myBlockSet.myBlockType == HR_BlockSet.BlockType.Empty) {
						if (HR_Player.Instance.haveSeed == true) {
							HR_Player.Instance.Throw ();
							t_block.SetMyBlockSet (
								HR_Grid.Instance.GetMyBlockSet (HR_BlockSet.BlockType.Flower)
							);
						} else if (HR_Player.Instance.haveNut == true) {
							HR_Player.Instance.Throw ();
							t_block.SetMyBlockSet (
								HR_Grid.Instance.GetMyBlockSet (HR_BlockSet.BlockType.Tree)
							);
						}
					} else if (t_block.myBlockSet.myBlockType == HR_BlockSet.BlockType.Seed) {
						HR_Player.Instance.PickUpSeed ();
					} else if (t_block.myBlockSet.myBlockType == HR_BlockSet.BlockType.Nut) {
						HR_Player.Instance.PickUpNut ();
					}

					GetComponent<LineRenderer> ().positionCount = 0;

					return;
				} 

				startPos = destPos;
				destPos = path.Get(currentStep);
			}
		}
	}

	protected virtual void StartMove(){
		move = true;
//		travelStartTime = Time.realtimeSinceStartup;
	}

	public bool GetMove () {
		return move;
	}
}

