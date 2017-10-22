using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using kitkat;

namespace kitkat
{
    public class EggScript : HueristicScript
    {
        float ManHattanHeuristic(Vector3 a, Vector3 b)
        {
            return (Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y));
        }

        public override float Hueristic(int x, int y, Vector3 start, Vector3 goal, GridScript gridScript)
        {
            return gridScript.costs[0] * ManHattanHeuristic(start, goal);
        }
    }
}