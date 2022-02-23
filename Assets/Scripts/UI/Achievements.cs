using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Achievements : MonoBehaviour
{
    public Button[] allButtons, achievementButtons;
    public string[] achievementNames;
    public int currentActiveAchievementIndex;
    public TextMeshProUGUI currAchievementText;
    public Text pageCount;

    public string[] nonAchievementButtonNames = new string[] {"BackBtn", "NavRightBtn", "NavLeftBtn"};

    void Awake()
    {
        allButtons = GetComponentsInChildren<Button>();
        achievementButtons = new Button[allButtons.Length - nonAchievementButtonNames.Length];

        //populate achievementButtons with allButtons except values in nonAchievementButtonNames
        bool[] toCopy = new bool[allButtons.Length];
        for(int a = 0; a < allButtons.Length; ++a)
        {
            toCopy[a] = true;
            for(int b = 0; b < nonAchievementButtonNames.Length; ++b)
            {
                if(allButtons[a].name.Equals(nonAchievementButtonNames[0]))
                {
                    toCopy[a] = false;
                    break;
                }
            }
        }

        int achievementIndex = 0;
        for(int c = 0; c < toCopy.Length; ++ c)
        {
            if(toCopy[c])
            {
                achievementButtons[achievementIndex] = allButtons[c];
                ++achievementIndex;
            }
        }

        //check there are the same number of entries in achivementDescriptions and achievementButtons
        
        //populate achievementNames list
        achievementNames = new string[achievementButtons.Length];
        for(int i = 0; i < achievementNames.Length; ++i)
        {
            achievementNames[i] = achievementButtons[i].name;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentActiveAchievementIndex = 0;
        achievementButtons[0].Select();
        currAchievementText.text = achievementDescriptions[currentActiveAchievementIndex];
        Debug.Log("set currAcheivementsText");
    }

    // Update is called once per frame
    void Update() 
    {
        //pageCount.text = currentActiveAchievementIndex + "/" + achievementDescriptions.Length;
    }
    
    //connect this to On Click of each achievement button
    public void setActiveAchievement()
    {
        //update currentActiveAchievement
        string justClickedName = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(justClickedName);
        for(int i = 0; i < achievementNames.Length; ++i)
        {
            if(justClickedName.Equals(achievementNames[i]))
            {
                currentActiveAchievementIndex = i;
                break;
            }
        }
        currAchievementText.text = achievementDescriptions[currentActiveAchievementIndex];
    }

    public void decrementAchievement()
    {
        //if alrealy at leftmost achievement, don't do anything
        if (currentActiveAchievementIndex == 0) return;

        //
        --currentActiveAchievementIndex;
        achievementButtons[currentActiveAchievementIndex].Select();
        currAchievementText.text = achievementDescriptions[currentActiveAchievementIndex];
    }

    public void incrementAchievement()
    {
        //if already at rightmost achievement don't do anything
        if (currentActiveAchievementIndex == achievementButtons.Length) return;

        ++currentActiveAchievementIndex;
        achievementButtons[currentActiveAchievementIndex].Select();
        currAchievementText.text = achievementDescriptions[currentActiveAchievementIndex];
    }
}
