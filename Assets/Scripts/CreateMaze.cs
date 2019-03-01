using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMaze : MonoBehaviour
{

    public bool activeOnDraw;
    public GameObject wall;

    public Vector2 gridWorldSize;
    public float nodeRadius;

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
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                // bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                bool walkable = true;
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
        CreateWalls();
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

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        if (activeOnDraw)
        {
            //For see in the inspector
            Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

            if (grid != null)
            {
                foreach (Node n in grid)
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 1f));
                }
            }
        }
       
    }
}
