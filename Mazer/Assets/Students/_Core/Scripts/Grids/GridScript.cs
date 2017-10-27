using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridScript : MonoBehaviour {

	public int gridWidth;
	public int gridHeight;
	public float spacing;
	
	public Material[] mats;
	public float[]   costs;

	public List<Vector3> start;
    public Vector3 goal;
    //public List<Vector3> goal;
	
	GameObject[,] gridArray;
	
	public GameObject startSprite;
	public GameObject goalSprite;
    public Transform startPrefab;

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
					GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
					quad.transform.localScale = new Vector3(spacing, spacing, spacing);
					quad.transform.position = new Vector3(offsetX + x * spacing, 
					                                      offsetY - y * spacing, 0);

					quad.transform.parent = transform;

					gridArray[x, y] = quad;
					
					quad.GetComponent<MeshRenderer>().sharedMaterial = GetMaterial(x, y);

                    //foreach (Vector3 goalPos in goal)
                    //{
                        if (goal.x == x && goal.y == y)
                        {
                            goalSprite.transform.position = quad.transform.position;
                        }
                    //}
                    foreach (Vector3 startPos in start)
                    {
                        if (startPos.x == x && startPos.y == y)
                        {
                            Instantiate(startPrefab, quad.transform.position, Quaternion.identity);
                            //startSprite.transform.position = quad.transform.position;
                        }
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
