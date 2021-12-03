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
    public Location[] itemLocations;

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
        Location[] itemLocations,
        int numEnemies,
        int numItems
        ) {
        this.width = width;
        this.height = height;
        this.map = map;
        this.seed = seed;
        this.exitLocations = exitLocations;
        this.itemLocations = itemLocations;
        this.numEnemies = numEnemies;
        this.numItems = numItems;
    }

    // End Constructors************************************************************************************************


    // Start Getter/Setter Functions***********************************************************************************

    public int getWidth() {
        return this.width;
    }

    public int getHeight() {
        return this.height;
    }

    public int[,] getMap() {
        return this.map;
    }

    public string getSeed() {
        return this.seed;
    }

    public Location[] getLocations() {
        return this.exitLocations;
    }

    public Location[] getItemLocations() {
        return this.itemLocations;
    }

    public void setWidth(int newWidth) {
        this.width = newWidth;
    }

    public void setHeight(int newHeight) {
        this.height = newHeight;
    }

    public void setMap(int[,] newMap) {
        this.map = newMap;
    }

    public void setSeed(string newSeed) {
        this.seed = newSeed;
    }

    public void setLocations(Location[] newExitLocations) {
        this.exitLocations = newExitLocations;
    }

    public void setItemLocations(Location[] newItemLocations) {
        this.itemLocations = newItemLocations;
    }

    // End Getter/Setter Functions*************************************************************************************
}