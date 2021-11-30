using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class holds information about the location of each rooms exit location ("door")
public class Location
{
    private string location;
    public int locX;
    public int locY;

    public Location() {
        this.location = "";
        this.locX = 0;
        this.locY = 0;
    }

    public Location(string newLocation, int newLocX, int newLocY) {
        this.location = newLocation;
        this.locX = newLocX;
        this.locY = newLocY;
    }

    string getLocation() {
        return this.location;
    }

    int getLocX() {
        return this.locX;
    }

    int getLocY() {
        return this.locY;
    }

    void setLocation(string newLocation) {
        this.location = newLocation;
    }

    void setLocX(int newLocX) {
        this.locX = newLocX;
    }

    void setLocY(int newLocY) {
        this.locY = newLocY;
    }
}

