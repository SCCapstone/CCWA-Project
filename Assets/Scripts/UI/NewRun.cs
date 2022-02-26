using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

//This class handles the inputs for the file select screen
public class NewRun : MonoBehaviour
{
    public Toggle speedrunToggle;

    public Toggle easy, medium, hard;

    public void selectMage() {

    }

    public void selectWarrior() {
        Variables.characterType = "warrior";
        setupRun();
    }

    public void selectRogue() 
    {
        Variables.characterType = "rogue";
        setupRun();
    }

    private void setupRun()
    {
        //check starting difficulty of the game
        if (easy.isOn)
        {
            Variables.difficulty = 0;
        }
        else if (medium.isOn)
        {
            Variables.difficulty = 1;
        }
        else
        {
            Variables.difficulty = 2;
        }
        
        //check if the game is a speedrun or not
        if(speedrunToggle.isOn)
        {
            Variables.isSpeedrun = true;
        }
        else
        {
            Variables.isSpeedrun = false;
        }

        //TODO add input validation for seeds
        Variables.floorSeed = GameObject.Find("SeedField").GetComponent<TMP_InputField>().text;

        Debug.Log(Variables.isSpeedrun);
        Debug.Log(Variables.difficulty);

        SceneManager.LoadScene("Gameplay");
    }
}
