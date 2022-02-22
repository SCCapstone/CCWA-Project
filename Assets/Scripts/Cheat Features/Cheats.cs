using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cheats : MonoBehaviour
{
    //End Key
    void DisableEnemies() {
        Debug.Log("Destroy Enemeies");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i=1; i<enemies.Length; i++) {
            if(enemies[i].name == "RedBirdEnemy(Clone)") {
                Destroy(enemies[i]);
            }
        }
    }

    //Page Up Key
    void RespawnRoomEnemies() {
        Debug.Log("Next Room");
        var floorMap = GameObject.Find("Floor Map");
        FloorGenerator floorGenerator = floorMap.GetComponent<FloorGenerator>();
        RoomRenderer roomRenderer = floorMap.GetComponent<RoomRenderer>();
        Room currRoom = floorGenerator.GetCurrRoom();
        roomRenderer.RenderRoom(currRoom);
    }

    //Home key
    void GenerateNewFloor() {
        Debug.Log("New Floor");
        DisableEnemies();
        var floorMap = GameObject.Find("Floor Map");
        FloorGenerator floorGenerator = floorMap.GetComponent<FloorGenerator>();
        floorGenerator.NewFloor(Random.Range(1,5));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.End)) {
            DisableEnemies();
        }
        else if(Input.GetKeyDown(KeyCode.PageUp)) {
            RespawnRoomEnemies();
        }
        else if(Input.GetKeyDown(KeyCode.Home)) {
            GenerateNewFloor();
        }
    }
}
