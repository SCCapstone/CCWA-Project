using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;



public class Tutorial : MonoBehaviour
{
    //Will eventually be the texts we actually use in game
    public string[] texts = {"This is the text for page 1.", "Undoubtably, the text for page 2.", "WOW! It's the text for page three."};
    public int iterator = 0;
    public int pageCounter = 1;
    public TextMeshProUGUI infoText;
    public Text pageCount;
    
    void Awake() {
        infoText = GetComponentInChildren<TextMeshProUGUI>();
        
        //Counter must be at top of the scene
        pageCount = GetComponentInChildren<Text>();
    }

    void Update() {
        infoText.text = texts[iterator];
        pageCount.text = pageCounter + "/3";
        
    }

    public void nextBtn() {
        if (iterator < 2) {
            ++iterator;
            ++pageCounter;
        }
    }

    public void backBtn() {
        if (iterator > 0) {
            --iterator;
            --pageCounter;
        } 
    }
    
    public void mainBackBtn() {
        SceneManager.LoadScene("Title Screen");
    }
}

