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
    public Location[] enemyLocations;

    public Location bossLocation;

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
        Location[] enemyLocations,
        Location bossLocation,
        int numEnemies,
        int numItems
        ) {
        this.width = width;
        this.height = height;
        this.map = map;
        this.seed = seed;
        this.exitLocations = exitLocations;
        this.itemLocations = itemLocations;
        this.enemyLocations = enemyLocations;
        this.numEnemies = numEnemies;
        this.numItems = numItems;
        this.bossLocation = bossLocation;
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

    public Location[] getEnemyLocations() {
        return this.enemyLocations;
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

    public void setEnemyLocations(Location[] newEnemyLocations) {
        this.enemyLocations = newEnemyLocations;
    }

    public Location getBossLocation() {
        return this.bossLocation;
    }

    public void setBossLocation(Location newBossLocation) {
        this.bossLocation = newBossLocation;
    }

    public bool isBossRoom() {
        if (bossLocation == null)
        {
            return false;
        }
        return true;
    }
    // End Getter/Setter Functions*************************************************************************************
}