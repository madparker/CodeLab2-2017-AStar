using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lrGridScript : GridScript {

    float perlinNoiseSeed;
    float perlinOffsetX;
    float perlinOffsetY;
    public float perlinFrequency;

    private void Awake(){
        perlinOffsetX = Random.Range(-1000f, 1000f);
        perlinOffsetY = Random.Range(-1000f, 1000f);
        //perlinNoiseSeed = Random.Range(0.1f, 1f);
    }

    protected override Material GetMaterial(int x, int y) {
        //Debug.Log(x + " " + y + " "+ Mathf.PerlinNoise((x+offsetX)*scale, (y + offsetY)*scale));

        float tilePerlin = Mathf.PerlinNoise((x + perlinOffsetX) * perlinFrequency, (y + perlinOffsetY) * perlinFrequency);

        float margin = 1f / 7f;
        int matToGet;
        Debug.Log(x + " " + y + " " + "tileperlin: " + tilePerlin);
        Debug.Log(margin);

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
}
