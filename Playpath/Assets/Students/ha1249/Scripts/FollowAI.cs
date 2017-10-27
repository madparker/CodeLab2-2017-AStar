using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAI : FollowAStarScript {


	protected override void Start (){
		
		path = astar.path;
		//Invoke("StartMove", 0f);
		Debug.Log (astar.path.path.Count);
		startPos = path.Get(0);
		destPos  = path.Get(currentStep);

		transform.position = startPos.gameObject.transform.position;

		//		Debug.Log(path.nodeInspected/100f);



		startTime = Time.realtimeSinceStartup;

		Debug.Log ("start follow AI");
		StartMove ();


	}

	protected override void Update () {

		if(move){
			lerpPer += Time.deltaTime/destPos.moveCost;

			transform.position = Vector3.Lerp(startPos.gameObject.transform.position, 
				destPos.gameObject.transform.position, 
				lerpPer);

			if(lerpPer >= 1){
				lerpPer = 0;

				currentStep++;

				if(currentStep >= path.steps){
					currentStep = 0;
					move = false;
					Debug.Log(path.pathName + " got to the goal in: " + (Time.realtimeSinceStartup - startTime));
					Debug.Log(path.pathName + " travel time: " + (Time.realtimeSinceStartup - travelStartTime));
					Destroy (gameObject);

				
				}

				startPos = destPos;
				destPos = path.Get(currentStep);


			}


		}
	}

	protected override void StartMove(){
		Debug.Log ("Invoked");
		move = true;
		travelStartTime = Time.realtimeSinceStartup;
	}


}
