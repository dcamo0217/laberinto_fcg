using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Grid : ScriptableObject
{
    
    private int cellSize;
    private Cell cellPrefab;
    private Cell[,] gridArray;
    private int obstacles;
    private List<Cell> gridsObtacles = new List<Cell>();
    public LayerMask obstacle;
    private int width;
    private int height;
    private int N;



    public Grid(int width, int height, int cellSize, Cell cellPrefab)
    {
        
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.cellPrefab = cellPrefab;
        N = PlayerPrefs.GetInt("nDropdownValue");
        obstacles = PlayerPrefs.GetInt("mDropdownValue");
        generateBoard();
    }

    private void generateBoard()
    {
        Cell cell;
        gridArray = new Cell[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var p = new Vector2(i, j) * cellSize;
                cell = Instantiate(cellPrefab, p, Quaternion.identity);
                cell.Init(this, (int)p.x, (int)p.y, true);
                cell.SetColor(Color.red);
                if (i == 0 && j == 0){
                    cell.setStartGrid(true);
                }
                if (i == width - 1 && j == height - 1){
                    cell.setFinishGrid(true);
                }
                gridArray[i, j] = cell;
            }
        }

        var center = new Vector2((float)height / 2 - 0.5f, (float)width / 2 - 0.5f);

        Camera.main.transform.position = new Vector3(center.x, center.y, -5);

        for (int i = 0; i < obstacles; i++){
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);
            //Debug.Log(x);
            //Debug.Log(y);
            if (gridsObtacles.Contains(gridArray[x, y])){
                i--;
            }else{
                if (gridArray[x, y].getStartGrid() || gridArray[x, y].getFinishGrid()){
                
                }else{
                gridArray[x, y].setLayerMask(obstacle);
                gridArray[x,y].SetWalkable(false);
                gridArray[x, y].SetColor(Color.black);
                gridsObtacles.Add(gridArray[x, y]);
                }
            }
        }
    }

    public int GetHeight()
    {
        return N;
    }

    public int GetWidth()
    {
        return N;
    }

    public void CellMouseClick(Cell cell)
    {
        //cell.SetText("Click on cell "+cell.x+ " "+ cell.y);
        BoardManager.Instance.CellMouseClick(cell.x, cell.y);
    }

    

    public Cell GetGridObject(int x, int y)
    {
        return gridArray[x, y];
    }

    internal float GetCellSize()
    {
        return cellSize;
    }
}
