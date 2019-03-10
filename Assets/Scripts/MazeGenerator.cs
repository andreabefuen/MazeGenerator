using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MazeGenerator : MonoBehaviour
{
    public bool isDone = false;

    public Vector2 gridWorldSize;
    public GameObject wall;
    public GameObject floorCell;
    public float nodeRadius;
    public Cell[] cells;

    int gridSizeX;
    int gridSizeY;
    float nodeDiameter;
    int totalNodes;
    int totalWalls ;
    

    GameObject walls;
    GameObject[] allWalls;
    Vector3 initialPos;


    private void Awake()
    {
        nodeDiameter = nodeRadius * 2;

        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        wall.gameObject.transform.localScale = new Vector3(wall.transform.localScale.x, wall.transform.localScale.y, nodeDiameter);
        floorCell.gameObject.transform.localScale = new Vector3(nodeDiameter, floorCell.transform.localScale.y, nodeDiameter);

        totalNodes = gridSizeX * gridSizeY;
        cells = new Cell[totalNodes];

        
    }

    public Cell GetRandomCell()
    {
        int rdn = Random.Range(0, cells.Length);
        return cells[rdn];
    }
    void CreateWalls()
    {
        walls = new GameObject();
        walls.name = "Maze";

        initialPos = new Vector3((-gridSizeX / 2) + nodeRadius, 0f, (-gridSizeY / 2) + nodeRadius);
        Vector3 actualPos = initialPos;
        GameObject actualWall;

        //Vertical
        for (int x = 0; x < gridSizeY; x++)
        {
            for (int y = 0; y < gridSizeX + 1; y++)
            {
                actualPos = new Vector3(initialPos.x + (y * nodeDiameter) - nodeRadius, 0f, initialPos.z + (x * nodeDiameter) - nodeRadius);
                actualWall = Instantiate(wall, actualPos, Quaternion.identity);
                actualWall.transform.parent = walls.transform;
            }
        }

        actualPos = initialPos;
        //Horizontal
        for (int x = 0; x < gridSizeY + 1; x++)
        {
            for (int y = 0; y < gridSizeX; y++)
            {
                actualPos = new Vector3(initialPos.x + (y * nodeDiameter), 0f, initialPos.z + (x * nodeDiameter) - nodeDiameter);
                actualWall = Instantiate(wall, actualPos, Quaternion.Euler(0f, 90f, 0f));
                actualWall.transform.parent = walls.transform;
            }
        }

        totalWalls = walls.transform.childCount;
        allWalls = new GameObject[totalWalls];
    }

    void CreateCells()
    {

        int eastAux = 0;
        int indexChild = 0;
        int cont = 0;

        for (int i = 0; i < totalWalls; i++)
        {
            allWalls[i] = walls.transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < cells.Length; i++)
        {
            cells[i] = new Cell();
            cells[i].num = i;
            cells[i].wallEast = allWalls[eastAux];
            cells[i].wallSouth = allWalls[indexChild + (gridSizeX + 1) * gridSizeY];
            if(cont == gridSizeX)
            {
                eastAux += 2;
                cont = 0;
            }
            else
            {
                eastAux++;
            }
            cont++;
            indexChild++;
            cells[i].wallWest = allWalls[eastAux];
            cells[i].wallNorth = allWalls[(indexChild + (gridSizeX + 1) * gridSizeY) + gridSizeX - 1];
        }

        
    }

    void GetAllNeighbour()
    {
        foreach(Cell c in cells)
        {
            GetNeighbours(c);
        }
        isDone = true;
    }

    void DestroyWall(GameObject wall)
    {
        if(wall != null)
        {
            GameObject.Destroy(wall);
            
        }
    }

    public void TestLinkMaze()
    {
        LinkMaze(cells[23], cells[24]);
    }

    public void LinkMaze(Cell currentCell, Cell neighborCell)
    {

        bool wallDestroyed = false;

        int checkNode = 0;
        // int indexNeighbours = 0;
        checkNode = ( currentCell.num + 1) / gridSizeX;
        checkNode -= 1;
        checkNode *= gridSizeX;
        checkNode += gridSizeX;


        while (!wallDestroyed)
        {
            if(neighborCell != null)
            {
                //North
                if (currentCell.num + gridSizeX == neighborCell.num)
                {
                    DestroyWall(currentCell.wallNorth);
                    DestroyWall(neighborCell.wallSouth);
                    currentCell.wallNorth = null;
                    neighborCell.wallSouth = null;
                    wallDestroyed = true;
                }
                //South
                else if (currentCell.num - gridSizeX == neighborCell.num)
                {

                    DestroyWall(currentCell.wallSouth);
                    DestroyWall(neighborCell.wallNorth);

                    currentCell.wallSouth = null;
                    neighborCell.wallNorth = null;


                    wallDestroyed = true;
                }

                //West
                else if (currentCell.num + 1 == neighborCell.num)
                {

                    DestroyWall(currentCell.wallWest);
                    DestroyWall(neighborCell.wallEast);
                    currentCell.wallWest = null;
                    neighborCell.wallEast = null;
                    wallDestroyed = true;
                }
                //east
                else if (currentCell.num - 1 ==neighborCell.num)
                {

                    DestroyWall(currentCell.wallEast);
                    DestroyWall(neighborCell.wallWest);
                    currentCell.wallEast = null;
                    neighborCell.wallWest = null;
                    wallDestroyed = true;
                }
            }
            else
            {
                break;
            }
            
        }
       
    }
    
    void GetNeighbours (Cell currentNode)
    {
        int checkNode = 0;
       // int indexNeighbours = 0;
        checkNode = (currentNode.num +1) / gridSizeX;
        checkNode -= 1;
        checkNode *= gridSizeX;
        checkNode += gridSizeX;

        //West
        if (currentNode.num + 1 < totalNodes && (currentNode.num + 1) != checkNode)
        {
            if (cells[currentNode.num + 1].visited == false)
            {
                currentNode.neighbourWest = cells[currentNode.num + 1];
                //Debug.Log(currentNode.neighbourWest.num);
            }
        }
        //east
        if (currentNode.num + 1 >= 0 && (currentNode.num) != checkNode)
        {
            if (cells[currentNode.num - 1].visited == false)
            {
                currentNode.neighbourEast = cells[currentNode.num - 1];
               // Debug.Log(currentNode.neighbourEast.num);
            }
        }
        //North
        if (currentNode.num + gridSizeX < totalNodes)
        {
            if (cells[currentNode.num + gridSizeX].visited == false)
            {
                currentNode.neighbourNorth = cells[currentNode.num + gridSizeX];
               // Debug.Log(currentNode.neighbourNorth.num);
            }
        }
        //South
        if (currentNode.num - gridSizeX >= 0)
        {
            if (cells[currentNode.num - gridSizeX].visited == false)
            {
                currentNode.neighbourSouth = cells[currentNode.num - gridSizeX];
               // Debug.Log(currentNode.neighbourSouth.num);
              
            }
        }

        

    }

    public void RestartMaze()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    // Start is called before the first frame update
    void Start()
    {
        CreateWalls();
        CreateCells();
        //GetNeighbours(cells[7]);
        GetAllNeighbour();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
