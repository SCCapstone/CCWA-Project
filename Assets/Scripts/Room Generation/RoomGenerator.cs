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

    public static int iterations = 500000;
    private int[,] map = new int[height,width];
    private int iterationCount = 5;

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

    public int[,] CreateRoomBorder(int[,] map) {
        for(int i=0; i<height; i++) {
            for(int j=0; j<width; j++) {
                if(i == 0 || i == width-1 || j == 0 || j == height-1) {
                    map[i,j] = 1;
                }
            }
        }
        this.map = map;
        return map;
    }

    public int[,] ClearSpawns(int[,] map) {
        for(int i=15; i<24; i++) {
            for(int j=10; j<15; j++) {
                if(map[i,j] != 2) {
                map[i,j] = 0;
                    
                }
            }
        }
        for(int i=7; i<15; i++) {
            for(int j=7; j<15; j++) {
                if(map[i,j] != 2) {
                    map[i,j] = 0;
                }
            }
        }
        this.map = map;
        return map;
    }

    public int[,] GenerateRoomWithPerlinNoise (string seed) {
        System.Random rand = new System.Random(seed.GetHashCode());
        int n = rand.Next();
        Vector2 offset = new Vector2(n*rand.Next(), n+rand.Next());
        int[,] map = new int[25,25];
        for(int i=0; i<height; i++) {
            for(int j=0; j<width; j++) {
                float x = ((j/1.8f * n) + (n + offset.x)) / n;
                float y = ((i/1.8f * n) + (n + offset.y)) / n;
                float perlinValue = Mathf.PerlinNoise(x,y);
                if(perlinValue > 0.65f) {
                    map[i,j] = 1;
                } else {
                    map[i,j] = 0;
                }
            }
        }
        return map;
    }

    public int[,] GenerateRoomMap(string seed) {
        return ClearSpawns(CreateRoomBorder(GenerateRoomWithPerlinNoise(seed)));
    }

    public Room GenerateRoom(string seed, int numEnemies, int numItems, bool bossRoom, string[] directions) {
        // Generate a randomly filled Room map
        // Iterate over map to create a Room
        int[,] newRoomMap = GenerateRoomMap(seed);
        // Generate multiple exit locations for the room, given an array of directions
        Location[] exitLocations = GenerateMultipleExits(directions.Length, directions, newRoomMap);
        // Generate spawn locations for items
        Location[] itemLocation = GenerateLootSpawns(numItems);
        // Generate a list of enemy spawn locations
        Location[] enemyLocations = GenerateEnemySpawns(numEnemies);
        // Generate a Room object with the newly created map

        Location bossLocation = null;
        if (bossRoom)
        {
            bossLocation = new Location("", 15, 15);
        }

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
                        if(map[i,j] == 0) {
                            newEnemyLocations[tempEnemyCounter] = new Location("", i, j);
                            tempEnemyCounter++;
                        }
                        
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
                                    return new Location(direction, i, j);
                            }
                        }
                    }
                    break;
            case "west":
                    for(int i = 3; i<7; i++) {
                        for(int j=10; j<width-10; j++) {
                            if(map[i,j] == 0) {
                                    return new Location(direction, i, j);
                            }
                        }
                    }
                    break;
            case "south":
                    for(int i = 10; i<height-10; i++) {
                        for(int j=3; j<7; j++) {
                            if(map[i,j] == 0) {
                                    return new Location(direction, i, j);
                            }
                        }
                    }
                    break;
            case "north":
                    for(int i = 10; i<height-10; i++) {
                        for(int j=width-3; j>width-7; j--) {
                            if(map[i,j] == 0) {
                                    return new Location(direction, i, j); 
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