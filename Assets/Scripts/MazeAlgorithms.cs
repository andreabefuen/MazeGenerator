﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeAlgorithms : MonoBehaviour
{

    //HUNT AND KILL ALGORITHM
    public GameObject maze;
    public MazeGenerator mazeGenerator;

    int cont = 0;

    int count = 1;

    private Cell currentCell;
    Cell neighbourCell;
    bool courseCompleted;

    bool killFinish = false;
    // Start is called before the first frame update
    void Start()
    {
        maze = GameObject.Find("MazeGenerator");
        mazeGenerator = maze.GetComponent<MazeGenerator>();

        currentCell = mazeGenerator.GetRandomCell();
    }
    //Destroy the north wall, only testing
    /*void DestroyNorthWall(Cell currentCell)
    {
        if(currentCell.wallNorth != null)
        {
            GameObject aux = currentCell.wallNorth;
            Destroy(aux);
        }
    
    }*/


    //This is the start of the algorithm, its call when you hit the HuntAndKill button
    public void HKAlgorithm()
    {
        this.gameObject.GetComponent<CreationMazeSizeUI>().InteractablePlayButton();
        KillAlgorithm();
        if(courseCompleted == true)
        {
            Debug.Log("Finish!");
        }
    }
    //KILL PHASE
    public void KillAlgorithm()
    {
        

        List<Cell> unvisitedNeighbours = new List<Cell>();

        if (CellAvaible(currentCell.neighbourNorth))
        {
            unvisitedNeighbours.Add(currentCell.neighbourNorth);
        }
        if (CellAvaible( currentCell.neighbourSouth))
        {
            unvisitedNeighbours.Add(currentCell.neighbourSouth);
        }
        if (CellAvaible( currentCell.neighbourWest))
        {
            unvisitedNeighbours.Add(currentCell.neighbourWest);
        }
        if (CellAvaible(currentCell.neighbourEast))
        {
            unvisitedNeighbours.Add(currentCell.neighbourEast);
        }
        if(unvisitedNeighbours.Count > 0)
        {
            int index = Random.Range(0, unvisitedNeighbours.Count);
            Debug.Log(unvisitedNeighbours.Count);
            neighbourCell = unvisitedNeighbours[index];
            currentCell.visited = true;
            mazeGenerator.LinkMaze(currentCell, neighbourCell);

            currentCell = neighbourCell;

            KillAlgorithm();
        }
        else
        {
            currentCell.visited = true;
            killFinish = true;
            HuntAlgorithm();
        }
    }
    //HUNT PHASE
    public void HuntAlgorithm()
    {
        courseCompleted = true;
        foreach(Cell c in mazeGenerator.cells)
        {
            if(c.visited == false && CellHasAdjacentVisitedCells(c))
            {
                currentCell = c;
                courseCompleted = false;
                mazeGenerator.LinkMaze(currentCell, AdjacentVisitedCell(c));
                KillAlgorithm();
            }
        }
    }


    bool CellAvaible(Cell c)
    {
        return (c != null && c.visited == false);
    }

    bool RouteAvaibles(Cell cell) //Returns if the cell have neighbours that you can choose
    {
        int availableRoutes = 0;
        if(cell.neighbourEast!= null && cell.neighbourEast.visited == false)
        {
            availableRoutes++;
        }
        if (cell.neighbourNorth!=null && cell.neighbourNorth.visited == false)
        {
            availableRoutes++;

        }
        if (cell.neighbourSouth != null && cell.neighbourSouth.visited == false)
        {
            availableRoutes++;

        }
        if (cell.neighbourWest != null && cell.neighbourWest.visited == false)
        {
            availableRoutes++;

        }

        return availableRoutes > 0;
    }


    bool CellHasAdjacentVisitedCells(Cell cell)
    {
        int visitedCells = 0;
        
        if (cell.neighbourEast != null && cell.neighbourEast.visited)
        {
            visitedCells++;
        }
        if (cell.neighbourWest !=null && cell.neighbourWest.visited)
        {
            visitedCells++;
        }
        if (cell.neighbourSouth !=null && cell.neighbourSouth.visited)
        {
            visitedCells++;
        }
        if (cell.neighbourNorth!=null &&cell.neighbourNorth.visited)
        {
            visitedCells++;
        }
        return visitedCells > 0;
    }

    Cell AdjacentVisitedCell(Cell cell)
    {
        if (cell.neighbourEast != null && cell.neighbourEast.visited)
        {
            return cell.neighbourEast;
        }
       else if (cell.neighbourWest != null && cell.neighbourWest.visited)
        {
            return cell.neighbourWest;
        }
        else if (cell.neighbourSouth != null && cell.neighbourSouth.visited)
        {
            return cell.neighbourSouth;
        }
        else 
        {
            return cell.neighbourNorth;
        }
    }

    public void RandomCellButton()
    {
        Debug.Log("Random cell: "+ mazeGenerator.GetRandomCell().num);
        
    }

    /*public void DestroyNorthButton()
    {
        DestroyNorthWall(mazeGenerator.cells[cont]);
        int aux = mazeGenerator.cells[cont].neighbourNorth.num;
        cont = aux;
    }*/
    // Update is called once per frame
    void Update()
    {

    }
}
