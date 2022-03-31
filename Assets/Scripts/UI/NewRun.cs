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
    public GameObject skinButtons;
    private Toggle[] toggleArr;
    FileData currentFile;

    async void Awake()
    {
        FileManager fm = GameObject.Find("FileManager").GetComponent<FileManager>();
        currentFile = fm.GetFileData(Constants.VALID_FILE_NUMS[fm.CurrFile]);

        toggleArr = skinButtons.GetComponentsInChildren<Toggle>();

        if(toggleArr.Length != Constants.ALL_ACHIEVEMENT_DESCRIPTIONS.Length)
        {
            Debug.Log("incorrect number of skin toggles");
        }
        else
        {
            for(int i = 0; i < currentFile.UnlockedAchievements.Length; ++i)
            {
                if(currentFile.UnlockedAchievements[i] != null)
                {
                    Debug.Log("skin unlocked: "+ (i));
                    ColorBlock toggleColors = toggleArr[i].colors;
                    toggleColors.normalColor = Constants.UNLOCKABLE_SKIN_COLORS[i];
                    toggleColors.highlightedColor = Constants.UNLOCKABLE_SKIN_COLORS[i];
                    toggleColors.selectedColor = Constants.UNLOCKABLE_SKIN_COLORS[i];
                    toggleArr[i].colors = toggleColors;
                    Debug.Log(toggleArr[i].colors.normalColor.ToString());
                }
                else
                {
                    toggleArr[i].enabled = false;
                    ColorBlock toggleColors = toggleArr[i].colors;
                    toggleColors.normalColor = Color.clear;
                    toggleArr[i].colors = toggleColors;
                }
            }
        }
    }

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

    private async void setupRun()
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

        bool defaultSkin = true;
        //check if an unlocked skin has been selected
        for(int i=0; i<toggleArr.Length;++i)
        {
            if(toggleArr[i].isOn)
            {
                Variables.skinColor = Constants.UNLOCKABLE_SKIN_COLORS[i];
                Debug.Log("Seleced skin " + i);
                defaultSkin = false;
            }
        }
        if(defaultSkin)
        {
            if(Variables.characterType.Equals("warrior"))
            {
                Variables.skinColor = Constants.WARRIOR_SKIN_COLOR;
                Debug.Log("Playing with default warrior skin");
            }
            else if(Variables.characterType.Equals("rogue"))
            {
                Variables.skinColor = Constants.ROUGE_SKIN_COLOR;
                Debug.Log("Playing with default rouge skin");
            }
            else
            {
                Variables.skinColor = Constants.MAGE_SKIN_COLOR;
                Debug.Log("Playing with default mage skin");
            }
        }

        //TODO add input validation for seeds
        Variables.floorSeed = GameObject.Find("SeedField").GetComponent<TMP_InputField>().text;

        Debug.Log(Variables.isSpeedrun);
        Debug.Log(Variables.difficulty);

        SceneManager.LoadScene("Gameplay");
    }
}
