using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Path {

	public string pathName;
	public int nodeInspected;

	public GameManager gameManager;
	public List<Step> path = new List<Step>();

	public float score;
	public int steps;

	public Path(string name, GameManager gameManager){
		this.gameManager = gameManager;
		pathName = name;
	}

	public Step Get(int index){
		return path[index];
	}
	
	public virtual void Insert(int index, GameObject go){
		float stepCost = gameManager.GetMovementCost(go);
		score += stepCost;
		
		path.Insert(index, new Step(go, stepCost));
		
		steps++;
	}

	public virtual void Insert(int index, GameObject go, Vector3 gridPos){
		float stepCost = gameManager.GetMovementCost(go);
		score += stepCost;
		
		path.Insert(index, new Step(go, stepCost, gridPos));

		steps++;
	}
}
