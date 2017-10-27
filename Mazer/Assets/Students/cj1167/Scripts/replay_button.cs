using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class replay_button : MonoBehaviour {

	public AudioSource audio;

	void Start(){

		audio = GetComponent<AudioSource>();

	}

	void Update () {

//		//GETTING THE MOUSE POSITION
//		Vector3 mouseScreenPos = Input.mousePosition; //position of mouse on screen
//		Vector3 mousePosition = Camera.main.ScreenToWorldPoint (mouseScreenPos); //convert to world coordinates
//		mousePosition.z = 0;//because it will otherwise be at z position of camera
//
//		//WAS THE MOUSE CLICKED
//		if (Input.GetMouseButtonDown (0)) { //clicked the left (0) mouseButton
//
//			//is the mouse on this object?
//			RaycastHit2D hit = Physics2D.Raycast (mousePosition, Vector2.zero);
//			if (hit.transform == transform) { //mouse clicked this object
//				//				beingDragged = true;
//				Application.LoadLevel (0);
//			}
//		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			
			audio.Play();
			Debug.Log ("audioplayed");
			SceneManager.LoadScene (0);
		}
	}


}