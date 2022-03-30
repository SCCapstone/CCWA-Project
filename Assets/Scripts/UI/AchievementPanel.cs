using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementPanel : MonoBehaviour
{
    public GameObject panel;
    public GameObject panel2;
    public GameObject title;
    public GameObject title2;
    public GameObject description;
    public GameObject description2;
    public bool ach01Trigger = false;
    public bool ach02Trigger = false;
    public bool ach03Trigger = false;
    public bool ach04Trigger = false;
    public bool ach05Trigger = false;
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
        ach03Trigger = Variables.achievementTriggers[2];
        ach04Trigger = Variables.achievementTriggers[3];
        ach05Trigger = Variables.achievementTriggers[4];

        if(ach01Trigger)
        {
            StartCoroutine(Achieve01());
        }
        if(ach02Trigger)
        {
            StartCoroutine(Achieve02());
        }
        if(ach03Trigger)
        {
            StartCoroutine(Achieve03());
        }
        if(ach04Trigger)
        {
            StartCoroutine(Achieve04());
        }
        if(ach04Trigger)
        {
            StartCoroutine(Achieve05());
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
        ach02Trigger = false;
    }
    
    IEnumerator Achieve03()
    {
        title.GetComponent<TMPro.TextMeshProUGUI>().text = Constants.ALL_ACHIEVEMENT_TITLES[2];
        description.GetComponent<TMPro.TextMeshProUGUI>().text = Constants.ALL_ACHIEVEMENT_DESCRIPTIONS[2];
        panel.SetActive(true);
        yield return new WaitForSeconds(3);
        panel.SetActive(false);
        Variables.achievementTriggers[2] = false;
        ach03Trigger = false;
    }

    IEnumerator Achieve04()
    {
        title.GetComponent<TMPro.TextMeshProUGUI>().text = Constants.ALL_ACHIEVEMENT_TITLES[3];
        description.GetComponent<TMPro.TextMeshProUGUI>().text = Constants.ALL_ACHIEVEMENT_DESCRIPTIONS[3];
        panel.SetActive(true);
        yield return new WaitForSeconds(3);
        panel.SetActive(false);
        Variables.achievementTriggers[3] = false;
        ach04Trigger = false;
    }

    IEnumerator Achieve05()
    {
        title2.GetComponent<TMPro.TextMeshProUGUI>().text = Constants.ALL_ACHIEVEMENT_TITLES[4];
        description2.GetComponent<TMPro.TextMeshProUGUI>().text = Constants.ALL_ACHIEVEMENT_DESCRIPTIONS[4];
        panel2.SetActive(true);
        yield return new WaitForSeconds(3);
        panel2.SetActive(false);
        Variables.achievementTriggers[4] = false;
        ach05Trigger = false;
    }
}
