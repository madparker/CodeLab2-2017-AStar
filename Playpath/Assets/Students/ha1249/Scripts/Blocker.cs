using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour {

	public float recTimRegain = 0.2f;

	public float health;

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Bullet") {

			PlayerStats stats = col.gameObject.GetComponent<ProjectileScript> ().player.gameObject.GetComponent<PlayerStats> ();
			stats.maxRecTime += recTimRegain;
			col.gameObject.GetComponent<ProjectileScript> ().DestroyBullet (col.gameObject);

			Destroy (gameObject);
		}

		if (col.gameObject.tag == "Deflector") {
			Debug.Log ("Overlapping blocks!");
			Destroy (gameObject);
		}
	}

	public void DisableBlock(){
		GetComponent<Collider> ().enabled = false;
		GetComponent<MeshRenderer> ().enabled = false;
	}

	public void EnableBlock(){
		GetComponent<Collider> ().enabled = true;
		GetComponent<MeshRenderer> ().enabled = true;
	}

}
