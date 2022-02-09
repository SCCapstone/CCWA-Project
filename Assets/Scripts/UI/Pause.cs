using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Pause : MonoBehaviour
{
    public GameObject difficultyButtons, speedRunMessage;
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
    }

    // Update is called once per frame
    void Update() {}
}
