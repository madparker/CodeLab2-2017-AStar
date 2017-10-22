using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour {

	float points;

	public enum Team {
		TEAM_A_GOAL,
		TEAM_B_GOAL,

	}

	public Team myTeam;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

//	void OnTriggerEnter (Collider col){
//		if (col.gameObject.tag == "Bullet") {
//			
////			points = col.gameObject.GetComponent<BulletScript> ().bulletPower * 0.01f;
////			ScreenShakeScript.shakeStrength = points * 0.2f;
//
//			if (myTeam == Team.TEAM_A_GOAL) {
//				if (col.gameObject.GetComponent<BulletScript> ().playerNum == 1) {
//					ScoreManager.p1Score -= points;
//					col.gameObject.GetComponent<BulletScript> ().ResetProjectileStatus ();
//					DestroyBullet (col.gameObject);
//				} else {
//					ScoreManager.p2Score += points;
//					col.gameObject.GetComponent<BulletScript> ().ResetProjectileStatus ();
//					DestroyBullet (col.gameObject);
//				}
//			} else {
//				if (col.gameObject.GetComponent<BulletScript> ().playerNum == 2) {
//					ScoreManager.p2Score -= points; 
//					col.gameObject.GetComponent<BulletScript> ().ResetProjectileStatus ();
//					DestroyBullet (col.gameObject);
//				} else {
//					ScoreManager.p1Score += points; 
//					col.gameObject.GetComponent<BulletScript> ().ResetProjectileStatus ();
//					DestroyBullet (col.gameObject);
//				}
//			}
//		}
//	}

	void DestroyBullet(GameObject bullet){
		Destroy (bullet);
	}
}
