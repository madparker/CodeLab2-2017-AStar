using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lrFollowAstarScript : FollowAStarScript {

    lrGridScript lrGridS;
    private void Awake() {
        lrGridS = GameObject.Find("Grid").GetComponent<lrGridScript>(); 
    }

    protected override void Update() {

        if (move) {
            lerpPer += Time.deltaTime / destPos.moveCost;

            transform.position = Vector3.Lerp(startPos.gameObject.transform.position,
                                              destPos.gameObject.transform.position,
                                              lerpPer);

            if (lerpPer >= 1) {
                lerpPer = 0;

                currentStep++;
                //lrGridS.GetGrid()
                //GameObject go = path.Get(currentStep).gameObject;
                //Debug.Log(go);

                //lrGridS.SetTileToLowerCostMat();


                if (currentStep >= path.steps) {
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
	
}
