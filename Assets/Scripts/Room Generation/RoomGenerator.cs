using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomGenerator : MonoBehaviour {
    // Begin Variable statements***************************************************************************************
    public int width = 25;
    public int height = 25;

    //int smooths = 5;
    private int[,] map;

    public string seed = "";
    public bool useSeed = false;

    public Tilemap tilemap;
    public TileBase floorTile;
    public TileBase wallTile;
      
    // End Variable Statements*****************************************************************************************

    // Begin Constructors**********************************************************************************************

    //Construct a RoomGenerator capabale of generating rooms of HeightxWidth size
    public RoomGenerator(int width, int height) {
        this.width = width;
        this.height = height;
    }

    // End Constructors************************************************************************************************

    // Start getters/setters*******************************************************************************************

    void setSeed(string newSeed) {
        this.seed = newSeed;
    }

    void setUseSeed(bool useSeed) {
        this.useSeed = useSeed;
    }

    // End getters/setters*********************************************************************************************

    // Begin room generation functions*********************************************************************************

    // To calculate the neighbors of a cell, I am assuming von Neumann
    // neighborhoods (4 neighbors one in each cardinal direction)
    // I'm also assuming that all 4 args will have a max value of 1, so max
    // return of this function is 4
    int CalcNeighbors(int i, int j) {
        int walls = 0;
        for(int x = i-1; x <= i+1; x++) {
            for(int y = j-1; y <=j+1; y++) {
                if(x >= 0 && x < width && y >= 0 && y < height) {
                    if(x != i || y != j) {
                        walls += map[i,j];
                    }
                }
                else {
                    walls++;
                }
            }
        }
        return walls;
    }

    // Randomly fills the map with points
    int[,] FillRoomMap(int width, int height, string seed) {
        int[,] m = new int[height,width];
        print(width);
        print(height);
        string tempSeed;
        float fillChance = 40.0f;
        for(int i=0; i<height; i++) {
            for(int j=0; j<width; j++) {
                //Enclose the room with walls
                if(i == 0 || i == width-1 || j == 0 || j == height-1) {
                    m[i,j] = 1;
                }
                //Randomly add walls in the room based on a fill chance
                else {
                   m[i,j] = (Random.Range(0,100) < fillChance) ? 1 : 0;
                }
            }
         }
         print(m[0,0]);
         return m;
    }

    //
    void GenerateRoom(string seed) {
         for(int i=1; i<height; i++) {
            for(int j=1; j<width; j++) {
                int numNeighbors = CalcNeighbors(i,j);
                if(numNeighbors < 4) {
                    map[i,j] = 0;
                }
                else if(numNeighbors > 4) {
                    map[i,j] = 1;
                }
            }  
        }
    }

    Room getRoom() {
        return new Room(width, height, map, seed);
    }

     void RenderRoom(int[,] map, Tilemap tilemap, TileBase wallTile, TileBase floorTile) {
        tilemap.ClearAllTiles();
        for(int i=0; i<height; i++) {
            for(int j=0; j<width; j++) {
                switch(map[i,j]) {
                    case 0:
                        tilemap.SetTile(new Vector3Int(i,j,0), wallTile);
                        break;

                    case 1:
                        tilemap.SetTile(new Vector3Int(i,j,0), floorTile);
                        break;
                }
            }
        }
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

    // Start is called before the first frame update
    void Start() {
        print(height);
        print(width);
       map = FillRoomMap(width, height, seed);
       GenerateRoom(seed);
       RenderRoom(map, tilemap, wallTile, floorTile);
    }

    // Update is called once per frame
    // void Update() {
        
    // }
    // End Unity Init/Looping Functions********************************************************************************
}
