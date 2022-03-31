using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementList : MonoBehaviour
{
    public GameObject text;
    // Update is called once per frame
    void Update()
    {
        FileManager fm = GameObject.Find("FileManager").GetComponent<FileManager>();
        FileData currentFile = fm.GetFileData(Constants.VALID_FILE_NUMS[fm.CurrFile]);
        string totalText = "";
        for(int i=0;i<currentFile.UnlockedAchievements.Length;i++)
        {
            if(currentFile.UnlockedAchievements[i] != null)
            {
                totalText += Constants.ALL_ACHIEVEMENT_TITLES[i] + "\n";
            }
        }
        if(totalText == "")
        {
            totalText = "No Achievements Unlocked";
        }
        text.GetComponent<TMPro.TextMeshProUGUI>().text = totalText;
    }
}
