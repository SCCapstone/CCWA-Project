using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// The purpose of the RoomGenerator is to be able to generate a single Room.
// A Room will contain a map of its floors and walls, an array of exitLocations, 
// A number of enemies, and a number of items

public class RoomGenerator {
    // Begin Variable statements***************************************************************************************
    public static int width = Constants.mapWidth;
    public static int height = Constants.mapHeight;

    public static int iterations = 5;
    private int[,] map = new int[height,width];

    public string seed = "";
    public bool useSeed = false;

    public int numEnemies = 0;
    public int numItems = 0;

    public Location[] exitLocations;
    public Location[] itemLocations;
    public Location[] enemyLocations;

    public Room storedRoom = new Room();
      
    // End Variable Statements*****************************************************************************************

    // Begin Constructors**********************************************************************************************

    //Construct a RoomGenerator capabale of generating rooms of HeightxWidth size
    public RoomGenerator(bool useSeed) {
        this.useSeed = useSeed;
    }
    // End Constructors************************************************************************************************

    // Begin room generation functions*********************************************************************************

    // This method takes the coordinates of a given cell in the map and calculates how many neighbors it has.
    // It utilizes von Neumann style neighborhoods, meaning a neighbor can exist only in a cardinal direction (not adjacent)
    // It returns an integer, the number of neighbors the cell at [i,j] has

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

    // This method takes a width, height (int) and a seed (string)
    // This method fills the map with random 0s and 1s
    // This is used as a way to populate the map before iterating to finalize the room shape
    // This method utilizes a seed, so the same Room can be generated repeatedly if needed
    // This method returns the int[,] map of the room
    // int[,] FillRoomMap(string seed) {
    //     string tempSeed;
    //     if(!useSeed) {
    //         tempSeed = Time.time.ToString();
    //     }
    //     else {
    //         tempSeed = seed;
    //     }
    //     System.Random rand = new System.Random(tempSeed.GetHashCode());
    //     int[,] m = new int[height,width];
    //     float fillChance = 20.0f;
    //     for(int i=0; i<height; i++) {
    //         for(int j=0; j<width; j++) {
    //             //Enclose the room with walls
    //             if(i == 0 || i == width-1 || j == 0 || j == height-1) {
    //                 m[i,j] = 1;
    //             }
    //             //Randomly add walls in the room based on a fill chance
    //             else {
    //                m[i,j] = (rand.Next(0,100) < fillChance) ? 1 : 0;
    //             }
    //         }
    //      }
    //      return m;
    // }

    // // This method iterates over a room and applies certain rules to change the room's shape
    // // 
    // int[,] IterateOverRoom(int[,] newMap) {
    //      for(int i=0; i<height; i++) {
    //         for(int j=0; j<width; j++) {
    //             int numNeighbors = CalcNeighbors(i,j);
    //             if(numNeighbors >= 2) {
    //                 newMap[i,j] = 1;
    //             }
    //         }  
    //     }
    //     return newMap;
    // }

    int squareDistance(int x, int y) {
        int dx = 2*x / width-1;
        int dy = 2*y / height-1;
        return dx*dx + dy*dy;
    }

    int[,] GenerateNoiseForRoom() {
        float scale = Random.Range(0.0f, 20.0f);
        for(int i=0; i<height; i++) {
            for(int j=0; j<width; j++) {
                float perlinValue = Mathf.PerlinNoise((j/scale),(i/scale));
                if(perlinValue > 0.6f) {
                    map[i,j] = 1;
                }
                else {
                    map[i,j] = 0;
                }
            }
        }
        return map;
    }

    public Room GenerateRoom(string seed, int numEnemies, int numItems, bool bossRoom, string[] directions) {
        // Generate a randomly filled Room map
        // Iterate over map to create a Room
        //int[,] newRoomMap = IterateOverRoom(FillRoomMap(seed));
        int[,] newRoomMap = GenerateNoiseForRoom();
        //int [,] finalMap = IterateOverRoom(newRoomMap);
        // Generate multiple exit locations for the room, given an array of directions
        Location[] exitLocations = GenerateMultipleExits(directions.Length, directions, newRoomMap);
        // Generate spawn locations for items
        Location[] itemLocation = GenerateLootSpawns(numItems);
        // Generate a list of enemy spawn locations
        Location[] enemyLocations = GenerateEnemySpawns(numEnemies);
        // Generate a Room object with the newly created map

        Location bossLocation = null;
        // Debug.Log("generating a boss room: "+bossRoom);
        if (bossRoom)
        {
            // Debug.Log("generating boss location");
            bossLocation = new Location("", 0, 0);
        }

        // Debug.Log(bossLocation.locX + " " + bossLocation.locY);
        return new Room(width, height, newRoomMap, seed, exitLocations, itemLocations, enemyLocations, bossLocation, numEnemies, numItems);


    }

    // End Room Generation Functions***********************************************************************************

    // Begin Spawn Location Generation Functions***********************************************************************
    void GenerateSpawnLocations(Room room) {
        GenerateEnemySpawns(room.numEnemies);
        GenerateLootSpawns(room.numItems);
        //GenerateExitSpawns(map, tilemap, exitTile);
    }

    Location[] GenerateEnemySpawns(int numEnemies) {
        Location[] newEnemyLocations = new Location[numEnemies];
        int tempEnemyCounter = 0;
        for(int i = height-3; i>height-7; i--) {
            if(tempEnemyCounter >= numEnemies) {
                this.enemyLocations = newEnemyLocations;
                return newEnemyLocations;
            }
            for(int j=10; j<width-10; j++) {
                if(map[i,j] == 0 && tempEnemyCounter < numEnemies) {
                    if(Random.Range(1,10) >= 1) {
                        newEnemyLocations[tempEnemyCounter] = new Location("", i, j);
                        tempEnemyCounter++;
                    }
                }
            }
        }
        this.enemyLocations = newEnemyLocations;
        return newEnemyLocations;
    }

    Location[] GenerateLootSpawns(int numItems) {
        Location[] newItemLocations = new Location[numItems];
        int tempItemCounter = 0;
        for(int i = height-3; i>height-7; i--) {
            if(tempItemCounter >= numItems) {
                this.itemLocations = newItemLocations;
                return newItemLocations;
            }
            for(int j=10; j<width-10; j++) {
                if(map[i,j] == 0 && tempItemCounter < numItems) {
                    if(Random.Range(1,10) >= 1) {
                        newItemLocations[tempItemCounter] = new Location("", i, j);
                        tempItemCounter++;
                    }
                   
                }
            }
        }
        this.itemLocations = newItemLocations;
        return newItemLocations;

    }

    // This method generates a single exit location in a room
    // It takes a direction (north, south, etc.) as a parameter, and returns a Location
    Location GenerateExitSpawn(string direction, int[,] map) {
        switch(direction) {
            case "east":
                    for(int i = height- 3; i>height-7; i--) {
                        for(int j=10; j<width-10; j++) {
                            if(map[i,j] == 0) {
                                if(Random.Range(1,10) < 9) {
                                    return new Location(direction, i, j);
                                }
                            }
                        }
                    }
                    break;
            case "west":
                    for(int i = 3; i<7; i++) {
                        for(int j=10; j<width-10; j++) {
                            if(map[i,j] == 0) {
                                if(Random.Range(1,10) < 9) {
                                    return new Location(direction, i, j);
                                }
                            }
                        }
                    }
                    break;
            case "south":
                    for(int i = 10; i<height-10; i++) {
                        for(int j=3; j<7; j++) {
                            if(map[i,j] == 0) {
                                if(Random.Range(1,10) < 9) {
                                    return new Location(direction, i, j);
                                }
                            }
                        }
                    }
                    break;
            case "north":
                    for(int i = 10; i<height-10; i++) {
                        for(int j=width-3; j>width-7; j--) {
                            if(map[i,j] == 0) {
                                if(Random.Range(1,10) < 9) {
                                    return new Location(direction, i, j);
                                    
                                }
                            }
                        }
                    }
                    break;
        }
        return new Location();
    }

    Location[] GenerateMultipleExits(int numOfLocations, string[] directions, int[,] map) {
        Location[] newLocations = new Location[numOfLocations];
        for(int i=0; i<directions.Length; i++) {
            newLocations[i] = GenerateExitSpawn(directions[i], map);
        }
        return newLocations;
    }

    // End Spawn Location Generation Functions*************************************************************************
}