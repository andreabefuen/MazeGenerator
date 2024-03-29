﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This class contains all the things that our cells needs
//I want to store many information because its easier to access for the algorithms


[System.Serializable]
public class Cell :ScriptableObject
{
    public bool visited;
    public int num;
    public GameObject wallNorth; //0
    public GameObject wallEast; //
    public GameObject wallWest; //
    public GameObject wallSouth; //
    public Cell neighbourNorth; //0
    public Cell neighbourEast; //
    public Cell neighbourWest; //
    public Cell neighbourSouth; //

    public Cell()
    {

        visited = false;
        num = -1;
        wallNorth = null;
        wallEast = null;
        wallWest = null;
        neighbourSouth = null;
        neighbourNorth = null;
        neighbourEast = null;
        neighbourWest = null;
        neighbourSouth = null;
    }
}
