using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellManager : MonoBehaviour {
    //cellMap size
    private int cellWidth = 100;
    private int cellHeight = 100;

    //cell
    private Transform[,] cellMap;
    private bool[,] tempCellMap;
    private Transform cell;
    public Transform spawnCell;
    public Transform GameOfLife;
    public Cell getCells;

    //Time Rate
    private bool isPlaying = false;
    private bool increaseRate = false;
    private bool changeSize = false;
    private float elapsedTime = 0;
    private float updateTime;

    //Button Text
    public Text mapSize;
    public Text rateText;
    public Text playStopText;

    // Use this for initialization
    public void Start() {
        rateText.text = "Rate : 0";
        playStopText.text = "Play";
        mapSize.text = "100 x 100";
        cellMap = new Transform[cellWidth, cellHeight];
        tempCellMap = new bool[cellWidth, cellHeight];
        for (int i = 0; i < cellHeight; i++)
        {
            for (int j = 0; j < cellWidth; j++)
            {
                cell = Instantiate(spawnCell, GameOfLife);
                cell.transform.position = new Vector2(j * cell.localScale.x, i * cell.localScale.z) - new Vector2(cellWidth * cell.localScale.x / 2, cellHeight * cell.localScale.z / 2);
                cellMap[i, j] = cell;               
            }
        }   
	}
	
	// Update is called once per frame
	void Update () {
        checkNeighbors();    
	}

    public void changeMap()
    {
        changeSize = !changeSize;
        if (changeSize)
        {
            cellWidth = 50;
            cellHeight = 50;
            mapSize.text = "50 x 50";
            cellMap = new Transform[cellWidth, cellHeight];

            for (int i = 0; i < GameOfLife.childCount; i++)
            {
                Destroy(GameOfLife.GetChild(i).gameObject);
            }

            for (int i = 0; i < cellHeight; i++)
            {
                for (int j = 0; j < cellWidth; j++)
                {                       
                    cell = Instantiate(spawnCell, GameOfLife);
                    cell.transform.position = new Vector2(j * cell.localScale.x, i * cell.localScale.z) - new Vector2(cellWidth * cell.localScale.x / 2, cellHeight * cell.localScale.z / 2);
                    cellMap[i, j] = cell;
                }
            }         
        }
        else
        {
            cellWidth = 100;
            cellHeight = 100;
            mapSize.text = "100 x 100";
            cellMap = new Transform[cellWidth, cellHeight];
            for (int i = 0; i < GameOfLife.childCount; i++)
            {
                Destroy(GameOfLife.GetChild(i).gameObject);
            }

            for (int i = 0; i < cellHeight; i++)
            {
                for (int j = 0; j < cellWidth; j++)
                {
                    cell = Instantiate(spawnCell, GameOfLife);
                    cell.transform.position = new Vector2(j * cell.localScale.x, i * cell.localScale.z) - new Vector2(cellWidth * cell.localScale.x / 2, cellHeight * cell.localScale.z / 2);
                    cellMap[i, j] = cell;
                }
            }           
        }          
    }

    void checkNeighbors()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= updateTime)
        {
            elapsedTime = 0;
        }
        else
        {
            return;
        }

        if (isPlaying)
        {
            //Debug.Log("Running");
            for (int i = 0; i < cellHeight; i++)
            {
                for (int j = 0; j < cellWidth; j++)
                {
                    getCells = cellMap[i, j].GetComponent<Cell>();
                    int neighbors = getNeigbour(i, j);
                    bool results = false;
                   
                    if (getCells.isAlive)
                    {
                        if (neighbors < 2)
                        {
                            results = false;
                        }
                        else if (neighbors == 2 || neighbors == 3)
                        {
                            results = true;
                        }
                        else if (neighbors > 3)
                        {
                            results = false;
                        }
                    }
                    else if (!getCells.isAlive)
                    {
                        if (neighbors == 3)
                        {
                            results = true;
                        }
                    }
                    tempCellMap[i, j] = results;
                }                
            }

            for (int i = 0; i < cellHeight; i++)
            {
                for (int j = 0; j < cellWidth; j++)
                {
                    getCells = cellMap[i, j].GetComponent<Cell>();
                    getCells.isAlive = tempCellMap[i, j];
                }
            }
        }
    }

    int getNeigbour(int x, int y)
    {
        int neighbor = 0;
        //right
        if(x + 1 < cellWidth)
        {
            if(cellMap[x+1,y].GetComponent<Cell>().isAlive)
            {
                neighbor++;
            }
        }
        //left
        if(x - 1 >= 0)
        {
            if (cellMap[x - 1, y].GetComponent<Cell>().isAlive)
            {
                neighbor++;
            }
        }
        //top
        if(y + 1 < cellHeight)
        {
            if (cellMap[x, y+ 1].GetComponent<Cell>().isAlive)
            {
                neighbor++;
            }
        }
        //bottom
        if (y - 1 >= 0)
        {
            if (cellMap[x, y - 1].GetComponent<Cell>().isAlive)
            {
                neighbor++;
            }
        }
        //bottomleft
        if (x - 1  >= 0 && y - 1 >=0)
        {
            if (cellMap[x - 1, y - 1].GetComponent<Cell>().isAlive)
            {
                neighbor++;
            }
        }
        //bottomright
        if (x + 1  <cellWidth && y - 1 >= 0)
        {
            if (cellMap[x + 1, y - 1].GetComponent<Cell>().isAlive)
            {
                neighbor++;
            }
        }
        //topleft
        if (x + 1 < cellWidth && y + 1 <cellHeight)
        {
            if (cellMap[x + 1, y + 1].GetComponent<Cell>().isAlive)
            {
                neighbor++;
            }
        }

        //topright
        if (x - 1 >= 0 && y + 1 < cellHeight)
        {
            if (cellMap[x - 1, y + 1].GetComponent<Cell>().isAlive)
            {
                neighbor++;
            }
        }
        return neighbor;
    }

    public void Rate()
    {
        increaseRate = !increaseRate;
        if(increaseRate)
        {
            updateTime = 0.5f;
            rateText.text = "Rate : 0.5";
        }
        else
        {
            updateTime = 0.0f;
            rateText.text = "Rate : 0";
        }
    }

    public void StartStop()
    {       
       isPlaying = !isPlaying;    
        if(isPlaying)
        {
            playStopText.text = "Stop";
        }
        else
        {
            playStopText.text = "Play";
        }
    }

    public void Clear()
    {
        for(int i = 0; i <cellHeight; i ++)
        {
            for(int j = 0; j<cellWidth; j++)
            {
                getCells = cellMap[i, j].GetComponent<Cell>();
                getCells.isAlive = false;
            }
        }
    }
}
