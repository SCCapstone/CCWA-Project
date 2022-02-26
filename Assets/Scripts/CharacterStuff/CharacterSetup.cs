using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSetup : MonoBehaviour
{
    public Character buildCharacter() {
        string characterType = Variables.characterType;
        Character newCharacter = new Character();
        switch (characterType) {
            case "rogue": 
                newCharacter = new RogueCharacter();
            break;

            case "warrior":
                newCharacter = new PlayerWarrior();
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
    void Start()
    {
        //Spawn Player Character based on globally set variable
        // Character newCharacter = buildCharacter();
        GameObject player = GameObject.FindWithTag("Player");
        switch(Variables.characterType) {
            case "rogue":
                // The default character is currently the rogue. In this case, do nothing.
                break;
            case "warrior":
                // Remove base RogueCharacter script
                Destroy(player.gameObject.GetComponent("RogueCharacter"));
                // Add the WarriorCharacter script
                player.gameObject.AddComponent<PlayerWarrior>();
                break;
            // TODO implement case for mage
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
