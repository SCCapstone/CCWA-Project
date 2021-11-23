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

    public Tilemap tilemap;
    public TileBase floorTile;
    public TileBase wallTile;

    // End Variables***************************************************************************************************

    // Start Constructors**********************************************************************************************

    public Room() {
        this.width = 0;
        this.height = 0;
        this.map = new int[width,height];
        this.seed = "none";
    }

    public Room(int width, int height, int[,] map, string seed) {
        this.width = width;
        this.height = height;
        this.map = map;
        this.seed = seed;
    }

    // End Constructors************************************************************************************************


    // Start Render Functions******************************************************************************************

     void RenderRoom() {
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

    // End Render Functions********************************************************************************************
}