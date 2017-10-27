using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour {

	public float x;
	public float y;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.T)) {
			
			x = this.transform.position.x - 1f;
			NotFollow ();
	
		}

		if (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.P)) {

			x = this.transform.position.x + 1f;
			NotFollow ();
		}

		if (Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.X)) {

			y = this.transform.position.y + 1f;
			NotFollow ();
		}

		if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Z)) {

			y = this.transform.position.y - 1f;
			NotFollow ();
		}

		this.transform.position = new Vector2 (x, y);
	}

	void NotFollow(){

		this.GetComponent<FollowAStarScript> ().enabled = false;


	}

	void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.name == "Killer") {
			other.gameObject.SetActive (false);
			SceneManager.LoadScene (1);
		}
	}





}
