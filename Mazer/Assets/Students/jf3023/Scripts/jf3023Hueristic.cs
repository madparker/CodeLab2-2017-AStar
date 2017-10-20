using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jf3023Hueristic : HueristicScript {

    public override float Hueristic(int x, int y, Vector3 start, Vector3 goal, GridScript gridScript)
    {
        float dx = Mathf.Abs(x - goal.x);
        float dy = Mathf.Abs(y - goal.y);

        //Tie breaker, cross product implementation 
        float dx1 = x - goal.x;
        float dy1 = y - goal.y;
        float dx2 = start.x - goal.x;
        float dy2 = start.y - goal.y;
        float cross = Mathf.Abs(dx1 * dy2 - dx2 * dy1);

        float hueristic = 1.63f * (dx + dy);
        hueristic += cross * 0.001f;
        return hueristic;
    }

    public override float Hueristic(int x, int y, jAStar.Node start, jAStar.Node goal, GridScript gridScript)
    {
        float dx = Mathf.Abs(x - goal.x);
        float dy = Mathf.Abs(y - goal.y);

        //Tie breaker, cross product implementation 
        float dx1 = x - goal.x;
        float dy1 = y - goal.y;
        float dx2 = start.x - goal.x;
        float dy2 = start.y - goal.y;
        float cross = Mathf.Abs(dx1 * dy2 - dx2 * dy1);

        float hueristic = 1.63f * (dx + dy);
        hueristic += cross * 0.001f;
        return hueristic;
    }
}
