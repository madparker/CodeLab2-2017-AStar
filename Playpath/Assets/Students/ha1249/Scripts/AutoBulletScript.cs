using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBulletScript : MonoBehaviour {

	void OnCollisionEnter(Collision coll){
		if (coll.gameObject.tag == "Player") {
			Destroy (gameObject);
		}
	}


}
