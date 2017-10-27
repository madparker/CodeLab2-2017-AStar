using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBlocker : MonoBehaviour {

	public void DisableBlock(){
		GetComponent<Collider> ().enabled = false;
		GetComponent<MeshRenderer> ().enabled = false;
	}

	public void EnableBlock(){
		GetComponent<Collider> ().enabled = true;
		GetComponent<MeshRenderer> ().enabled = true;
	}

}
