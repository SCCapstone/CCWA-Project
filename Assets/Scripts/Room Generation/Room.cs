using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    // Start Variables*************************************************************************************************

    int[,] map;
    int width;
    int height;

    // These variables are assigned in Unity using tile assets
    public Tilemap tilemap;
    public TileBase floorTile;
    public TileBase wallTile;

    // End Variables***************************************************************************************************

    // Start Constructors**********************************************************************************************

    public Room() {
        this.width = 0;
        this.height = 0;
        this.map = new int[width,height];
    }

    public Room(int width, int height, int[,] map) {
        this.width = width;
        this.height = height;
        this.map = map;
    }

    // End Constructors************************************************************************************************

    // Start Generation functions**************************************************************************************

     void RenderRoom() {
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

    // End Generation Functions****************************************************************************************

    // Start Unity Looping Functions***********************************************************************************

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // Assign the Room to a Tilemap and respective Tiles for the walls/floors
    // This should handle it from there
    void Update()
    {
        RenderRoom();
    }

    // End Unity Looping Functions*************************************************************************************
}
