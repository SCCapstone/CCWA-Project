using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid { 

    private int width;
    private int height;
    private float cellSize;
    private PathNode[,] gridArray;
    private Vector3 originPosition;


    private int[,] map = GameObject.Find("Grid/Tilemap").GetComponent<FloorGenerator>().GetCurrRoom().getMap();

    //the grid is constructed with the width and height of the grid in mind, the position of the origin is also set
    public Grid(int width, int height, float cellSize, Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        gridArray = new PathNode[width, height];
        this.originPosition = originPosition;
        //LayerMask Unwalkable = LayerMask.GetMask("Unwalkable");

        for (int x= 0; x<gridArray.GetLength(0); x++)
        {
            for (int y=0; y<gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = new PathNode(this, x, y);
                if (map[x, y] == 1)
                {
                        gridArray[x, y].setWalkable(false);
                }
            }
        }
    }
    

    // GETTERS
    public int GetHeight()
    {
        return height;
    }

    public int GetWidth()
    {
        return width;
    }

    public float GetCellSize()
    {
        return cellSize;
    }
    

    // gets the world postion of from the x y as a vector 3
    private Vector3 GetWorldPosition (int x, int y)
    {
        return new Vector3(x, y) * cellSize+ originPosition;
    }
    

    //gets the x y coordinantes from a vector 3
    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x/ cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }

    //Sets the value of a cell by its coordinates
    public void SetNode(int x, int y, PathNode value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
        }
    }

    //Sets the value of a cell by its world position
    public void SetNode(Vector3 worldPosition, PathNode value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetNode(x, y, value);
    }

    public PathNode GetNode(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        } else
        {
            return default(PathNode);
        }
    }

    public PathNode GetNode(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetNode(x, y);
    }
   

}
