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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void loadScreen(bool didWin)
    {
        victory.SetActive(didWin);
        failure.SetActive(!didWin);
    }

    public void quitButton()
    {
        Application.Quit();
    }

    public  void mainMenuButton()
    {
        SceneManager.LoadScene("Title Screen");
    }

    public  void retryButton(){}
}
