using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject Princess;
	public GameObject Killer;
	public GameObject Guardian;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

//		if (Time.time > 10) {
//			Princess.SetActive (true);
//		}

		if (Time.timeSinceLevelLoad > 5) {
			Killer.SetActive (true);
		}

		if (Time.timeSinceLevelLoad > 15) {
			Guardian.SetActive (true);
		}
		
	}


}
