using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrincessManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.name == "Killer") {
			
			this.gameObject.SetActive (false);
			SceneManager.LoadScene (2);

		}
	}
}
