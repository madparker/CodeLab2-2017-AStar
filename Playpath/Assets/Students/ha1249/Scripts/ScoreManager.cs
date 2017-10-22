using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {



	public Text p1ScoreText;
	public Text p2ScoreText;

	public static float p1Score;
	public static float p2Score;

	// Use this for initialization
	void Start () {
		p1Score = 0;
		p2Score = 0;
	}
	
	// Update is called once per frame
	void Update () {

		p1ScoreText.text = p1Score.ToString();
		p2ScoreText.text = p2Score.ToString();
		
	}
}
