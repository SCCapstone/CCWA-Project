using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomRenderer : MonoBehaviour
{

    // Begin Variables*************************************************************************************************
    public Tilemap floorMap;
    public Tilemap wallMap;
    public Tilemap itemMap;
    public TileBase floorTile;
    public TileBase wallTile;
    public TileBase exitTile;
    public GameObject attackUp;
    public GameObject health;
    public GameObject enemy;
    public Room currentRoom;
    // End Variables***************************************************************************************************

    // Begin Setters***************************************************************************************************
    public void setCurrentRoom(Room newRoom) {
        this.currentRoom = newRoom;
    }

    // End Setters*****************************************************************************************************


    // Begin Rendering methods*****************************************************************************************
    public void RenderRoom(Room room) {
        // Clear the tilemap of any leftover tiles
        Debug.Log(room.seed);
        floorMap.ClearAllTiles();
        wallMap.ClearAllTiles();
        itemMap.ClearAllTiles();
        // Iterate over the map; place walls and floors
        for(int i=0; i<room.height; i++) {
            for(int j=0; j<room.width; j++) {
                switch(room.map[i,j]) {
                    case 0:
                        floorMap.SetTile(new Vector3Int(i,j,0), floorTile);
                        break;

                    case 1:
                        wallMap.SetTile(new Vector3Int(i,j,0), wallTile);
                        break;
                }
            }
        }
        // Iterate again; place exit locations
        for(int i=0; i<room.exitLocations.Length; i++) {
            Location l = room.exitLocations[i];
            floorMap.SetTile(new Vector3Int(l.locX, l.locY,0), exitTile);
        }

        // Iterate to place items in the room
        for(int i=0; i<room.itemLocations.Length; i++) {
            Location l = room.itemLocations[i];
            if(room.map[l.locX, l.locY] != 1) {
                if(i%2 == 0) {  //health
                    Instantiate(health, new Vector3(l.locX, l.locY, -1), Quaternion.identity);
                }
                else {  //attack up
                    Instantiate(attackUp, new Vector3(l.locX, l.locY, -1), Quaternion.identity);
                }
            }
        }
        Debug.Log(room.enemyLocations);
        //Iterate to generate enemy spawns
        for(int i=0; i<room.enemyLocations.Length; i++) {
            
            Location l = room.enemyLocations[i];
            Instantiate(enemy, new Vector3(l.locX, l.locY, -1), Quaternion.identity);
        }
    }
    // End Rendering methdos*******************************************************************************************

    // Start is called before the first frame update
    void Start()
    {
        // The following code is a test block. It is not intended to 
        // be the final functionality of this renderer.
        // TODO: Find a way to load a Room from Floor and attach it to this script

        // RoomGenerator generator = new RoomGenerator(true);

        // string[] testDirections = new string[] {
        //     "south",
        //     "east"
        // };
        // Room testRoom = generator.GenerateRoom("bootymeat", 3, 4, testDirections);
        // RenderRoom(testRoom);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}