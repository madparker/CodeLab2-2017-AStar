using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour {
	[SerializeField] Transform swimer;
	[SerializeField] Vector3 endGoal;
	[SerializeField] GridScript grid;
	// Use this for initialization
	
	// Update is called once per frame
	void Start(){
		endGoal = grid.GetGrid()[(int)endGoal.x,(int)endGoal.y].transform.position;
	}
	void Update () {
		if(swimer.position == endGoal){
			Application.Quit();
			UnityEditor.EditorApplication.isPlaying = false;
			Debug.Log("Quit");
		}
	}
}
