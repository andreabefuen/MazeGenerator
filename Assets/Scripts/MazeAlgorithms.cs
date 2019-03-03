using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeAlgorithms : MonoBehaviour
{
    public GameObject maze;
    public MazeGenerator mazeGenerator;

    int cont = 0;
    // Start is called before the first frame update
    void Start()
    {
        maze = GameObject.Find("MazeGenerator");
        mazeGenerator = maze.GetComponent<MazeGenerator>();
    }

    void DestroyNorthWall(Cell currentCell)
    {
        if(currentCell.wallNorth != null)
        {
            GameObject aux = currentCell.wallNorth;
            Destroy(aux);
        }
    
    }

    public void DestroyNorthButton()
    {
        DestroyNorthWall(mazeGenerator.cells[cont]);
        int aux = mazeGenerator.cells[cont].neighbourNorth.num;
        cont = aux;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
