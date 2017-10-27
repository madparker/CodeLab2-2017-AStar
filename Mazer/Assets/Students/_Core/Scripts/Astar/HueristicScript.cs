using UnityEngine;
using System.Collections;

public class HueristicScript : MonoBehaviour {
    public GridScript gridScript;
    public int id;

    private void Start()
    {
        id = Random.Range(0, gridScript.start.Count);
    }
    public virtual float Hueristic(int x, int y, Vector3 start, Vector3 goal, GridScript gridScript){
        float dx = Mathf.Abs(x - goal.x);
        float dy = Mathf.Abs(y - goal.y);

        //Tie breaker, cross product implementation 
        float dx1 = x - goal.x;
        float dy1 = y - goal.y;
        float dx2 = start.x - goal.x;
        float dy2 = start.y - goal.y;
        float cross = Mathf.Abs(dx1*dy2 -dx2*dy1);

        float hueristic = 1.63f  * (dx+dy);
        hueristic += cross * 0.001f;
        return hueristic;
	}
}
