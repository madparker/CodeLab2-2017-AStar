using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Respawn : MonoBehaviour {

	float respawnAmount;
	GameObject player;

	bool respawning;

	float respawnDelay;

	Image ui;
	Text timeText;

	// Use this for initialization
	void Start () {
		ui = GetComponent<Image> ();
		respawnAmount = 0;

		timeText = GetComponentInChildren<Text> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (respawning) {

//			player.GetComponent<HealthScript> ().healthText.enabled = false;
//			player.GetComponent<FuelScript> ().fuelText.enabled = false;

			respawnAmount += Time.deltaTime;
			float respawnCounter = UtilScript.remapRange (respawnAmount, 0, respawnDelay, 0, 1);
			ui.fillAmount = respawnCounter;

			timeText.text = Mathf.Clamp (respawnDelay - respawnAmount,0,respawnDelay).ToString ("#0.0");

			if (respawnAmount >= respawnDelay) {

				respawning = false;
//				player.GetComponent<HealthScript> ().healthText.enabled = true;
//				player.GetComponent<FuelScript> ().fuelText.enabled = true;
//				Destroy (this.gameObject);

			}
		}
		
	}

	public void StartRespawn(GameObject obj, float delay){
		player = obj;
		respawnDelay = delay;

		respawning = true;
	}


}
