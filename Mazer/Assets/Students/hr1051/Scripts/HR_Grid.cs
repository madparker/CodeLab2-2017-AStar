using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HR_Grid : MonoBehaviour {
	
	private static HR_Grid instance = null;

	//========================================================================
	public static HR_Grid Instance {
		get { 
			return instance;
		}
	}

	void Awake () {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
		} else {
			instance = this;
		}

		CreateGrid ();
	}
	//========================================================================

	public HR_Block[,] myGridArray;

	private string[] myMap = new string[]{
		"----tttttttt----",
		"----tfffffft----",
		"----tfttttft----",
		"----tftfftft----",
		"----tfttttft----",
		"----tfffffft----",
		"----tttttttt----",
		"----------------",
		"----------------",
	};

	public int myGridWidth;
	public int myGridHeight;
	[SerializeField] float mySpacing = 1;

	[SerializeField] GameObject myBlockPrefab;
	[SerializeField] HR_BlockSet[] myBlockSetArray;

	public Vector3 myCurrentPos = Vector3.one * 3;
	public Vector3 myTargetPos = Vector3.zero;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void CreateGrid () {
		if (myGridArray != null)
			return;
		
		myGridArray = new HR_Block[myGridWidth, myGridHeight];

		float t_offsetX = ((myGridWidth - 1) * -mySpacing) / 2f ;
		float t_offsetY = ((myGridHeight - 1) * -mySpacing) / 2f;

		for(int x = 0; x < myGridWidth; x++){
			for(int y = 0; y < myGridHeight; y++){

				GameObject t_block = Instantiate (myBlockPrefab, this.transform);

				t_block.name = "Block(" + x.ToString("0") + ")(" + y.ToString("0") + ")";

				t_block.transform.position = new Vector2 (t_offsetX + x * mySpacing, t_offsetY + y * mySpacing);

				myGridArray [x, y] = t_block.GetComponent<HR_Block> ();

				myGridArray [x, y].myGridPos = new Vector3 (x, y);

				myGridArray [x, y].SetMyBlockSet (GetFromMap (x, y));
			}
		}

		myGridArray [0, 0].SetMyBlockSet (GetMyBlockSet (HR_BlockSet.BlockType.Seed));
		myGridArray [myGridWidth - 1, 0].SetMyBlockSet (GetMyBlockSet (HR_BlockSet.BlockType.Nut));
	}

	public void SetTarget (Vector3 g_gridPos) {
		myTargetPos = g_gridPos;
		HR_AStar.Instance.InitAstar ();
	}

	private HR_BlockSet GetFromMap(int x, int y){

		char c = myMap [myGridHeight - 1 - y].ToCharArray () [x];

		HR_BlockSet t_set;

		switch(c){
		case 'f': 
			t_set = GetMyBlockSet (HR_BlockSet.BlockType.Flower);
			break;
		case 't': 
			t_set = GetMyBlockSet (HR_BlockSet.BlockType.Tree);
			break;
		default: 
			t_set = GetMyBlockSet (HR_BlockSet.BlockType.Empty);
			break;
		}

		return t_set;
	}

	public HR_BlockSet GetMyBlockSet (HR_BlockSet.BlockType g_type) {
		foreach (HR_BlockSet f_BlockSet in myBlockSetArray) {
			if (f_BlockSet.myBlockType == g_type)
				return f_BlockSet;
		}

		Debug.LogError ("cannot find type!");
		return null;
	}
}
