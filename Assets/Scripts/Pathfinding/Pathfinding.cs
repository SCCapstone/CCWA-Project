using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding 
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;
    private const float CELL_SIZE = 1f;
    
    private Grid<PathNode> grid;
    private List<PathNode> openList;
    private List<PathNode> closedList;

    public Pathfinding(int width, int height)
    {
        grid = new Grid<PathNode>(width, height, CELL_SIZE, Vector3.zero, (Grid<PathNode> g, int x, int y) => new PathNode(g, x, y));

    }

    // <params>  are the XY coordinates of both the end goal and the start
    // <returns>  list of path nodes that are in order from start to finish
    private List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = grid.GetNode(startX, startY);
        PathNode endNode = grid.GetNode(endX, endY);

        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        for (int x=0; x<grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                PathNode pathNode = grid.GetNode(x, y);
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);
            if (currentNode == endNode)
            {
                //final node has been reached
                return CalculatePath(endNode);
            }
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathNode neighbourNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neighbourNode)) continue;

                if (!neighbourNode.isWalkable)
                {
                    closedList.Add(neighbourNode);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.cameFromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }

        //no nodes on the openList
        return null;
    }

    // <params> vector3 of both the start and end position
    // <returns>  list of path nodes that are in order from start to finish
    public List<Vector3> FindPath(Vector3 startWorldPosition, Vector3 endWorldPosition)
    {
        grid.GetXY(startWorldPosition, out int startX, out int startY);
        grid.GetXY(endWorldPosition, out int endX, out int endY);

        List<PathNode> path = FindPath(startX, startY, endX, endY);
        if (path == null)
        {
            return null;
        } else
        {
            List<Vector3> vectorPath = new List<Vector3>();
            foreach (PathNode pathNode in path)
            {
                vectorPath.Add(new Vector3(pathNode.x, pathNode.y) * grid.GetCellSize() * .5f);
            }
            return vectorPath;
        }
    }

    // generates a list of the adjacent nodes
    // <params> the selected node
    // <returns>  A pathnode list of the neighbors of the selected node
    private List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();

        //LEFT SIDE NODES
        if(currentNode.x -1 >= 0)
        {
            //Left Node
            neighbourList.Add(grid.GetNode(currentNode.x - 1, currentNode.y));
            if (currentNode.y - 1 >= 0)
            {
                //Diagonal left down node
                neighbourList.Add(grid.GetNode(currentNode.x - 1, currentNode.y - 1));
            }

            if (currentNode.y +1 < grid.GetHeight())
            {
                //Diagonal left up node
                neighbourList.Add(grid.GetNode(currentNode.x - 1, currentNode.y + 1));
            }
        }
        
        //RIGHT SIDE NODES
        if(currentNode.x +1 < grid.GetWidth())
        {
            //Right Node
            neighbourList.Add(grid.GetNode(currentNode.x + 1, currentNode.y));

            if (currentNode.y - 1 >= 0)
            {
                //Diagonal Right down node
                neighbourList.Add(grid.GetNode(currentNode.x + 1, currentNode.y - 1));
            }

            if (currentNode.y + 1 < grid.GetHeight())
            {
                //Diagonal Right up node
                neighbourList.Add(grid.GetNode(currentNode.x + 1, currentNode.y + 1));
            }
        }

        //Down Node
        if (currentNode.y - 1 >= 0)
        {
            neighbourList.Add(grid.GetNode(currentNode.x, currentNode.y - 1));
        }

        //Up Node
        if (currentNode.y + 1 < grid.GetHeight())
        {
            neighbourList.Add(grid.GetNode(currentNode.x , currentNode.y + 1));
        }

        return neighbourList;
    }

    // Retraces the path to get the path from order start to finish
    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;
        while (currentNode.cameFromNode != null) {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        return path;
    }

    // calculates the cost of transitioning to from node a to node b
    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.x);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    // returns the pathnode with the lowest F cost from a list of pathnodes
    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostNode = pathNodeList[0];
        for (int i= 1; i<pathNodeList.Count; i++)
        {
            if(pathNodeList[i].fCost <lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }

}
