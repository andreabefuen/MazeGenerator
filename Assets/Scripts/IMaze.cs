using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMaze
{
    void GetNeighbours();
    void CreateWalls();
    void CreateCells();
    void DestroyWall(GameObject wall);
}
