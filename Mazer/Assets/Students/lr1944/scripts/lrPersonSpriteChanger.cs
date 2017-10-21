using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lrPersonSpriteChanger : MonoBehaviour {


    [SerializeField] Color col1;
    [SerializeField] Color col2;
    [SerializeField] Color col3;
    [SerializeField] Color col4;
    [SerializeField] Color col5;
    [SerializeField] Color col6;
    [SerializeField] Color col7;
    [SerializeField] Color col8;
    [SerializeField] Color col9;
    [SerializeField] Color col10;

    SpriteRenderer spriteRend;

    List<Color> colors = new List<Color>();
	
	void Start () {

        spriteRend = gameObject.GetComponent<SpriteRenderer>();
        colors.Add(col1);
        colors.Add(col2);
        colors.Add(col3);
        colors.Add(col4);
        colors.Add(col5);
        colors.Add(col6);
        colors.Add(col7);
        colors.Add(col8);
        colors.Add(col9);
        colors.Add(col10);

        int rnd2 = Random.Range(0, 10);
        spriteRend.color = colors[rnd2];

        transform.localPosition = Random.insideUnitCircle;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
