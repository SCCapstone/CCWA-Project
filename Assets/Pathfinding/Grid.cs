using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid <T>{ 

    private int width;
    private int height;
    private float cellSize;
    private T[,] gridArray;
    private Vector3 originPosition;

    //the grid is constructed with the width and height of the grid in mind, the position of the origin is also set
    public Grid(int width, int height, float cellSize, Vector3 originPosition, Func<Grid<T>, int, int, T> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        gridArray = new T[width, height];
        this.originPosition = originPosition;

        for (int x= 0; x<gridArray.GetLength(0); x++)
        {
            for (int y=0; y<gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = createGridObject(this, x, y);
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
    


    private Vector3 GetWorldPosition (int x, int y)
    {
        return new Vector3(x, y) * cellSize+ originPosition;
    }
    


    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x/ cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }

    //Sets the value of a cell by its coordinates
    public void SetNode(int x, int y, T value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
        }
    }

    //Sets the value of a cell by its world position
    public void SetNode(Vector3 worldPosition, T value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetNode(x, y, value);
    }

    public T GetNode(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        } else
        {
            return default(T);
        }
    }

    public T GetNode(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetNode(x, y);
    }
   

}
