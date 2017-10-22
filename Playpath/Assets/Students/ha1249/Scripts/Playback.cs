using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playback : MonoBehaviour {

	Recording record;
	MainPlayer player;
	TeamAssignment input;
	KeyframeScript key;

	public float playbackSpeed = 0.2f;
	public bool isPlaying = false;
	public bool isPaused = false;

	bool nodeRecognized = false;


	float t = 0;
	float r = 0;
	float b = 0;

	void Start(){
		player = GetComponent<MainPlayer> ();
		input = GetComponent<TeamAssignment> ();
		key = GetComponent<KeyframeScript> ();
		record = GetComponent<Recording> ();

	}





	public void Play(){

		float playAmount = record.recTime - record.playTime;
		float playFill = UtilScript.remapRange (playAmount, record.recTime, 0, 1, 0);

		//		play.fillAmount = playFill;



		// Replaying the Player Movement 
		if (record.posIndex < record.recPos.Count) {



			if (Input.GetButtonDown (input.play)) {
				if (!isPlaying) {
					isPlaying = true;
					isPaused = false;
				} else {
					isPlaying = false;
					isPaused = true;
				}
			}

	

			if (isPlaying && !isPaused) {

//				player.PlaybackAction ();



				record.nextPos = record.recPos [record.posIndex];

				for (int i = 0; i < key.nodes.Count; i++) {

					//This line might be causing problems
					if (record.recPos [record.posIndex] == key.nodes[key.blockIndex]) {
						if (!nodeRecognized) {

							if (key.ready [key.blockIndex]) {
//								Debug.Log ("activating block");
								key.ActivateQueuedBlocker ();
							}

						
							nodeRecognized = true;
						}
					}
				}

				for (int i = 0; i < key.attkNode.Count; i++) {

					if (record.recPos [record.posIndex] == key.attkNode [key.attackIndex]) {
						if (!nodeRecognized) {

							if (key.attkReady [i]) {
								Debug.Log ("attacking");
								key.ActivateQueuedAttack ();
							}


							nodeRecognized = true;
						}
					}
				}
//				play.enabled = true;
//				rec.enabled = false;

				record.playTime += Time.deltaTime;

				transform.position = record.nextPos;

				if (transform.position == record.nextPos) {

			

					// Used to slow down incrementing
					t += Time.deltaTime;
					if (t >= playbackSpeed) { 
						record.posIndex++;
						nodeRecognized = false;
						t = 0;

					}
				}
			} 

		} else {

			// Put End of Playing calls and  Code here 

			key.ResetIndex ();
		
			record.posIndex = 0;
			record.playTime = 0;

			isPlaying = false;
			isPaused = false;

			if (!isPlaying && record.Segments == 0) {

				record.ReplenishSegments ();

			}




		}

		// Replaying the Player Rotation 

		if (record.rotIndex < record.recRot.Count) {

			if (isPlaying) {
				record.nextRot = record.recRot [record.rotIndex];
				transform.eulerAngles = record.nextRot;
				if (transform.eulerAngles == record.nextRot) {


					// Used to slow down incrementing
					r += Time.deltaTime;
					if (r >= playbackSpeed) { 
						record.rotIndex++;
						r = 0;
					}
				}	
			} 

		} else {
			record.rotIndex = 0;
		}


		// Replaying Player Actions

		if (record.buttonIndex < record.recButton.Count) {

			if (isPlaying) {
				record.nextButton = record.recButton [record.buttonIndex];
				bool action = record.nextButton;
				if (action == true) {
					player.PlayerAction ();

				}
				if (action == record.nextButton) {

					// Used to slow down incrementing
					b += Time.deltaTime;
					if (b >= playbackSpeed) { 
						player.hasFired = false;
						record.buttonIndex++;
						b = 0;
					}
				}	
			} 

		} else {

			record.buttonIndex = 0;
		}
	}
		
}
