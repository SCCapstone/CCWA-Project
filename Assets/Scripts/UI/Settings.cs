using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void tutorialBtn()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void mainBackBtn()
    {
        SceneManager.LoadScene("Title Screen");
    }
}
