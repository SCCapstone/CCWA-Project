using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room
{
    // Start Variables*************************************************************************************************

    public int[,] map;
    public int width;
    public int height;
    public string seed;

    public int numEnemies;
    public int numItems;

    public Location[] exitLocations;

    // End Variables***************************************************************************************************

    // Start Constructors**********************************************************************************************

    //Default Constructor: Builds a "blank" room object
    public Room() {
        this.width = 0;
        this.height = 0;
        this.map = new int[width,height];
        this.seed = "none";
    }

    // Fully parameterized constructor:
    //Builds a room with all parameters specified.
    public Room(
        int width, 
        int height, 
        int[,] map, 
        string seed, 
        Location[] exitLocations,
        int numEnemies,
        int numItems
        ) {
        this.width = width;
        this.height = height;
        this.map = map;
        this.seed = seed;
        this.exitLocations = exitLocations;
        this.numEnemies = numEnemies;
        this.numItems = numItems;
    }

    // End Constructors************************************************************************************************


    // Start Getter/Setter Functions***********************************************************************************

    int getWidth() {
        return this.width;
    }

    int getHeight() {
        return this.height;
    }

    public int[,] getMap() {
        return this.map;
    }

    string getSeed() {
        return this.seed;
    }

    Location[] getLocations() {
        return this.exitLocations;
    }

    void setWidth(int newWidth) {
        this.width = newWidth;
    }

    void setHeight(int newHeight) {
        this.height = newHeight;
    }

    void setMap(int[,] newMap) {
        this.map = newMap;
    }

    void setSeed(string newSeed) {
        this.seed = newSeed;
    }

    void setLocations(Location[] newExitLocations) {
        this.exitLocations = newExitLocations;
    }

    // End Getter/Setter Functions*************************************************************************************
}