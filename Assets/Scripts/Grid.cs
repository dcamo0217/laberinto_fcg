using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Grid : ScriptableObject
{
    
    private int cellSize;

    private BoardManager board = new BoardManager();
    private Cell cellPrefab;
    private Cell[,] gridArray;
    private float obstacles;
    private List<Cell> gridsObtacles = new List<Cell>();
    public LayerMask obstacle;

    public LayerMask walkable;
    private int N;
    private List<Cell> path;



    public Grid(int width, int height, int cellSize, Cell cellPrefab)
    {
        this.walkable = LayerMask.NameToLayer("Default");
        this.cellSize = cellSize;
        this.cellPrefab = cellPrefab;
        N = PlayerPrefs.GetInt("DropdownValue");
        obstacles = (int)Math.Round((N*N) * 0.2);
        
        generateBoard(N,N);
    }

    private void generateBoard(int width, int height)
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
                    cell.SetColor(Color.green);
                }
                if (i == width - 1 && j == height - 1){
                    cell.setFinishGrid(true);
                    cell.SetColor(Color.green);
                }
                gridArray[i, j] = cell;
            }
        }

        var center = new Vector2((float)height / 2 - 0.5f, (float)width / 2 - 0.5f);

        Camera.main.orthographicSize = (height + width) / 3.5f;
        Camera.main.transform.position = new Vector3(center.x, center.y, -10);

        
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
                    gridArray[x,y].SetWalkable(false);
                    path = PathManager.Instance.FindPath(this, (int)this.GetGridObject(0,0).x, (int)this.GetGridObject(0,0).y, this.GetGridObject(N-1,N-1).x, this.GetGridObject(N-1,N-1).y);
                    if (path==null)
                    {
                        gridArray[x,y].SetWalkable(true);
                        i--;
                    }else{
                        gridArray[x, y].setLayerMask(obstacle);
                        gridArray[x, y].SetColor(Color.black);
                        gridsObtacles.Add(gridArray[x, y]);
                    }       
                }
            }

        }
        

        
    }
    
    public List<Cell> GetGridsObtacles()
    {
        return gridsObtacles;
    }
    
    public int GetHeight()
    {
        return N;
    }

    public int GetWidth()
    {
        return N;
    }

    public void MoveEnemy(Cell cell)
    {
        //cell.SetText("Click on cell "+cell.x+ " "+ cell.y);
        BoardManager.Instance.MoveEnemy(cell.x, cell.y);
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
