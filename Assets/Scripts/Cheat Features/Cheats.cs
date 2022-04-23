using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cheats : MonoBehaviour
{
    //End Key
    void DisableEnemies() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject boss = GameObject.FindWithTag("Boss");
        Destroy(boss);
        for(int i=0; i<enemies.Length; i++) {
            Destroy(enemies[i]);
        }
    }

    // Zero Key
    void HealPlayer() {
        var player = GameObject.FindWithTag("Player");
        Character character = player.GetComponent<Character>();
        character.health = 8;
    }

    // Page Down Key
    void MakePLayerInvincible() {
        var player = GameObject.FindWithTag("Player");
        Character character = player.GetComponent<Character>();
        if(Variables.isInvincible = false) {
            character.health = 10000;
            Variables.isInvincible = true;
        } else {
            character.health = 8;
            Variables.isInvincible = false;
        }
    }

    //Page Up Key
    void RespawnRoomEnemies() {
        var floorMap = GameObject.Find("Floor Map");
        FloorGenerator floorGenerator = floorMap.GetComponent<FloorGenerator>();
        RoomRenderer roomRenderer = floorMap.GetComponent<RoomRenderer>();
        Room currRoom = floorGenerator.GetCurrRoom();
        roomRenderer.RenderRoom(currRoom);
    }

    //Home key
    void GenerateNewFloor() {
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
        else if(Input.GetKeyDown(KeyCode.PageDown)) {
            MakePLayerInvincible();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha0)) {
            HealPlayer();
        }
    }
}
