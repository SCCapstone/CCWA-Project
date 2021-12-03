using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor
{
    public string seed;
    public int[,] floorLayout;
    public Room[] rooms;

    //Default Constructor
    public Floor()
    {
        this.seed = "none";
        this.floorLayout = new int[1,1];
        this.rooms = new Room[1];
    }

    //Full Constructor
    public Floor(string seed, int[,] floorLayout, Room[] rooms) //TODO add input validation
    {
        this.seed = seed;
        this.floorLayout = floorLayout;
        this.rooms = rooms;
    }
}