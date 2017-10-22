using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLHueristicScript : HueristicScript {

    public override float Hueristic(int x, int y, Vector3 start, Vector3 goal, GridScript gridScript) {
        return gridScript.GetMovementCost(gridScript.GetGrid()[x][ y]);
    }
}
