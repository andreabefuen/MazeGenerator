using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreationMazeSizeUI : MonoBehaviour
{

    public Text rowText;
    public Text columnText;

    public GameObject firstPanel;
    public GameObject secondPanel;

    public GameObject player;


    MazeGenerator mazeGenerator;

    int rows, columns;

    private void Awake()
    {

        mazeGenerator = this.GetComponent<MazeGenerator>();
        rows =(int) mazeGenerator.gridWorldSize.x;
        columns = (int)mazeGenerator.gridWorldSize.y;

        UpdateText();


    }

    void UpdateText()
    {
        rowText.text = "Size in X: " + rows;
        columnText.text = "Size in Y: " + columns;
    }

    public void PlusRowButton()
    {
        rows++;
        mazeGenerator.gridWorldSize.x = rows;
        UpdateText();
        RegerateMaze();


    }
    public void MinusRowButton()
    {
        rows--;
        mazeGenerator.gridWorldSize.x = rows;
        UpdateText();
        RegerateMaze();
    }
    public void PlusColumnButton()
    {
        columns++;
        mazeGenerator.gridWorldSize.y = columns;
        UpdateText();
        RegerateMaze();

    }

    public void MinusColumnButton()
    {
        columns--;
        mazeGenerator.gridWorldSize.y = columns;
        UpdateText();
        RegerateMaze();

    }

    public void RegerateMaze()
    {
        mazeGenerator.RecreateMazeWithDifferentSize();
    }

    public void GoToMaze()
    {
        firstPanel.SetActive(false);
        secondPanel.SetActive(true);
        this.GetComponent<MazeAlgorithms>().enabled = true;
        this.GetComponent<RecursiveBacktracker>().enabled = true;
    }

    public void GoToMyWeb()
    {
        Application.OpenURL("https://andreabenitodev.blogspot.com/");
    }

    public void SpawnPlayer()
    {
        Vector3 pos = mazeGenerator.cells[0].wallSouth.transform.position;
        pos.z += 1f;
        Instantiate(player, pos, Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
