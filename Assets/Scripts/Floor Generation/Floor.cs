using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public string seed;
    public int numRooms;
    public int[,] floorLayout;
    public Room[] rooms;

    //Default Constructor
    public Floor()
    {
        this.seed = "none";
        this.numRooms = 0;
        this.floorLayout = new int[numRooms,numRooms];
        this.rooms = new Room[numRooms];
    }

    //Full Constructor
    public Floor(int numRooms, int[,] floorLayout, string seed, Room[] rooms) //TODO add input validation
    {
        this.seed = seed;
        this.numRooms = numRooms;
        this.floorLayout = new int[numRooms,numRooms];
        this.rooms = rooms;
    }
}