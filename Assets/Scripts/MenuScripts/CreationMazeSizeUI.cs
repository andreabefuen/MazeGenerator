﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Class for the UI elements and gameloop

public class CreationMazeSizeUI : MonoBehaviour
{

    public Text rowText;
    public Text columnText;

    public GameObject firstPanel;
    public GameObject secondPanel;

    public GameObject player;
    public GameObject goal;

    public GameObject winCanvas;

    public Button playButton;

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
        if(rows == 5)
        {
            return;
        }
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
        if(columns == 5)
        {
            return;
        }
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
        playButton.interactable = false;
        
    }

    public void InteractablePlayButton()
    {
        playButton.interactable = true;

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

        Vector3 posGoal = mazeGenerator.cells[mazeGenerator.cells.Length - 1].wallNorth.transform.position;
        posGoal.z -= 1f;
        Instantiate(goal, posGoal, Quaternion.identity);

        secondPanel.SetActive(false);
    }

    public void WinCanvas()
    {
        winCanvas.SetActive(true);
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
