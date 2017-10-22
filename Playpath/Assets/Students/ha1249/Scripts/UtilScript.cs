using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilScript : MonoBehaviour {

	public static float remapRange(float oldValue, float oldMin, float oldMax, float newMin, float newMax ){
		float newValue = 0;
		float oldRange = (oldMax - oldMin);
		float newRange = (newMax - newMin);
		newValue = (((oldValue - oldMin) * newRange) / oldRange) + newMin;
		return newValue;


	}

	public static Vector3 CalculateCentroid (Vector3[] points, float yOffset){
		
		float sumX = 0;
		float sumZ = 0; 

		for (int i = 0; i < points.Length; i++) {
			sumX += points [i].x;
			sumZ += points [i].z;
		}

		return new Vector3 ((sumX / points.Length), yOffset, (sumZ / points.Length));

	}


	//Tweening formula:  x+= (targetposition - currentposition) * 0.1f;

	// Zeno's paradox - for game animation
}
