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
    public TileBase lockedExitTile;
    public GameObject attackUp;
    public GameObject health;
    public GameObject enemy;
    public GameObject boss;
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
        //Iterate to generate enemy spawns
        Debug.Log("PLEASEEEEE: " + room.enemyLocations.Length);
        for(int i=0; i<room.enemyLocations.Length; i++) {
            Location l = room.enemyLocations[i];
            Instantiate(enemy, new Vector3(l.locX, l.locY, -1), Quaternion.identity);
        }
        Debug.Log("here");
        if (!(room.bossLocation == null)){
            Debug.Log("up in here");
            Location b = room.bossLocation;
            Debug.Log("Boss spawning at: "+b.locX +" " + b.locY);
            Instantiate(boss, new Vector3(b.locX, b.locY, -1), Quaternion.identity);
        }
    }
    // End Rendering methods*******************************************************************************************

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
                Debug.Log("Player: "+ pX + " " + pY + " " + l.locX + " " + l.locY);
                //Debug.Log("Location: " + l.locX + " " + l.locY);

                if(pX == l.locX && pY == l.locY) {
                    Debug.Log("TRUE");
                    string direction = l.location;
                    for(int j=0; j<currFloor.rooms.Length; j++) {
                        Room newRoom = currFloor.rooms[j];
                        if(direction == "north" && Array.Exists(newRoom.exitLocations, l => l.location == "south")) {
                            this.currentRoom = newRoom;
                            Variables.currentRoom = newRoom;
                            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                        } else if(direction == "south" && Array.Exists(newRoom.exitLocations, l => l.location =="north")) {
                            this.currentRoom = newRoom;
                            Variables.currentRoom = newRoom;
                            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                        } else if(direction == "west" && Array.Exists(newRoom.exitLocations, l => l.location =="east")) {
                            this.currentRoom = newRoom;
                            Variables.currentRoom = newRoom;
                            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                        } else if(direction == "east" && Array.Exists(newRoom.exitLocations, l => l.location =="west")) {
                            this.currentRoom = newRoom;
                            Variables.currentRoom = newRoom;
                            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                        }
                    }
                }
            }
        }
    }
}