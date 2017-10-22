using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeScript : MonoBehaviour {

	Camera myCamera;

	public static float shakeStrength = 0f;

	void Start () {

		myCamera = GetComponent<Camera> ();
		
	}
	

	void Update () {
		Vector3 shakeOffset = Random.onUnitSphere;
//		shakeOffset.z = 0;

		myCamera.transform.localPosition = Vector3.Lerp(myCamera.transform.localPosition, shakeOffset * shakeStrength, Time.deltaTime *5f);

		if (shakeStrength > 0) {
			shakeStrength -= Time.deltaTime;
		}
			
	}
}
