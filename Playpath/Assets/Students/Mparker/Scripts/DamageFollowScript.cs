using UnityEngine;
using System.Collections;

public class DamageFollowScript : FollowAStarScript {
	
	public float health = 100;

	bool init = false;

	public float speed;
	public float toughness;

	public DamageGridScript dgs; 

	// Use this for initialization
	protected override void Start () {
	}

	protected virtual void Update () {

		if(!init){
			init = !init;
			path = astar.path;
			startPos = path.Get(0);
			destPos  = path.Get(currentStep);
			
			transform.position = startPos.gameObject.transform.position;
			
			Debug.Log(path.nodeInspected/100f);
			
			Invoke("StartMove", path.nodeInspected/100f);
			
			startTime = Time.realtimeSinceStartup;
		}
		
		if(move){
			lerpPer += (Time.deltaTime * (speed + 1))/destPos.moveCost;
			
			transform.position = Vector3.Lerp(startPos.gameObject.transform.position, 
			                                  destPos.gameObject.transform.position, 
			                                  lerpPer);
			float damage = dgs.GetDamageCost(destPos.gameObject);

			health -= (damage/200f)/toughness;
			
			if(lerpPer >= 1){
				lerpPer = 0;
				
				currentStep++;
				
				if(currentStep >= path.steps){
					currentStep = 0;
					move = false;
					Debug.Log(path.pathName + " got to the goal in: " + (Time.realtimeSinceStartup - startTime));
					Debug.Log(path.pathName + " travel time: " + (Time.realtimeSinceStartup - travelStartTime));
				} 
				
				startPos = destPos;
				destPos = path.Get(currentStep);
			}
		}
	}
}
