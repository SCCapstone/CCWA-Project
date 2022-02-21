using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cheats : MonoBehaviour
{

    void DestroyAllEnemies() {
        WarriorEnemy[] warriorEnemies = GameObject.FindObjectsOfType<WarriorEnemy>();
        for(int i=0; i<warriorEnemies.Length; i++) {
            Destroy(warriorEnemies[i]);
        }
    }

    void MoveToNextRoom() {
        var floorMap = GameObject.Find("Floor Map");
        FloorGenerator floorGenerator = floorMap.GetComponent<FloorGenerator>();
        RoomRenderer roomRenderer = floorMap.GetComponent<RoomRenderer>();
        Room nextRoom = floorGenerator.GetCurrFloor().rooms[floorGenerator.currRoomIdx++];
        RoomRenderer.SetCurrentRoom(nextRoom);
    }

    void GenerateNewFloor() {
        var floorMap = GameObject.Find("Floor Map");
        FloorGenerator floorGenerator = floorMap.GetComponent<FloorGenerator>();
        floorGenerator.NewFloor(Random.Range(1,10));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.End)) {
            DestroyAllEnemies();
        }
        else if(Input.GetKeyDown(KeyCode.PageUp)) {
            MoveToNextRoom();
        }
        else if(Input.GetKeyDown(KeyCode.Home)) {
            GenerateNewFloor();
        }
    }
}
