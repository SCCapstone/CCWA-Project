using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using System;

public class RoomRenderer : MonoBehaviour
{

    // Begin Variables*************************************************************************************************
    public Tilemap floorMap = new Tilemap();
    public Tilemap wallMap = new Tilemap();
    public Tilemap itemMap = new Tilemap();
    public TileBase floorTile;
    public TileBase wallTile;
    public TileBase exitTile;
    public TileBase lockedExitTile;
    public GameObject attackUp;
    public GameObject health;
    public GameObject enemy;
    public GameObject boss;
    public Room currentRoom = new Room();
    public GameObject player;
    public int playerX = 0;
    public int playerY = 0;
    public Floor currFloor = new Floor();
    public RoomGenerator roomGenerator = new RoomGenerator(true);

    // End Variables***************************************************************************************************

    // Begin Setters***************************************************************************************************
    public void setCurrentRoom(Room newRoom) {
        this.currentRoom = newRoom;
    }

    // End Setters*****************************************************************************************************


    // Begin Rendering methods*****************************************************************************************
    public void RenderRoom(Room room) {
        // Clear the tilemap of any leftover tiles
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
            System.Random rng = new System.Random();
            int isLockedExit = rng.Next(1,20);
            Location l = room.exitLocations[i];
            if (isLockedExit == 1){
                floorMap.SetTile(new Vector3Int(l.locX, l.locY,0), lockedExitTile);
            }
            else{
                floorMap.SetTile(new Vector3Int(l.locX, l.locY,0), exitTile);
            }
            
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
        //Iterate to generate enemy spawns
        for(int i=0; i<room.numEnemies; i++) {
            Location l = room.enemyLocations[i];
            Instantiate(enemy, new Vector3(l.locX, l.locY, -1), Quaternion.identity);
        }
        if (!(room.bossLocation == null)){
            Location b = room.bossLocation;
            Instantiate(boss, new Vector3(b.locX, b.locY, -1), Quaternion.identity);
        }
    }
    // End Rendering methods*******************************************************************************************

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        player.GetComponent<SpriteRenderer>().color = Variables.skinColor;

        this.currFloor = Variables.currFloor;
        this.roomGenerator = new RoomGenerator(true);
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindWithTag("Player");
        if(currFloor != null) {
            for(int i=0; i<currentRoom.exitLocations.Length; i++) {
                Location l = currentRoom.exitLocations[i];
                int x = (int)Math.Floor(player.transform.position.x);
                int y = (int)Math.Floor(player.transform.position.y);
                int pX = Math.Abs(x);
                int pY = Math.Abs(y);

                if(pX == l.locX && pY == l.locY) {
                    string direction = l.location;
                    for(int j=0; j<currFloor.rooms.Length; j++) {
                        Room newRoom = currFloor.rooms[j];
                        if(direction == "north" && Array.Exists(newRoom.exitLocations, l => l.location == "south")) {
                            setCurrentRoom(newRoom);
                            Variables.currentRoom = newRoom;
                            RenderRoom(newRoom);
                            floorMap.GetComponent<FloorGenerator>().currRoomIdx = Array.IndexOf(currFloor.rooms,newRoom);
                            player.transform.position = new Vector3(10,10,-0.32f);
                        } else if(direction == "south" && Array.Exists(newRoom.exitLocations, l => l.location =="north")) {
                            setCurrentRoom(newRoom);
                            Variables.currentRoom = newRoom;
                            RenderRoom(newRoom);
                            floorMap.GetComponent<FloorGenerator>().currRoomIdx = Array.IndexOf(currFloor.rooms,newRoom);
                            player.transform.position = new Vector3(10,10,-0.32f);
                        } else if(direction == "west" && Array.Exists(newRoom.exitLocations, l => l.location =="east")) {
                            setCurrentRoom(newRoom);
                            Variables.currentRoom = newRoom;
                            RenderRoom(newRoom);
                            floorMap.GetComponent<FloorGenerator>().currRoomIdx = Array.IndexOf(currFloor.rooms,newRoom);
                            player.transform.position = new Vector3(10,10,-0.32f);
                            setCurrentRoom(newRoom);
                            Variables.currentRoom = newRoom;
                            RenderRoom(newRoom);
                            floorMap.GetComponent<FloorGenerator>().currRoomIdx = Array.IndexOf(currFloor.rooms,newRoom);
                            player.transform.position = new Vector3(10,10,-0.32f);
                        }
                    }
                }
            }
        }
    }
}