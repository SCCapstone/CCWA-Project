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

    public GameObject runTime;

    Text RTT;

    // Start is called before the first frame update
    void Start()
    {
        loadScreen(Variables.wonGame);

        RTT = runTime.GetComponent<Text>();

        float timer = Variables.clock;
        int minutes = Mathf.FloorToInt(timer/60.0f);
        int seconds = Mathf.FloorToInt(timer - minutes *60);

        RTT.text = string.Format("Run Time: {0:00}:{1:00}", minutes, seconds);
    }

    // Update is called once per frame
    void Update() {}
    public void loadScreen(bool wonGame)
    {
        victory.SetActive(wonGame);
        failure.SetActive(!wonGame);
    }
}
