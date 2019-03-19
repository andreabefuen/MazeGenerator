
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursiveBacktracker : MonoBehaviour
{
   

    public GameObject maze;
    public MazeGenerator mazeGenerator;

    int cont = 0;

    int count = 1;

    private Cell currentCell;
    Cell neighbourCell, auxCell;

    Stack<Cell> stackCells;



    // Start is called before the first frame update
    void Start()
    {
        maze = GameObject.Find("MazeGenerator");
        mazeGenerator = maze.GetComponent<MazeGenerator>();
        stackCells = new Stack<Cell>();

        currentCell = mazeGenerator.GetRandomCell();
        
    }

    void AddCellToStack(Cell cell)
    {
        stackCells.Push(cell);
    }

    //REMOVE THE CELL
    Cell GetCellToStack()
    {
        return stackCells.Pop();
    }

    //Returns true if the cell is avaible
    // cell is not null and cell is not visited
    bool CellAvaible(Cell c)
    {
        return (c != null && c.visited == false);
    }

    bool RouteAvaibles(Cell cell) //Returns if the cell have neighbours that you can choose
    {
        int availableRoutes = 0;
        if (cell.neighbourEast != null && cell.neighbourEast.visited == false)
        {
            availableRoutes++;
        }
        if (cell.neighbourNorth != null && cell.neighbourNorth.visited == false)
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

    public void RecursiveBacktrackerAlgorithm()
    {

        this.gameObject.GetComponent<CreationMazeSizeUI>().InteractablePlayButton();

        currentCell = mazeGenerator.GetRandomCell();

        RecursiveBTA();

        

    }

    private void RecursiveBTA()
    {
        List<Cell> unvisitedNeighbours = new List<Cell>();

        if (CellAvaible(currentCell.neighbourNorth))
        {
            unvisitedNeighbours.Add(currentCell.neighbourNorth);
        }
        if (CellAvaible(currentCell.neighbourSouth))
        {
            unvisitedNeighbours.Add(currentCell.neighbourSouth);
        }
        if (CellAvaible(currentCell.neighbourWest))
        {
            unvisitedNeighbours.Add(currentCell.neighbourWest);
        }
        if (CellAvaible(currentCell.neighbourEast))
        {
            unvisitedNeighbours.Add(currentCell.neighbourEast);
        }
        if (unvisitedNeighbours.Count > 0)
        {
            
            int index = Random.Range(0, unvisitedNeighbours.Count -1);
            neighbourCell = unvisitedNeighbours[index];
            currentCell.visited = true;
            mazeGenerator.LinkMaze(currentCell, neighbourCell);
            currentCell = neighbourCell;
            AddCellToStack(neighbourCell);
            RecursiveBTA();
        }
        else
        {
            currentCell.visited = true;
            if (stackCells.Count > 0)
            {
                currentCell = GetCellToStack();
                RecursiveBTA();

            }
            else
            {
                Debug.Log("Its done!");
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
