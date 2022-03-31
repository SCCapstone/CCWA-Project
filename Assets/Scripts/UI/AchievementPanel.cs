using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private FileManager fm;

    void Start()
    {
        fm = GameObject.Find("FileManager").GetComponent<FileManager>();
    }

    // Update is called once per frame

    void Update()
    {
        //Save file on new floor
        FileData currentFile = fm.GetFileData(Constants.VALID_FILE_NUMS[fm.CurrFile]);

        ach01Trigger = Variables.achievementTriggers[0];
        ach02Trigger = Variables.achievementTriggers[1];
        ach03Trigger = Variables.achievementTriggers[2];
        ach04Trigger = Variables.achievementTriggers[3];
        ach05Trigger = Variables.achievementTriggers[4];

        if(ach01Trigger && currentFile.UnlockedAchievements[0] == null)
        {
            StartCoroutine(Achieve01());
        }
        if(ach02Trigger && currentFile.UnlockedAchievements[1] == null)
        {
            StartCoroutine(Achieve02());
        }
        if(ach03Trigger && currentFile.UnlockedAchievements[2] == null)
        {
            StartCoroutine(Achieve03());
        }
        if(ach04Trigger && currentFile.UnlockedAchievements[3] == null && SceneManager.GetActiveScene().name == "GameOver")
        {
            StartCoroutine(Achieve04());
        }
        if(ach05Trigger && currentFile.UnlockedAchievements[4] == null && SceneManager.GetActiveScene().name == "GameOver")
        {
            StartCoroutine(Achieve05());
        }
    }

    IEnumerator Achieve01()
    {
        title.GetComponent<TMPro.TextMeshProUGUI>().text = Constants.ALL_ACHIEVEMENT_TITLES[0];
        description.GetComponent<TMPro.TextMeshProUGUI>().text = Constants.ALL_ACHIEVEMENT_DESCRIPTIONS[0];
        panel.SetActive(true);

        FileData currentFile = fm.GetFileData(Constants.VALID_FILE_NUMS[fm.CurrFile]);
        currentFile.UnlockedAchievements[0] = Constants.ALL_ACHIEVEMENT_TITLES[0];
        FileData fd = new FileData(Constants.VALID_FILE_NUMS[fm.CurrFile], currentFile.DateCreated, currentFile.TotalTime, currentFile.FastestTime,
                                        currentFile.NumRuns, currentFile.NumWins, currentFile.UnlockedAchievements,
                                        false, null);                           
        fm.SaveFile(Constants.VALID_FILE_NUMS[fm.CurrFile], fd);

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

        FileData currentFile = fm.GetFileData(Constants.VALID_FILE_NUMS[fm.CurrFile]);
        currentFile.UnlockedAchievements[1] = Constants.ALL_ACHIEVEMENT_TITLES[1];
        FileData fd = new FileData(Constants.VALID_FILE_NUMS[fm.CurrFile], currentFile.DateCreated, currentFile.TotalTime, currentFile.FastestTime,
                                        currentFile.NumRuns, currentFile.NumWins, currentFile.UnlockedAchievements,
                                        false, null);                           
        fm.SaveFile(Constants.VALID_FILE_NUMS[fm.CurrFile], fd);

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

        FileData currentFile = fm.GetFileData(Constants.VALID_FILE_NUMS[fm.CurrFile]);
        currentFile.UnlockedAchievements[2] = Constants.ALL_ACHIEVEMENT_TITLES[2];
        FileData fd = new FileData(Constants.VALID_FILE_NUMS[fm.CurrFile], currentFile.DateCreated, currentFile.TotalTime, currentFile.FastestTime,
                                        currentFile.NumRuns, currentFile.NumWins, currentFile.UnlockedAchievements,
                                        false, null);                           
        fm.SaveFile(Constants.VALID_FILE_NUMS[fm.CurrFile], fd);

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

        FileData currentFile = fm.GetFileData(Constants.VALID_FILE_NUMS[fm.CurrFile]);
        currentFile.UnlockedAchievements[3] = Constants.ALL_ACHIEVEMENT_TITLES[3];
        FileData fd = new FileData(Constants.VALID_FILE_NUMS[fm.CurrFile], currentFile.DateCreated, currentFile.TotalTime, currentFile.FastestTime,
                                        currentFile.NumRuns, currentFile.NumWins, currentFile.UnlockedAchievements,
                                        false, null);                           
        fm.SaveFile(Constants.VALID_FILE_NUMS[fm.CurrFile], fd);

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

        FileData currentFile = fm.GetFileData(Constants.VALID_FILE_NUMS[fm.CurrFile]);
        currentFile.UnlockedAchievements[4] = Constants.ALL_ACHIEVEMENT_TITLES[4];
        FileData fd = new FileData(Constants.VALID_FILE_NUMS[fm.CurrFile], currentFile.DateCreated, currentFile.TotalTime, currentFile.FastestTime,
                                        currentFile.NumRuns, currentFile.NumWins, currentFile.UnlockedAchievements,
                                        false, null);                           
        fm.SaveFile(Constants.VALID_FILE_NUMS[fm.CurrFile], fd);

        yield return new WaitForSeconds(3);
        panel2.SetActive(false);
        Variables.achievementTriggers[4] = false;
        ach05Trigger = false;
    }
}
