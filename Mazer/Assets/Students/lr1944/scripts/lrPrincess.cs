using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lrPrincess : MonoBehaviour {





	void Update () {
		gameObject.transform.RotateAround (gameObject.transform.position, Vector3.forward, 1*Time.deltaTime);
	}
}
