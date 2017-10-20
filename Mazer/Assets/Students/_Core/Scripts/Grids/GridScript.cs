using UnityEngine;
using System.Collections;

public class GridScript : MonoBehaviour {

	public int gridWidth;
	public int gridHeight;
	public float spacing;
	
	public Material[] mats;
	public float[]   costs;

	public Vector3 start = new Vector3(0,0);
	public Vector3 goal = new Vector3(14,14);
	
	GameObject[,] gridArray;
	
	public GameObject startSprite;
	public GameObject goalSprite;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public virtual GameObject[,] GetGrid(){

		if(gridArray == null){

			gridArray = new GameObject[gridWidth, gridHeight];
			
			float offsetX = (gridWidth  * -spacing)/2f;
			float offsetY = (gridHeight * spacing)/2f;

			for(int x = 0; x < gridWidth; x++){
				for(int y = 0; y < gridHeight; y++){
					GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);				//a quad is a primitive that we put our sprites on, this is creating it
					quad.transform.localScale = new Vector3(spacing, spacing, spacing);				//scaling the position
					quad.transform.position = new Vector3(offsetX + x * spacing, 					//setting the position (guess: probably so they're spaced properly not overlapping)
					                                      offsetY - y * spacing, 0);

					quad.transform.parent = transform;												//setting parent in heirarchy

					gridArray[x, y] = quad;															//seperate array keeping track of what's in the grid
					
					quad.GetComponent<MeshRenderer>().sharedMaterial = GetMaterial(x, y);			//getting the MeshRenderer of current quad, setting its material using Matt's function "GetMaterial"

					if(goal.x == x && goal.y == y){													//if the positon we're looking at is the goal block, add the goal sprite
						goalSprite.transform.position = quad.transform.position;
					}
					if(start.x == x && start.y == y){												//if the position we're looking at is the start block, add the start sprite
						startSprite.transform.position = quad.transform.position;
					}
				}
			}
		}

		return gridArray;
	}

	public virtual float GetMovementCost(GameObject go){
		Material mat = go.GetComponent<MeshRenderer>().sharedMaterial;
		int i;

		for(i = 0; i < mats.Length; i++){
			if(mat.name.StartsWith(mats[i].name)){
				break;
			}
		}

		return costs[i];
	}

	protected virtual Material GetMaterial(int x, int y){
		return mats[Random.Range(0,mats.Length)];
	}
}
