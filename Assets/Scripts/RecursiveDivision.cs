using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursiveDivision : MonoBehaviour
{

    public Vector2 gridWorldSize;
    public GameObject wall;
    public GameObject floorCell;
    public float nodeRadius;


    int gridSizeX;
    int gridSizeY;
    float nodeDiameter;
    int totalNodes;
    int totalWalls;

    GameObject walls;
    Vector3 initialPos;

    GameObject wallHorizontal;
    GameObject wallVertical;


    void Awake()
    {
        nodeDiameter = nodeRadius * 2;

        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        wall.gameObject.transform.localScale = new Vector3(wall.transform.localScale.x, wall.transform.localScale.y, nodeDiameter);

        walls = new GameObject();
        walls.name = "Maze";

        wallHorizontal = wall;
        wallHorizontal.transform.rotation = Quaternion.identity;
        wallVertical = wall;
        wallVertical.transform.rotation = Quaternion.Euler(0f, 90f, 0f);

    }
    private void Start()
    {

        Boundaries();
        RecursiveDivisionAlgorithm(gridSizeX, gridSizeY);
       //CreateDivisionHorizontal(gridSizeX, gridSizeY);
       //CreateDivisionVertical(gridSizeX, gridSizeY);
    }

    void Boundaries()
    {
        int bottomBoundary = gridSizeX;
        int topBoundary = gridSizeY;



        initialPos = new Vector3((-gridSizeX / 2) + nodeRadius, 0f, (-gridSizeY / 2) + nodeRadius);
        Vector3 actualPos = initialPos;
        GameObject actualWall;

        //Right walls
        for (int i = 0; i < gridSizeY; i++)
        {
            actualPos = new Vector3(initialPos.x + (0 * nodeDiameter) - nodeRadius, 0f, initialPos.z + (i * nodeDiameter) - nodeRadius);
            actualWall = Instantiate(wall, actualPos, Quaternion.identity);
            actualWall.transform.parent = walls.transform;
          
        }
        //Left walls
        for (int i = 0; i < gridSizeY; i++)
        {
            actualPos = new Vector3(initialPos.x + (bottomBoundary * nodeDiameter) - nodeRadius, 0f, initialPos.z + (i * nodeDiameter) - nodeRadius);
            actualWall = Instantiate(wall, actualPos, Quaternion.identity);
            actualWall.transform.parent = walls.transform;

        }

        //Bottom walls
        for (int i = 0; i < gridSizeY; i++)
        {
            actualPos = new Vector3(initialPos.x + (i * nodeDiameter), 0f, initialPos.z + (0 * nodeDiameter) - nodeDiameter);
            actualWall = Instantiate(wall, actualPos, Quaternion.Euler(0f, 90f, 0f));
            actualWall.transform.parent = walls.transform;

        }
        //Top walls
        for (int i = 0; i < gridSizeY; i++)
        {
            actualPos = new Vector3(initialPos.x + (i * nodeDiameter), 0f, initialPos.z + (topBoundary * nodeDiameter) - nodeDiameter);
            actualWall = Instantiate(wall, actualPos, Quaternion.Euler(0f, 90f, 0f));
            actualWall.transform.parent = walls.transform;

        }

    }



    void RecursiveDivisionAlgorithm(int w, int h)
    {

        if (w <= 1 && h <= 1)
        {
            Debug.Log("FINISHED");
            return;
        }

        else if (w >= h || h ==1)
        {
            CreateDivisionVertical(w, h);


        }
        else if(w==1 || w < h)
        {
            CreateDivisionHorizontal(w, h);

        }
        else
        {
            return;
        }
    }


    void CreateDivisionHorizontal(int width, int height)
    {


        initialPos = new Vector3((-width / 2) + nodeRadius, 0f, (-height / 2) + nodeRadius);
        Vector3 actualPos = initialPos;
        GameObject actualWall;


        List<GameObject> division = new List<GameObject>();

        int rdn = Random.Range(0, height);
        for (int i = 0; i < height; i++)
        {
            actualPos = new Vector3(initialPos.x + (rdn * nodeDiameter) - nodeRadius, 0f, initialPos.z + (i * nodeDiameter) - nodeRadius);
            actualWall = Instantiate(wall, actualPos, Quaternion.identity);

            actualWall.transform.parent = walls.transform;
            division.Add(actualWall);
        }
        if(division.Count>= 1)
        {
            int index = Random.Range(0, rdn);
            Destroy(division[index]);
        }

        // if (division.Count >= 1)
        // {
        //     int index = Random.Range(0, division.Count - 1);
        //     Destroy(division[index]);
        // }

        // RecursiveDivisionAlgorithm(width, rdn);
        // RecursiveDivisionAlgorithm(width, height - rdn);
        // CreateDivisionVertical(width, rdn);
        // CreateDivisionVertical(width, height - rdn-1);

        //return (CreateDivisionVertical(width, rdn) && CreateDivisionVertical(width , height - rdn - 1));

        RecursiveDivisionAlgorithm(width, height - rdn -1);
        RecursiveDivisionAlgorithm(width, rdn);


    }

    void CreateDivisionVertical(int width, int height)
    {



        initialPos = new Vector3((-width / 2) + nodeRadius, 0f, (-height / 2) + nodeRadius);
        Vector3 actualPos = initialPos;
        GameObject actualWall;

        List<GameObject> division = new List<GameObject>();

        bool removeWall = false;


        int rdn = Random.Range(0, width);

        for (int i = 0; i < height; i++)
        {
            actualPos = new Vector3(initialPos.x + (i * nodeDiameter), 0f, initialPos.z + (rdn * nodeDiameter) - nodeDiameter);
            actualWall = Instantiate(wall, actualPos, Quaternion.Euler(0f, 90f, 0f));

            actualWall.transform.parent = walls.transform;
            division.Add(actualWall);


        }

        if (division.Count >= 1)
        {
            int index = Random.Range(0, rdn);
            Destroy(division[index]);
        }
        // RecursiveDivisionAlgorithm(rdn, height);
        // RecursiveDivisionAlgorithm(width - rdn, height);
        //return (CreateDivisionHorizontal(rdn, height) && CreateDivisionHorizontal(width - rdn - 1, height)) ;
        //CreateDivisionHorizontal(width-rdn-1, height);
        //CreateDivisionHorizontal(rdn, height);
        RecursiveDivisionAlgorithm(width - rdn-1, height);
        RecursiveDivisionAlgorithm( rdn, height);

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
