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

    public void DisplayDifficulty() {
        switch(Variables.difficulty) {
            case 0:
            nestedButtons[0].interactable = !nestedButtons[0].interactable;
            break;

            case 1:
            nestedButtons[1].interactable = !nestedButtons[1].interactable;
            break;

            case 2:
            nestedButtons[2].interactable = !nestedButtons[2].interactable;
            break;
        }
    }    
    // Update is called once per frame
    void Update() {}
}
