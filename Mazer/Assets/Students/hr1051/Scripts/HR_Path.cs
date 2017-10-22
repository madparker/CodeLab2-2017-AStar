using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HR_Path {

	public string pathName;
	public int nodeInspected;

	public List<Step> path = new List<Step>();

//	public float score;
	public int steps;

	public HR_Path(string name){
		pathName = name;
	}

	public Step Get(int index){
		return path [index];
	}

	public virtual void Insert(int index, HR_Block g_block){
		float stepCost = g_block.myBlockSet.mySpeed;
//		score += stepCost;

		path.Insert(index, new Step(g_block.gameObject, stepCost));

		steps++;
	}

	public virtual void Insert(int index, HR_Block g_block, Vector3 gridPos){
		float stepCost = g_block.myBlockSet.mySpeed;
//		score += stepCost;

		path.Insert(index, new Step(g_block.gameObject, stepCost, gridPos));

		steps++;
	}
}
