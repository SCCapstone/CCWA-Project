using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using System;

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
    public GameObject player;
    public int playerX;
    public int playerY;
    public Floor currFloor;

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
            Debug.Log(l.locX + " " + l.locY);
            Debug.Log(room);
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
        Variables.currentRoom = room;
    }
    // End Rendering methdos*******************************************************************************************

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); 
        currFloor = Variables.currFloor;
    }

    // Update is called once per frame
    void Update()
    {
        if(currFloor != null) {
            for(int i=0; i<currentRoom.exitLocations.Length; i++) {
                Location l = currentRoom.exitLocations[i];
                Debug.Log("Location: " + l.locX + " " + l.locY);
                int x = (int)Math.Floor(player.transform.position.x);
                int y = (int)Math.Floor(player.transform.position.y);
                int pX = Math.Abs(x);
                int pY = Math.Abs(y);
                if(pX == l.locY && pY == l.locX) {
                    Debug.Log("TRUE");
                    string direction = l.location;
                    for(int j=0; j<currFloor.rooms.Length; j++) {
                        Room newRoom = currFloor.rooms[j];
                        if(direction == "north" && Array.Exists(newRoom.exitLocations, l => l.location == "south")) {
                            this.currentRoom = newRoom;
                            Variables.currentRoom = newRoom;
                            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                            RenderRoom(newRoom);
                        } else if(direction == "south" && Array.Exists(newRoom.exitLocations, l => l.location =="north")) {
                            this.currentRoom = newRoom;
                            Variables.currentRoom = newRoom;
                            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                            RenderRoom(newRoom);
                        } else if(direction == "west" && Array.Exists(newRoom.exitLocations, l => l.location =="east")) {
                            this.currentRoom = newRoom;
                            Variables.currentRoom = newRoom;
                            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                            RenderRoom(newRoom);
                        } else if(direction == "east" && Array.Exists(newRoom.exitLocations, l => l.location =="west")) {
                            this.currentRoom = newRoom;
                            Variables.currentRoom = newRoom;
                            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                            RenderRoom(newRoom);
                        }
                    }
                }
            }
        }
    }
}