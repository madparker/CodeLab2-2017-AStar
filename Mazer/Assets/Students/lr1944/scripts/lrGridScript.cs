using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lrGridScript : GridScript {
    
    float perlinOffsetX;
    float perlinOffsetY;
    public float perlinFrequency;

    private void Awake(){
        // random offset for changing maps
        perlinOffsetX = Random.Range(-1000f, 1000f);
        perlinOffsetY = Random.Range(-1000f, 1000f);
        // random start and goal positions
        start = new Vector3(Mathf.Floor(Random.Range(0, gridWidth)), Mathf.Floor(Random.Range(0, gridHeight)));
        goal = new Vector3(Mathf.Floor(Random.Range(0, gridWidth)), Mathf.Floor(Random.Range(0, gridHeight)));
        // reset goal if too close
        while (Vector3.Distance(start, goal)<gridWidth/2){
            goal = new Vector3(Mathf.Floor(Random.Range(0, gridWidth)), Mathf.Floor(Random.Range(0, gridHeight)));
        }
    }

    protected override Material GetMaterial(int x, int y) {

        // generate tile grid through perlin noise
        float tilePerlin = Mathf.PerlinNoise((x + perlinOffsetX) * perlinFrequency, (y + perlinOffsetY) * perlinFrequency);

       // function to smooth bell curve of perlin values to get more even distribution
        //float s = Mathf.Abs(tilePerlin - 0.5f);
        //if (x < 0.5f) {
        //    tilePerlin += s;
        //} else{
        //    tilePerlin -= s;
        //}

        float margin = 1f / (mats.Length-1);
        int matToGet;
        //Debug.Log(x + " " + y + " " + "tileperlin: " + tilePerlin);

        if (tilePerlin<margin){
            matToGet = 1;
            } else if (tilePerlin < margin*2) {
            matToGet = 2;
            } else if (tilePerlin < margin*3) {
            matToGet = 3;
            } else if (tilePerlin < margin*4) {
            matToGet = 4;
            } else if (tilePerlin < margin*5) {
            matToGet = 5;
            } else if (tilePerlin < margin*6) {
            matToGet = 6;
            }else {
            matToGet = 7;
        }

        return mats[matToGet];
        }

    public void SetTileToPath(int x, int y) {
        gridArray[x] [y].GetComponent<MeshRenderer>().sharedMaterial = mats[0];                                                    
    }

    public void SetTileToLowerCostMat(int x, int y) {

        Material currentMat = gridArray[x][y].GetComponent<MeshRenderer>().sharedMaterial;
        int currentCost = System.Array.IndexOf(mats, currentMat);

        if (currentCost == 0f) {
            SetTileToPath(x,y);
        } else {
            gridArray[x][ y].GetComponent<MeshRenderer>().sharedMaterial = mats[currentCost-1];
        }
    }
}
