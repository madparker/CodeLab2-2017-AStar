using UnityEngine;
using System.Collections;

public class ChaoHueristicScript : HueristicScript {

	GameObject[,] pos;
		
	public override float Hueristic(int x, int y, Vector3 start, Vector3 goal, GridScript gridScript){

		pos = gridScript.GetGrid();

		GameObject go = pos[x,y];

		Material mat = go.GetComponent<MeshRenderer>().sharedMaterial;

		if (mat.name == "Forest" || mat.name == "Water") {
			return 1000000000;
		
		}

		else return 0;



	}

}
