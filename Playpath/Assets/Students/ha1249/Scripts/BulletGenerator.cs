using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator : MonoBehaviour {

	GameManager gameManager;

	int startPosX = 8;
	int startPosZ = 2;

	public GameObject prefab;


	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("[GameManager]").GetComponent<GameManager> ();

//		transform.position = gameManager.GetWorldPosition (8, 0);
		InvokeRepeating ("CreateBullet", 1, 1);
	

	}

	void CreateBullet(){
		GameObject bullet = Instantiate (prefab, gameManager.gridArray[startPosX,startPosZ].position, Quaternion.identity) as GameObject;

//		bullet.GetComponent<AstarAI> ().player = bullet.GetComponent<PlayerScript> ();
	}
	

}
