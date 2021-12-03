using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Pause : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //if in speedrun mode disable the buttons and make their colors correct
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void tutorialBtn()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void achievementsBtn()
    {
        SceneManager.LoadScene("Achievements");
    }

    public void settingsBtn()
    {
        SceneManager.LoadScene("Settings");
    }

    public void quitBtn()
    {
        SceneManager.LoadScene("Title Screen");
    }
}
