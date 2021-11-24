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

    public Location exitLocation;

    // End Variables***************************************************************************************************

    // Start Constructors**********************************************************************************************

    public Room() {
        this.width = 0;
        this.height = 0;
        this.map = new int[width,height];
        this.seed = "none";
    }

    public Room(int width, int height, int[,] map, string seed, Location exitLocation) {
        this.width = width;
        this.height = height;
        this.map = map;
        this.seed = seed;
        this.exitLocation = exitLocation;
    }

    // End Constructors************************************************************************************************


    // Start Getter/Setter Functions***********************************************************************************

    int getWidth() {
        return this.width;
    }

    int getHeight() {
        return this.height;
    }

    int[,] getMap() {
        return this.map;
    }

    string getSeed() {
        return this.seed;
    }

    Location getLocation() {
        return this.exitLocation;
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

    void setLocation(Location newExitLocation) {
        this.exitLocation = newExitLocation;
    }

    // End Getter/Setter Functions*************************************************************************************
}