using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameOver : MonoBehaviour
{

    public GameObject victory;
    public GameObject failure;

    public Text runTimeText;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        Variables.score = 0;

        Variables.isDead = false;
        loadScreen(Variables.wonGame);

        float timer = Variables.clock;
        int minutes = Mathf.FloorToInt(timer/60.0f);
        int seconds = Mathf.FloorToInt(timer - minutes *60);

        runTimeText.text = string.Format("Run Time: {0:00}:{1:00}", minutes, seconds);
        scoreText.text = string.Format("Score: {0:0000}", Variables.score);


        //check if a new fastest time has been set
        if (Variables.difficulty == 0)
        {
            if(timer < Variables.fastest_E)
                Variables.fastest_E = timer;
        }
        else if(Variables.difficulty == 1)
        {
            if(timer < Variables.fastest_M)
                Variables.fastest_M = timer;
        }
        else
        {
            if(timer < Variables.fastest_H)
                Variables.fastest_H = timer;
        }

    }

    // Update is called once per frame
    void Update() {}
    public void loadScreen(bool wonGame)
    {
        victory.SetActive(wonGame);
        failure.SetActive(!wonGame);
    }
}
