using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lrManager : MonoBehaviour {

	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Application.LoadLevel(Application.loadedLevel);
        }
	}
}
