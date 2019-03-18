using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//First idea of what are the methods for the maze

public interface IMaze
{
    void GetNeighbours();
    void CreateWalls();
    void CreateCells();
    void DestroyWall(GameObject wall);
}
