using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSetup : MonoBehaviour
{

    public GameObject warriorPrefab;
    public GameObject roguePrefab;
    public GameObject magePrefab;

    public Character buildCharacter() {
        string characterType = Variables.characterType;
        Character newCharacter = new Character();
        switch (characterType) {
            case "rogue": 
                newCharacter = new PlayerRogue();
            break;

            case "warrior":
                newCharacter = new PlayerWarrior();
            break;

            case "mage": 
                newCharacter = new PlayerMage();
            break;

            default:
                Debug.Log("Invalid character name");
                newCharacter = newCharacter;
            break;
            // TODO implement mage character
        }
        return newCharacter;
    }

    // Start is called before the first frame update
    void Awake()
    {
        //Spawn Player Character based on globally set variable
        // Character newCharacter = buildCharacter();
        GameObject player = GameObject.FindWithTag("Player");
        switch(Variables.characterType) {
            case "rogue":
                Instantiate(roguePrefab, new Vector3(10,10,-1.0f), Quaternion.identity);
                break;

            case "warrior":
                Instantiate(warriorPrefab, new Vector3(10,10,-1.0f), Quaternion.identity);
                break;

             case "mage":
                Instantiate(magePrefab, new Vector3(10,10,-1.0f), Quaternion.identity);
                break;
        }
    }
}
