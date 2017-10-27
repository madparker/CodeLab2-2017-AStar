using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathScript {

	public string pathName;
	public int nodeInspected;

	public GridScript gridScipt;
	public List<Step> pathList = new List<Step>();

	public float score;
	public int steps;

	public GameManager gameManager;

	public PathScript(string name, GameManager gameManager){
		this.gameManager = gameManager;
		pathName = name;
	}

	public Step Get(int index){
		return pathList[index];
	}


	public void Insert (int index, GameObject go, Vector3 gridPos){
		float stepCost = gameManager.GetMovementCost(go);
		score += stepCost;

		pathList.Insert(index, new Step(go, stepCost, gridPos));

		steps++;
	}

	public void Insert (int index, GameObject go){
		float stepCost = gameManager.GetMovementCost(go);
		score += stepCost;

		pathList.Insert(index, new Step(go, stepCost));

		steps++;
	}


}
