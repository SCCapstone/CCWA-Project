using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Pause : MonoBehaviour
{
    public GameObject difficultyButtons, speedRunMessage;
    public Button[] nestedButtons;
    private TextMeshProUGUI messageTxt;
    public TextMeshProUGUI timeText;

    // Start is called before the first frame update
    void Start()
    {
        //if in speedrun mode disable the buttons and make their colors correct
        if(Variables.isSpeedrun)
        {
            difficultyButtons.active = false;
            speedRunMessage.active = true;

            messageTxt = speedRunMessage.GetComponent<TextMeshProUGUI>();
            if(Variables.difficulty == 0)
                messageTxt.text = "In Speedrun Mode\nDifficulty: Easy";
            else if(Variables.difficulty == 1)
                messageTxt.text = "In Speedrun Mode\nDifficulty: Medium";
            else
                messageTxt.text = "In Speedrun Mode\nDifficulty: Hard";
        }
        else
        {
            difficultyButtons.active = true;
            speedRunMessage.active = false;
        }
        DisplayDifficulty();
    }

    // Update is called once per frame
    void Update() {
        int minutes = Mathf.FloorToInt(Variables.clock/60.0f);
        int seconds = Mathf.FloorToInt(Variables.clock - minutes *60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        DisplayDifficulty();
    }

     /* Allows the user to see and change their difficulty mid-gameplay
    * Easy = 0
    * Normal = 1
    * Hard = 2
    */
    public void DisplayDifficulty() {
        var colorsNotSelected = nestedButtons[0].GetComponent<Button>().colors;
        var colorsSelected = nestedButtons[0].GetComponent<Button>().colors;
        colorsNotSelected.normalColor = Color.white;

        switch(Variables.difficulty) {
            case 0:
            colorsSelected.normalColor = Color.green;
            colorsSelected.selectedColor = Color.green;
            nestedButtons[0].GetComponent<Button>().colors = colorsSelected;
            nestedButtons[1].GetComponent<Button>().colors = colorsNotSelected;
            nestedButtons[2].GetComponent<Button>().colors = colorsNotSelected;
            break;

            case 1:
            colorsSelected.normalColor = Color.blue;
            colorsSelected.selectedColor = Color.blue;
            nestedButtons[0].GetComponent<Button>().colors = colorsNotSelected;
            nestedButtons[1].GetComponent<Button>().colors = colorsSelected;
            nestedButtons[2].GetComponent<Button>().colors = colorsNotSelected;
            break;

            case 2:
            colorsSelected.normalColor = Color.red;
            colorsSelected.selectedColor = Color.red;
            nestedButtons[0].GetComponent<Button>().colors = colorsNotSelected;
            nestedButtons[1].GetComponent<Button>().colors = colorsNotSelected;
            nestedButtons[2].GetComponent<Button>().colors = colorsSelected;
            break;
        }
    }
    

    //Functions for the click events of the buttons
    public void SwitchToEasy() {
        Variables.difficulty = 0;
    }

    public void SwitchToNormal() {
        Variables.difficulty = 1;
    }

    public void SwitchToHard() {
        Variables.difficulty = 2;
    }
}
