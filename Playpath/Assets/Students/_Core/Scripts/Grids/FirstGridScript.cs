using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FirstGridScript : GridScript {

	string[] gridString = new string[]{ 				//an array of strings
		"ww--|-rw-|----|------------", 			//ToCharArray is used to look at our string (which is also basically an array of characters) and get the position of a character
		"-ww-|-wr-rrrr-|---rrrr-----",			//down in line 42, we are using x to find a position in gridString[y]
		"-ww-|----r----|------r--www",
		"-ww-|----r----|------r--ww-",
		"-ww-|----rrrrrrrrrrrr--dww-",
		"-wwwwwwww|----|-------dww--",			//there will be an area in the script assigning values to the materials, like a difficulty level for each one
		"-wwwwwwwwww---|------d-----",			//we will be using the x, y to find the value of the spot
		"----|---www---|-----dd-----",
		"----|---www---|----dd------",
		"--ddd----w--www---dd-------",
		"--drd----wwww-w--dd--------",
		"--drd----|----wwdd---------",
		"--ddd----|----|wdd---------",
		"----dddddd----|------------",
		"----|----d----|------------",
	};

	// Use this for initialization
	void Start () {
		gridWidth = gridString[0].Length;
		gridHeight = gridString.Length;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override float GetMovementCost(GameObject go){
		return base.GetMovementCost(go);
	}
	
	protected override Material GetMaterial(int x, int y){ //getting the x and y from 2D grid array in GridScript, and using quad's x y

		char c = gridString[y].ToCharArray()[x]; //y is quad's space in 2D array (not literaly Unity transform position). //we are running a fucntion ToCharArray()[x] ON gridString[y]

		Material mat; 

		switch(c){
		case 'd': 
			mat = mats[1];
			break;
		case 'w': 
			mat = mats[2];
			break;
		case 'r': 
			mat = mats[3];
			break;
		default: 
			mat = mats[0];
			break;
		}
	
		return mat;
	}
}
