using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class RoomGenerator : MonoBehaviour {
    // Begin Variable statements***************************************************************************************
    public int width = 25;
    public int height = 25;

    public int iterations = 5;
    private int[,] map;

    public string seed = "";
    public bool useSeed = false;

    public Location exitLocation;

    public int numEnemies;
    public int numItems;

    public Tilemap tilemap;
    public TileBase floorTile;
    public TileBase wallTile;

    public TileBase exitTile;

    private static string[] exitLocationChoices = new string[4];
    public Location[] exitLocations; 

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
        string tempSeed;
        if(!useSeed) {
            tempSeed = Time.time.ToString();
        }
        else {
            tempSeed = seed;
        }
        System.Random rand = new System.Random(tempSeed.GetHashCode());
        int[,] m = new int[height,width];
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

    // Begin Room saving/loading Functions*****************************************************************************

    // Adds a room to the list of rooms held by this room generator
    List<Room> addRoomToRoomList(Room saveRoom) {
        rooms.Add(saveRoom);
        return rooms;
    }

    // Loads a room to the scene
    // This is accomplished by setting the seed
    // in this generator, then reloading the scene
    void loadRoomFromSeed(string searchSeed) {
        for(int i=0; i<rooms.Count; i++) {
            if(rooms[i].seed == searchSeed) {
                this.setSeed(searchSeed);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                return;
            }
        }
    }

    // End Room saving/loading Functions*******************************************************************************

    // Begin Spawn Location Generation Functions***********************************************************************

    // Empty for now. Will be used to generate random spawning points on 
    // the room floor
    // NOTE: My thought was to use a second tilemap over the floor map
    // to hold the starting points of enemies
    void GenerateSpawnLocations(int[,] map, Tilemap tilemap, TileBase exitTile, int numEnemies, int numItems) {
        GenerateEnemySpawns(numEnemies);
        GenerateLootSpawns(numItems);
        //GenerateExitSpawns(map, tilemap, exitTile);
    }

    // Need to instantiate enemy objects at (x,y) coordinates here
    void GenerateEnemySpawns(int numEnemies) {
        for(int i = height-3; i>height-7; i--) {
            for(int j=10; j<width-10; j++) {
                if(map[i,j] == 0 && numEnemies > 0) {
                    // Instantiate enemy here
                    numEnemies--;
                }
                if(numEnemies == 0) {
                    return;
                }
            }
        }

    }

    // These could be represented as a special tile,
    // but can also be a game object instantiated at (x,y) coordinates
    void GenerateLootSpawns(int numItems) {
        for(int i = height-3; i>height-7; i--) {
            for(int j=10; j<width-10; j++) {
                if(map[i,j] == 0 && numItems > 0) {
                    // Instantiate Items here
                    numItems--;
                }
                if(numItems == 0) {
                    return;
                }
            }
        }

    }

    // This will be a special tile
    // When the player passes over it, it will take them to the next room
    Location GenerateExitSpawn(int[,] map, Tilemap tilemap, TileBase exitTile) {
       exitLocationChoices[0] = "top";
       exitLocationChoices[1] = "bottom";
       exitLocationChoices[2] = "left";
       exitLocationChoices[3] = "right";

        string location = exitLocationChoices[Random.Range(0,3)];
        switch(location) {
            case "top":
                    for(int i = height- 3; i>height-7; i--) {
                        for(int j=10; j<width-10; j++) {
                            if(map[i,j] == 0) {
                                if(Random.Range(1,10) < 9) {
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
                                if(Random.Range(1,10) < 9) {
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
                                if(Random.Range(1,10) < 9) {
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
                                if(Random.Range(1,10) < 9) {
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
        // Need input for a seed 
        string seed = "10010010101";
       map = FillRoomMap(width, height, seed);
       for(int i=0; i<iterations; i++) {
           GenerateRoom(seed);
       }
       RenderRoom(map, tilemap, wallTile, floorTile);
       int numLocations = Random.Range(1,4);
       exitLocations = new Location[numLocations];
       print(numLocations);
       for(int i=0; i<numLocations; i++) {
           exitLocations[i] = GenerateExitSpawn(map, tilemap, exitTile);
       }
    }
    // End Unity Init/Looping Functions********************************************************************************
}
