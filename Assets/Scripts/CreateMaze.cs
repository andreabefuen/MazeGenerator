using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMaze : MonoBehaviour
{
    [System.Serializable]
    public class Node
    {
        public bool visited;
        public GameObject north;
        public GameObject east ;
        public GameObject west ;
        public GameObject south;

    }

    private bool startedBuilding = false;
    private int currentNeighbour = 0;
    private List<int> lastNode;
    private int backingUp = 0;

    private int visitedNodes;

    public bool activeOnDraw;
    public GameObject wall;

    public Vector2 gridWorldSize;
    public float nodeRadius;

    public int currentNode;
    private int totalNodes;

    public Node[] nodes;

    int gridSizeX;
    int gridSizeY;
    float nodeDiameter;

    GameObject walls;

    Vector3 initialPos;

    Node[,] grid;





    private void Awake()
    {
        nodeDiameter = nodeRadius * 2;

        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        wall.gameObject.transform.localScale = new Vector3(wall.transform.localScale.x, wall.transform.localScale.y, nodeDiameter);
        //CreateGrid();

        //CreateCells();
    }

    void CreateMazeDFS()
    {
       if(visitedNodes < totalNodes)
        {
            if (startedBuilding)
            {
                GetNeighbours();
                if(nodes[currentNeighbour].visited == false && nodes[currentNode].visited == true)
                {
                    BreakWall();
                    nodes[currentNeighbour].visited = true;
                    visitedNodes++;
                    lastNode.Add(currentNode);
                    currentNode = currentNeighbour;
                    if(lastNode.Count> 0)
                    {
                        backingUp = lastNode.Count - 1;
                    }
                }
            }
            else
            {
                currentNode = UnityEngine.Random.Range(0, totalNodes);
                nodes[currentNode].visited = true;
                visitedNodes++;
                startedBuilding = true;
            }
            Invoke("CreateMazeDFS", 1f);
        }
       
    }

    private void BreakWall()
    {
        
    }

    void CreateWalls()
    {
        walls = new GameObject();
        walls.name = "Maze";

        initialPos = new Vector3((-gridSizeX / 2) + nodeRadius, 0f, (-gridSizeY / 2) + nodeRadius);
        Vector3 actualPos = initialPos;
        GameObject actualWall;

        for (int x = 0; x < gridSizeY; x++)
        {
            for (int y = 0; y < gridSizeX+1; y++)
            {
                actualPos = new Vector3(initialPos.x + (y * nodeDiameter) - nodeRadius, 0f, initialPos.z + (x * nodeDiameter) - nodeRadius);
                actualWall = Instantiate(wall, actualPos, Quaternion.identity);
                actualWall.transform.parent = walls.transform;
            }
        }

        actualPos = initialPos;

        for (int x = 0; x < gridSizeY+1; x++)
        {
            for (int y = 0; y < gridSizeX; y++)
            {
                actualPos = new Vector3(initialPos.x + (y * nodeDiameter), 0f, initialPos.z + (x * nodeDiameter) - nodeDiameter);
                actualWall = Instantiate(wall, actualPos, Quaternion.Euler(0f,90f,0f));
                actualWall.transform.parent = walls.transform;
            }
        }

        CreateCells();

    }

    void CreateCells()
    {
        totalNodes = gridSizeX * gridSizeY;
        int children = walls.transform.childCount;
        GameObject[] allWalls = new GameObject[children];
        nodes = new Node[gridSizeX * gridSizeY];
        int eastAux = 0;
        int indexChild = 0;
        int cont = 0;

        for (int i = 0; i < children; i++)
        {
            allWalls[i] = walls.transform.GetChild(i).gameObject;
        }
        for (int aux = 0; aux < nodes.Length; aux++)
        {
            nodes[aux] = new Node();
            nodes[aux].east = allWalls[eastAux];
            nodes[aux].south = allWalls[indexChild + (gridSizeX + 1) * gridSizeY];
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
            nodes[aux].west = allWalls[eastAux];
            nodes[aux].north = allWalls[(indexChild + (gridSizeX + 1) * gridSizeY) + gridSizeX - 1];
        }
    }

    void GetNeighbours()
    {

        int[] neighbours = new int[4];
        int indexNeighbours = 0;
        int checkNode = 0;
        checkNode = (currentNode + 1) / gridSizeX;
        checkNode -= 1;
        checkNode *= gridSizeX;
        checkNode += gridSizeX;

        //West
        if(currentNode + 1 < totalNodes && (currentNode + 1) != checkNode)
        {
            if(nodes[currentNode + 1].visited == false)
            {
                neighbours[indexNeighbours] = currentNode + 1;
                indexNeighbours++;
            }
        }
        //east
        if (currentNode + 1 >= 0 && (currentNode ) != checkNode)
        {
            if (nodes[currentNode - 1].visited == false)
            {
                neighbours[indexNeighbours] = currentNode - 1;
                indexNeighbours++;
            }
        }
        //North
        if (currentNode + gridSizeX < totalNodes)
        {
            if (nodes[currentNode +gridSizeX].visited == false)
            {
                neighbours[indexNeighbours] = currentNode + gridSizeX;
                indexNeighbours++;
            }
        }
        //South
        if (currentNode - gridSizeX >= 0)
        {
            if (nodes[currentNode - gridSizeX].visited == false)
            {
                neighbours[indexNeighbours] = currentNode - gridSizeX;
                indexNeighbours++;
            }
        }
       // for (int i = 0; i < indexNeighbours; i++)
       // {
       //     Debug.Log(neighbours[i]);
       // }
        
        if (indexNeighbours != 0)
        {
            int choosenOne = UnityEngine.Random.Range(0, indexNeighbours);
            currentNeighbour = neighbours[choosenOne];
        }
        else
        {
            if(backingUp > 0)
            {
                currentNode = lastNode[backingUp];
                backingUp--;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateWalls();
        GetNeighbours();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector3((-gridSizeX / 2) + nodeDiameter + 0.4f , 0f, (-gridSizeY / 2) + nodeDiameter), new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
        if (activeOnDraw)
        {
            //For see in the inspector
           

            if (grid != null)
            {
                foreach (Node n in grid)
                {
                    Gizmos.color = Color.white;
                    //Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 1f));
                }
            }
        }
       
    }
}
