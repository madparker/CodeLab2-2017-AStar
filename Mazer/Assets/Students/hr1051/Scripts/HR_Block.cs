using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HR_Block : MonoBehaviour {

	[SerializeField] SpriteRenderer myPlant;
	public HR_BlockSet myBlockSet;
	public Vector3 myGridPos;

	// Use this for initialization
	void Start () {
//		SetMyBlockSet (HR_Grid.Instance.GetMyBlockSet (HR_BlockSet.BlockType.Empty));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown () {
		HR_Grid.Instance.SetTarget (myGridPos);
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player") {
			if (myBlockSet.myBlockType == HR_BlockSet.BlockType.Flower) {
				SetMyBlockSet (HR_Grid.Instance.GetMyBlockSet (HR_BlockSet.BlockType.Empty));
			}
		}
	}

	public void SetMyBlockSet (HR_BlockSet g_blockSet){
		myBlockSet = g_blockSet;
		myPlant.sprite = g_blockSet.mySprite;
	}
}

[System.Serializable]
public class HR_BlockSet {
	public enum BlockType {
		Empty,
		Flower,
		Tree,
		Seed,
		Nut,
	}

	public BlockType myBlockType = BlockType.Empty;
	public Sprite mySprite;
	public float mySpeed;
	public float myCost;
}
