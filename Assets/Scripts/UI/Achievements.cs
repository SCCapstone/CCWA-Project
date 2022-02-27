using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Achievements : MonoBehaviour
{
    public GameObject achievementButtonsGO;
    public TextMeshProUGUI currAchievementText, achievementIndexer;
    public Button[] achievementButtons;
    public int currentActiveAchievementIndex;

    void Awake()
    {
        //get all achievement buttons from GameObject
        achievementButtons = achievementButtonsGO.GetComponentsInChildren<Button>();

        //check that the correct number of achievement buttons were found
        Debug.Log("Found " + achievementButtons.Length + " achievement buttons");
        Debug.Log("there are " + Constants.ALL_ACHIEVEMENT_TITLES.Length + " achievement names");
        Debug.Log("There are " + Constants.ALL_ACHIEVEMENT_DESCRIPTIONS.Length + " achievement descriptions");

        if(achievementButtons.Length != Constants.ALL_ACHIEVEMENT_TITLES.Length || achievementButtons.Length != Constants.ALL_ACHIEVEMENT_DESCRIPTIONS.Length)
        {
            SceneManager.LoadScene("File Select");
            Debug.Log("there was a mismatch between the number of achievement buttons and either achievement titles or descriptions");
        }

        //load current file
        FileManager fm = GameObject.Find("FileManager").GetComponent<FileManager>();
        FileData currentFile = fm.GetFileData(Constants.VALID_FILE_NUMS[fm.CurrFile]);

        //set text of achievement buttons
        for (int i = 0; i < achievementButtons.Length; ++i)
        {
            TextMeshProUGUI currText;
            currText = achievementButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            currText.text = Constants.ALL_ACHIEVEMENT_TITLES[i];
        }

        //change colors of unlocked achievement button
        for(int i = 0; i < currentFile.UnlockedAchievements.Length; ++i)
        {
            for(int c = 0; c < Constants.ALL_ACHIEVEMENT_TITLES.Length; ++c)
            {
                if (currentFile.UnlockedAchievements[i].Equals(Constants.ALL_ACHIEVEMENT_TITLES[c]))
                {
                    ColorBlock cb = achievementButtons[c].colors;
                    cb.normalColor = new Color(0.180f, 0.741f, 0.082f, 1.0f);
                    cb.selectedColor = new Color(0.125f, 0.478f, 0.062f, 1.0f);
                    achievementButtons[c].colors = cb;
                }
            }
        }

        //select the first achievement
        currentActiveAchievementIndex = 0;
        achievementButtons[currentActiveAchievementIndex].Select();
    }

    //Takes you back to the main menu, idk why i can't see it in my scene editor rn
    public void mainBackBtn()
    {
        SceneManager.LoadScene("Settings");
    }

    //connect this to On Click of each achievement button
    public void setActiveAchievement()
    {
        //update currentActiveAchievement
        GameObject justClickedBtn = EventSystem.current.currentSelectedGameObject;
        TextMeshProUGUI btnText = justClickedBtn.GetComponentInChildren<TextMeshProUGUI>();

        for(int i = 0; i < Constants.ALL_ACHIEVEMENT_TITLES.Length; ++i)
        {
            if(btnText.text.Equals(Constants.ALL_ACHIEVEMENT_TITLES[i]))
            {
                currentActiveAchievementIndex = i;
                break;
            }
        }
    }

    public void decrementAchievement()
    {
        //if alrealy at leftmost achievement, don't do anything
        if (currentActiveAchievementIndex == 0) 
        {
            achievementButtons[currentActiveAchievementIndex].Select();
            return;
        }

        --currentActiveAchievementIndex;
        achievementButtons[currentActiveAchievementIndex].Select();
    }

    public void incrementAchievement()
    {
        //if already at rightmost achievement don't do anything
        if (currentActiveAchievementIndex == achievementButtons.Length-1)
        {
            achievementButtons[currentActiveAchievementIndex].Select();
            return;
        }

        ++currentActiveAchievementIndex;
        achievementButtons[currentActiveAchievementIndex].Select();
    }

    void Update()
    {
        currAchievementText.text = Constants.ALL_ACHIEVEMENT_DESCRIPTIONS[currentActiveAchievementIndex];
        achievementIndexer.text = string.Format("{0}/{1}", currentActiveAchievementIndex+1, Constants.ALL_ACHIEVEMENT_TITLES.Length);
    }
}