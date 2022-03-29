using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementPanel : MonoBehaviour
{
    public GameObject panel;
    public GameObject title;
    public GameObject description;
    public bool ach01Trigger = false;
    public bool ach02Trigger = false;
    // Update is called once per frame

    void Update()
    {
        // FileManager fm = GameObject.Find("FileManager").GetComponent<FileManager>();
        // var player = GameObject.FindWithTag("Player");
        // Character character = player.GetComponent<Character>();
        // //Save file on new floor
        // FileData currentFile = fm.GetFileData(Constants.VALID_FILE_NUMS[fm.CurrFile]);
        // FileData fd = new FileData(Constants.VALID_FILE_NUMS[fm.CurrFile], currentFile.DateCreated, currentFile.TotalTime, currentFile.FastestTime,
        //                                 currentFile.NumRuns+1, currentFile.NumWins, currentFile.UnlockedAchievements,
        //                                 false, null); //TODO get wins saved
        // Debug.Log(currentFile.NumRuns+" "+fd.NumRuns);                                        
        // fm.SaveFile(Constants.VALID_FILE_NUMS[fm.CurrFile], fd);

        ach01Trigger = Variables.achievementTriggers[0];
        ach02Trigger = Variables.achievementTriggers[1];

        if(ach01Trigger)
        {
            StartCoroutine(Achieve01());
        }
        if(ach02Trigger)
        {
            StartCoroutine(Achieve02());
        }
    }

    IEnumerator Achieve01()
    {
        title.GetComponent<TMPro.TextMeshProUGUI>().text = Constants.ALL_ACHIEVEMENT_TITLES[0];
        description.GetComponent<TMPro.TextMeshProUGUI>().text = Constants.ALL_ACHIEVEMENT_DESCRIPTIONS[0];
        panel.SetActive(true);
        yield return new WaitForSeconds(3);
        panel.SetActive(false);
        Variables.achievementTriggers[0] = false;
        ach01Trigger = false;
    }

    IEnumerator Achieve02()
    {
        title.GetComponent<TMPro.TextMeshProUGUI>().text = Constants.ALL_ACHIEVEMENT_TITLES[1];
        description.GetComponent<TMPro.TextMeshProUGUI>().text = Constants.ALL_ACHIEVEMENT_DESCRIPTIONS[1];
        panel.SetActive(true);
        yield return new WaitForSeconds(3);
        panel.SetActive(false);
        Variables.achievementTriggers[1] = false;
        ach01Trigger = false;
    }
}
