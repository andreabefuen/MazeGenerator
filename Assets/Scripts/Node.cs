using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool IsWall;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public Node parent;

    public bool visited;
    public GameObject north;
    public GameObject east;
    public GameObject west;
    public GameObject south;

    public Node() {
        north = null;
        east = null; 
        west = null;
        south = null;
    }


    public Node(bool a_IsWall, Vector3 _worldPos, int _gridX, int _gridY)
    {
        IsWall = a_IsWall;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

}
