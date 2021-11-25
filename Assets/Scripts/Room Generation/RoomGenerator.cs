using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomGenerator : MonoBehaviour {
    // Begin Variable statements***************************************************************************************
    public int width = 25;
    public int height = 25;

    public int iterations = 5;
    private int[,] map;

    public string seed = "";
    public bool useSeed = false;

    public Location exitLocation;

    public Tilemap tilemap;
    public TileBase floorTile;
    public TileBase wallTile;

    public TileBase exitTile;

    private static string[] exitLocations = new string[4];

    public List<Room> rooms = new List<Room>();
      
    // End Variable Statements*****************************************************************************************

    // Begin Constructors**********************************************************************************************

    //Construct a RoomGenerator capabale of generating rooms of HeightxWidth size
    public RoomGenerator(int width, int height) {
        this.width = width;
        this.height = height;
    }

    // End Constructors************************************************************************************************

    // Start getters/setters*******************************************************************************************

    Room getRoom() {
        return new Room(width, height, map, seed, exitLocation);
    }

    public void setSeed(string newSeed) {
        this.seed = newSeed;
    }

    public void setUseSeed(bool useSeed) {
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
    int[,] FillRoomMap(int width, int height) {
        if(!useSeed) {
            seed = Time.time.ToString();
        }
        System.Random rand = new System.Random(seed.GetHashCode());
        int[,] m = new int[height,width];
        string tempSeed;
        float fillChance = 20.0f;
        for(int i=0; i<height; i++) {
            for(int j=0; j<width; j++) {
                //Enclose the room with walls
                if(i == 0 || i == width-1 || j == 0 || j == height-1) {
                    m[i,j] = 1;
                }
                //Randomly add walls in the room based on a fill chance
                else {
                   m[i,j] = (rand.Next(0,100) < fillChance) ? 1 : 0;
                }
            }
         }
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

     void RenderRoom(int[,] map, Tilemap tilemap, TileBase wallTile, TileBase floorTile) {
        tilemap.ClearAllTiles();
        for(int i=0; i<height; i++) {
            for(int j=0; j<width; j++) {
                switch(map[i,j]) {
                    case 0:
                        tilemap.SetTile(new Vector3Int(i,j,0), floorTile);
                        break;

                    case 1:
                        tilemap.SetTile(new Vector3Int(i,j,0), wallTile);
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
    void GenerateSpawnLocations(int[,] map, Tilemap tilemap, TileBase exitTile) {
        GenerateEnemySpawns();
        GenerateLootSpawns();
        GenerateExitSpawn(map, tilemap, exitTile);
    }

    // Need to instantiate enemy objects at (x,y) coordinates here
    void GenerateEnemySpawns() {

    }

    // These could be represented as a special tile,
    // but can also be a game object instantiated at (x,y) coordinates
    void GenerateLootSpawns() {

    }

    // This will be a special tile
    // When the player passes over it, it will take them to the next room
    Location GenerateExitSpawn(int[,] map, Tilemap tilemap, TileBase exitTile) {
       exitLocations[0] = "top";
       exitLocations[1] = "bottom";
       exitLocations[2] = "left";
       exitLocations[3] = "right";
       int choice = Random.Range(0,3);
       string location = exitLocations[choice];
       print(location);
       switch(location) {
           case "top":
                for(int i = height- 3; i>height-7; i--) {
                    for(int j=10; j<width-10; j++) {
                        if(map[i,j] == 0) {
                            if(Random.Range(1,10) < 6) {
                                tilemap.SetTile(new Vector3Int(i,j,0), exitTile);
                                return new Location(location, i, j);
                            }
                        }
                    }
                }
                break;
           case "bottom":
                for(int i = 3; i<7; i++) {
                    for(int j=10; j<width-10; j++) {
                        if(map[i,j] == 0) {
                             if(Random.Range(1,10) < 2) {
                                tilemap.SetTile(new Vector3Int(i,j,0), exitTile);
                                return new Location(location, i, j);
                            }
                        }
                    }
                }
                break;
           case "left":
                for(int i = 10; i<height-10; i++) {
                    for(int j=3; j<7; j++) {
                        if(map[i,j] == 0) {
                             if(Random.Range(1,10) < 2) {
                                tilemap.SetTile(new Vector3Int(i,j,0), exitTile);
                                return new Location(location, i, j);
                            }
                        }
                    }
                }
                break;
           case "right":
                for(int i = 10; i<height-10; i++) {
                    for(int j=width-3; j>width-7; j--) {
                        if(map[i,j] == 0) {
                             if(Random.Range(1,10) < 2) {
                                tilemap.SetTile(new Vector3Int(i,j,0), exitTile);
                                return new Location(location, i, j);
                            }
                        }
                    }
                }
                break;
       }
        return new Location();
    }

    // End Spawn Location Generation Functions*************************************************************************

    // Begin Unity Init/Looping Functions******************************************************************************

    // Start is called before the first frame update
    void Start() {
       map = FillRoomMap(width, height);
       for(int i=0; i<iterations; i++) {
           GenerateRoom(seed);
       }
       RenderRoom(map, tilemap, wallTile, floorTile);
       exitLocation = GenerateExitSpawn(map, tilemap, exitTile);
    }

    // Update is called once per frame
    // void Update() {
        
    // }
    // End Unity Init/Looping Functions********************************************************************************
}
