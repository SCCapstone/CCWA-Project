using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomGenerator : MonoBehaviour {
    // Begin Variable statements***************************************************************************************
    int width;
    int height;
    int numRooms;

    string seed;
    bool useSeed = false;
   
    // End Variable Statements*****************************************************************************************

    // Begin Constructors**********************************************************************************************

    public RoomGenerator(int width, int height, int numRooms) {
        this.width = width;
        this.height = height;
        this.numRooms = numRooms;
    }

    // End Constructors************************************************************************************************

    // Start getters/setters*******************************************************************************************
    void setSeed(string seed) {
        this.seed = seed;
    }

    void setUseSeed(bool useSeed) {
        this.useSeed = useSeed;
    }

    // End getters/setters*********************************************************************************************

    // Begin room generation functions*********************************************************************************

    // Initializes a 2x2 int "map", represented as an array of binaries.
    // 0 represents a wall, 1 represents floor 
    // Don't need to store a 2x2 array at all times, 
    // so this inits map for when it is needed to generate and render the room
    int[,] InitMap() {
        return new int[width,height];
    }

    // To calculate the neighbors of a cell, I am assuming von Neumann
    // neighborhoods (4 neighbors one in each cardinal direction)
    // I'm also assuming that all 4 args will have a max value of 1, so max
    // return of this function is 4
    int CalcNeighbors(int i1,int i2,int i3,int i4) {
        return i1+i2+i3+i4;
    }

    int[,] FillRoomMap(string seed) {
        if(!useSeed || seed == null || seed == "") {
            setSeed(Time.time.ToString());
        }
        else {
            setSeed(seed);
        }
        int[,] map = InitMap();
        float fillChance = 0.2f;
        System.Random random= new System.Random(seed.GetHashCode());

        for(int i=0; i<height; i++) {
            for(int j=0; j<width; j++) {
                //Enclose the room with walls
                if(i == 0 || i == width || j == 0 || j == height) {
                    map[i,j] = 1;
                }
                //Randomly add walls in the room based on a fill chance
                else {
                    int val = random.Next(0,100);
                    if(val <= fillChance) {
                        map[i,j] = 1;
                    }
                    else if(val > fillChance) {
                        map[i,j] = 0;
                    }
                }
            }
         }
         return map;
    }

    Room GenerateRoom() {
        int[,] map = FillRoomMap(seed);
         for(int i=0; i<height; i++) {
            for(int j=0; j<width; j++) {
                int numNeighbors = CalcNeighbors(map[i-1, j], map[i+1, j], map[i, j-1], map[i, j+1]);
                if(numNeighbors <=1) {
                    map[i,j] = 0;
                }
                else if(numNeighbors == 2 || numNeighbors == 3) {
                    map[i,j] = 1;
                }
                else {
                    map[i,j] = 0;
                }
            }  
        }
        return new Room(width, height, map, seed);
    }

    Room[] GenerateRoomsForFloor() {
        Room[] rooms = new Room[numRooms];
        for(int i=0; i<numRooms; i++) {
            rooms[i] = GenerateRoom();
        }
        return rooms;
    }

    // End Room Generation Functions***********************************************************************************

    // Begin Spawn Location Generation Functions***********************************************************************

    // Empty for now. Will be used to generate random spawning points on 
    // the room floor
    // NOTE: My thought was to use a second tilemap over the floor map
    // to hold the starting points of enemies
    void GenerateSpawnLocations() {
        GenerateEnemySpawns();
        GenerateLootSpawns();
        GenerateExitSpawn();
    }

    void GenerateEnemySpawns() {

    }

    void GenerateLootSpawns() {

    }

    void GenerateExitSpawn() {

    }

    // End Spawn Location Generation Functions*************************************************************************

    // Begin Unity Init/Looping Functions******************************************************************************
    
    // I don't believe we need these functions here currently,
    // but leaving them commented just in case

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    // End Unity Init/Looping Functions********************************************************************************
}
