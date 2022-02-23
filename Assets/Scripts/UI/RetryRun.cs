using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryRun : MonoBehaviour
{
    public void Retry()
    {
        Variables.inRun = true;
        Variables.newGame = true;
        if(Variables.difficulty == 0 && Variables.isSpeedrun == false)
        {
            SceneManager.LoadScene("Gameplay");
        } else
        {
            System.Random temp = new System.Random();
            Variables.floorSeed = temp.Next().ToString();
            SceneManager.LoadScene("Gameplay");
        }
    }
}
