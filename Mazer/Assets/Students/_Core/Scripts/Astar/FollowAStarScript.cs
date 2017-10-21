using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowAStarScript : MonoBehaviour {

	protected bool move = false;

	protected Path path;
	public AStarScript astar;
	public Step startPos;
	public Step destPos;

	protected int currentStep = 1;

	protected float lerpPer = 0;
	
	protected float startTime;
	protected float travelStartTime;

    lrGridScript lrGridS;
    private void Awake() {
        lrGridS = GameObject.Find("Grid").GetComponent<lrGridScript>();
        }

	// Use this for initialization
	protected virtual void Start () {
		path = astar.path;
		startPos = path.Get(0);
		destPos  = path.Get(currentStep);

		transform.position = startPos.gameObject.transform.position;

        //		Debug.Log(path.nodeInspected/100f);

        StartMove();

		startTime = Time.realtimeSinceStartup;

	}
	
	// Update is called once per frame
	protected virtual void Update () {

		if(move){
			lerpPer += Time.deltaTime/destPos.moveCost;

			transform.position = Vector3.Lerp(startPos.gameObject.transform.position, 
			                                  destPos.gameObject.transform.position, 
			                                  lerpPer);

			if(lerpPer >= 1){
				lerpPer = 0;

				currentStep++;
                GameObject go = path.Get(currentStep).gameObject;
                //Debug.Log(go);
                //Debug.Log(go.transform.position);

                int iY = -1;
                int iX = -1;
                //Debug.Log(lrGridS.gridArray.Length);
                for (int i = 0; i < lrGridS.gridArray.Length; i++){
                    if (iY != -1)
                        break;
                    iX = i;
                    for (int j = 0;  j< lrGridS.gridArray[i].Length; j++) {
                        if(lrGridS.gridArray[i][j]==go){
                            iY = j;
                            break;
                        }
                       
                        }



                    //iY = System.Array.IndexOf()lrGridS.gridArray[i], go);
                    //if (iX > -1){
                    //    iX = i;
                    //    //it did find it
                    //    break;
                    //}
                }

                //Debug.Log("x " + iX + " y " + iY);
                lrGridS.SetTileToLowerCostMat(iX,iY);


				if(currentStep >= path.steps){
					currentStep = 0;
					move = false;
					//Debug.Log(path.pathName + " got to the goal in: " + (Time.realtimeSinceStartup - startTime));
					//Debug.Log(path.pathName + " travel time: " + (Time.realtimeSinceStartup - travelStartTime));
				} 

				startPos = destPos;
				destPos = path.Get(currentStep);
			}
		}
	}

	protected virtual void StartMove(){
		move = true;
		travelStartTime = Time.realtimeSinceStartup;
	}
}

