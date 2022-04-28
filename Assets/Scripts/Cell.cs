using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMeshPro;
    [SerializeField] private GameObject Inner;
    private Grid grid;
    public bool isWalkable;
    public int x, y ;
    public int gCost, hCost, fCost;
    public Cell pastCell;
    private bool startGrid = false;
    private bool finishGrid = false;
    public LayerMask obstacle;

    public void Init(Grid grid, int x, int y, bool isWalkable)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        this.isWalkable = isWalkable;
    }
    public void setLayerMask(LayerMask layerMask)
    {
        this.obstacle = layerMask;
        gameObject.layer = (int)layerMask+3;
        Debug.Log(gameObject.layer);
    }
    public Vector2 Position => transform.position;

    public void SetText(string text)
    {
        textMeshPro.text = text;
    }

    public void SetColor(Color color)
    {
        Inner.GetComponent<SpriteRenderer>().color = color;
    }

    private void OnMouseDown()
    {
        
        if (Input.GetMouseButton(0))
        {
            grid.CellMouseClick(this);
        } 
    }

    internal void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    internal void SetWalkable(bool v)
    {
        isWalkable = v;
        SetColor(Color.black);
    }

    public override string ToString()
    {
        return "Cell "+x + "," + y;
    }

    public void setStartGrid(bool value)
    {
        startGrid = value;
        Debug.Log("startGrid: " + startGrid);
    }

    public void setFinishGrid(bool value)
    {
        finishGrid = value;
        Debug.Log("finishGrid: " + finishGrid);
    }

    public bool getStartGrid()
    {
        return startGrid;
    }

    public bool getFinishGrid()
    {
        return finishGrid;
    }
}
