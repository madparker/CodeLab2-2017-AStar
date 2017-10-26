using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EmptyGridScript : GridScript
{

    string[] gridString = new string[]{
        "---------------------------",
        "---------------------------",
        "---------------------------",
        "---------------------------",
        "---------------------------",
        "---------------------------",
        "---------------------------",
        "---------------------------",
        "---------------------------",
        "---------------------------",
        "---------------------------",
        "---------------------------",
        "---------------------------",
        "---------------------------",
        "---------------------------",
    };

    // Use this for initialization
    void Start()
    {
        gridWidth = gridString[0].Length;
        gridHeight = gridString.Length;
        GameObject[,] pos = GetGrid();
        playerCharacter.transform.position = pos[0, 0].transform.position;
        goal = playerCharacter.transform.position;
        gridWidth = gridString[0].Length;
        gridHeight = gridString.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //playerCharacter.transform.position = new Vector3(playerCharacter.transform.position.x + spacing, playerCharacter.transform.position.y);
            GameObject[,] pos = GetGrid();
            playerCharacter.transform.position = pos[(int)playerCharacter.transform.position.x + 1, (int)playerCharacter.transform.position.y].transform.position;
            goal = playerCharacter.transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //playerCharacter.transform.position = new Vector3(playerCharacter.transform.position.x - spacing, playerCharacter.transform.position.y);
            //GameObject[,] pos = GetGrid();
            //playerCharacter = pos[(int)playerCharacter.transform.position.x - (int)spacing, (int)playerCharacter.transform.position.y];
        }
    }

    public override float GetMovementCost(GameObject go)
    {
        return base.GetMovementCost(go);
    }

    public override GameObject[,] GetGrid()
    {
        return base.GetGrid();
    }

    protected override Material GetMaterial(int x, int y)
    {

        char c = gridString[y].ToCharArray()[x];

        Material mat;

        switch (c)
        {
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
