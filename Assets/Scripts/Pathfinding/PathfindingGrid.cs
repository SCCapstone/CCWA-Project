using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingGrid { 

    private int width;
    private int height;
    private float cellSize;
    private PathNode[,] PathfindingGridArray;
    private Vector2 originPosition;


    private int[,] map;

    //the PathfindingGrid is constructed with the width and height of the PathfindingGrid in mind, the position of the origin is also set
    public PathfindingGrid(int width, int height, float cellSize, Vector2 originPosition)
    {
        GameObject fm = GameObject.Find("Grid/Floor Map");
        RoomRenderer rr = fm.GetComponent<RoomRenderer>();
        Room cr = rr.currentRoom;
        // while(cr is null)
        // {
        //     cr = rr.currentRoom;
        //     Debug.Log("say something im giving up on you");
        // }
        // Debug.Log(cr is null);
        map = cr.getMap();
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        PathfindingGridArray = new PathNode[width, height];
        this.originPosition = originPosition;
        //LayerMask Unwalkable = LayerMask.GetMask("Unwalkable");

        for (int x= 0; x<PathfindingGridArray.GetLength(0); x++)
        {
            for (int y=0; y<PathfindingGridArray.GetLength(1); y++)
            {
                PathfindingGridArray[x, y] = new PathNode(this, x, y);
                if (map[x, y] == 1)
                {
                        PathfindingGridArray[x, y].setWalkable(false);
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
    private Vector2 GetWorldPosition (int x, int y)
    {
        return new Vector2(x, y) * cellSize+ originPosition;
    }
    

    //gets the x y coordinantes from a vector 3
    public void GetXY(Vector2 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x/ cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }

    //Sets the value of a cell by its coordinates
    public void SetNode(int x, int y, PathNode value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            PathfindingGridArray[x, y] = value;
        }
    }

    //Sets the value of a cell by its world position
    public void SetNode(Vector2 worldPosition, PathNode value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetNode(x, y, value);
    }

    public PathNode GetNode(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return PathfindingGridArray[x, y];
        } else
        {
            return default(PathNode);
        }
    }

    public PathNode GetNode(Vector2 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetNode(x, y);
    }
   

}
