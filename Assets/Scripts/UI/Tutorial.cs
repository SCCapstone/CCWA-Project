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
    private string[] texts = {
    "Charlie and Charlinda's Whacky Adventure is a top down 2d action game that focuses around Charlie's adventure into \n the fabled bird cave! Though he has been warned, he enters the dungeon in hopes of achieving glory fighting these whacky lookin beasts!",
    "You can move Charlie with the following arrow keys shown at the bottom, or the WASD keys during gameplay.",
    "J = Attack \nU = Special Ability \nESC = Pause/Unpause. \nUse the mouse in menus.",
    "Head to brown doors in each room to navigate to the next room! There will be many traps and enemies to harm you on your way. If your health ever reaches zero... GAME OVER!",
    "This is a basic enemy. He will chase you and hurt you if he touches you.",
    "This is the first boss. He has more health and will be harder to defeat. If you can manage it, you'll be able to move to the next floor!",
    "Bear traps will temporarily halve your movement speed if you get ensared! \n Poison spills will temporarily drain your health if you run over one!",
    "You can restore your health by picking up health kits!",
    "Touching the sword power-up or shield power-up will permanently increase your attack or defensive stats by 1!",
    "Pick up keys and approach a locked door or chest to open them! \n Good Luck!"};

    //Variables for the page counter
    public int iterator;
    public int pageCounter;
    public TextMeshProUGUI infoText;
    public Text pageCount;
    
    //Variables for the images
    public Sprite[] exampleObjects;
    public Image enemyDisplay;
    public Image item1Display;
    public Image item2Display;


    void Awake() {
        iterator = 0;
        pageCounter = 1;
        infoText = GetComponentInChildren<TextMeshProUGUI>();
        
        //Counter must be at top of the scene
        pageCount = GetComponentInChildren<Text>();

        //Getting the image of the enemy
        enemyDisplay.sprite = exampleObjects[0];
        item1Display.sprite = exampleObjects[2];
        item2Display.sprite = exampleObjects[4];

        ImageChange();
    }

    
    public void nextBtn() {
        if (iterator < texts.Length - 1) {
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
                item1Display.enabled = false;
                item2Display.enabled = false;
                break;
            
            case 3:
                enemyDisplay.enabled = false;
                break;
            
            case 4:
                enemyDisplay.enabled = true;
                enemyDisplay.sprite = exampleObjects[0];
                break;
            
            case 5:
                enemyDisplay.enabled = true;
                enemyDisplay.sprite = exampleObjects[1];
                item1Display.enabled = false;
                item2Display.enabled = false;
                break;
            
            case 6:
                enemyDisplay.enabled = false;
                item1Display.enabled = true;
                item1Display.sprite = exampleObjects[2];
                item2Display.enabled = true;
                item2Display.sprite = exampleObjects[3];
                break;

            case 7:
                item1Display.sprite = exampleObjects[4];
                item2Display.enabled = false;
                break;
            
            case 8:
                item1Display.sprite = exampleObjects[5];
                item2Display.enabled = true;
                item2Display.sprite = exampleObjects[6];
                break;
            
            case 9:
                item1Display.sprite = exampleObjects[7];
                item2Display.sprite = exampleObjects[8];
                break;
            
        }
    }
}

