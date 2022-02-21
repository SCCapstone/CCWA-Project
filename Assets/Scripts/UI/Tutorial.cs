using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


//This class handles the inputs for the tutorial screen
public class Tutorial : MonoBehaviour
{
    //Must be private for the text to update on gui
    private string[] texts = {"You can move the player with the following arrow keys shown at the bottom, or the WASD keys during gameplay.",
    "You may attack with the J key, and active your special mode with the U key. Pause with the esc key.", 
    "This is a basic enemy. He will chase you and hurt you if he touches you.",
    "This is the first boss. He has more health than the other enemy. You have been warned."};

    public Sprite[] spriteArray;

    public int iterator = 0;
    public int pageCounter = 1;
    public TextMeshProUGUI infoText;
    public Text pageCount;
    public GameObject enemy;
    public Image enemyDisplay;

    void Awake() {
        infoText = GetComponentInChildren<TextMeshProUGUI>();
        
        //Counter must be at top of the scene
        pageCount = GetComponentInChildren<Text>();

        //Getting the image of the enemy
        enemy = GameObject.FindWithTag("enemyImage");
        enemyDisplay = enemy.GetComponent<Image>();
        enemyDisplay.enabled = false;
    }

    public void nextBtn() {
        if (iterator < 3) {
            ++iterator;
            ++pageCounter;
        }
        infoText.text = texts[iterator];
        pageCount.text = pageCounter + "/" + texts.Length;
        ImageChange();
    }

    public void backBtn() {
        if (iterator > 0) {
            --iterator;
            --pageCounter;
        } 
        infoText.text = texts[iterator];
        pageCount.text = pageCounter + "/" + texts.Length;
        ImageChange();
    }

    public void ImageChange() {
        switch(iterator) {
            case 0:
                enemyDisplay.enabled = false;
                break;

            case 1:
                enemyDisplay.enabled = false;
                break;
            
            case 2:
                enemyDisplay.enabled = true;
                enemyDisplay.sprite = spriteArray[0];
                break;
            
            case 3:
               enemyDisplay.sprite = spriteArray[1];
                break;
            
        }
    }
}

