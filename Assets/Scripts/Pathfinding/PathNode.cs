using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode 
{
    private Grid<PathNode> grid;
    public int x;
    public int y;

    public int gCost;
    public int hCost;
    public int fCost;
    
    public bool isWalkable;
    //keeps track of the node previously that was in transition
    public PathNode cameFromNode;

   
    public PathNode(Grid<PathNode> grid, int x, int y)
    { 
        this.grid = grid;
        this.x = x;
        this.y = y;
        isWalkable = true;
    }

    /*
    public PathNode(Grid<PathNode> grid, int x, int y,bool isWalkable)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        this.isWalkable = isWalkable;
    }
    */


    // calculates the FCost which is the g Cost and h Cost summed togther
    // g Cost is the distance to the start node
    // h Cost is the distance to the end node

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    public override string ToString()
    {
        return x+","+y;
    }
}
