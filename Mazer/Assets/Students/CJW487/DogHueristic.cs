using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogHueristic : HueristicScript {

    public override float Hueristic(int x, int y, Vector3 start, Vector3 goal, GridScript gridScript)
    {
        /*
         *  Developed by Chris Williams
         * 
         *  DEPTH FRIST SEARCH DOG BUILD
         *      STATS:
         *          DPTH    *****
         *          BRTH        *
         *          CUTE    *****
         * 
         *  BUILD OVERVIEW:
         *  
         *      This is a Princess build heavily spec'd in depth first search. 
         *      Its hueristic is based on Manhattan distance formula. The Manhattan 
         *      distance is a fast-to-compute to exit. 
         *  
         *      The Manhattan distance is distance between two points if you were 
         *      only allowed to walk on paths that were at 90-degree angles from 
         *      each other (similar to walking the streets of Manhattan).
         *  
         *  Manhattan Distance Formula:
         *      Distance = | cellx - exitx | + | celly - exity |
         *      
         *  Build Formula:
         *      Hueristic = (|pos.X - goal.X| + |pos.Y - goal.Y|) ^ 3
         *      
         *  Why multiply by a power of 3:
         *      To discourage searching other paths and go untilthe goal is found.
         *      A power of 2 does not create enough distance between the current cost 
         *      and a potential new cost and a power greater than 3 does not add anything 
         *      more to the depth first search calculation.
         *      
         *  Build Strengths:
         *      On maps with costs that are all the sameand have limited obstacles
         *      the early start time compared to other builds will secure you an
         *      easy win.
         * 
         *  Build Weaknesses:
         *      On maps with many obstacles this heavily greedy-based approach will
         *      get you in trouble. 
         */

        return Mathf.Pow(Mathf.Abs(x - goal.x) + Mathf.Abs(y - goal.y), 3);
    }
}
