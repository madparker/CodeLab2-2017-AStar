using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatHuman : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D collider){
		Destroy(collider.gameObject);
	}
}
