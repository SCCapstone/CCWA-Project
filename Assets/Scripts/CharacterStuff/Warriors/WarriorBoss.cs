using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WarriorBoss : WarriorEnemy
{
    GameObject bossUI;

    // Start is called before the first frame update
    void Awake()
    {
        base.Awake();
        defense = 3;
        maxHealth = 20;
        health = maxHealth;
        //BossDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Die()
    {
        Variables.achievementTriggers[2] = true;
        Destroy(GameObject.Find("CharUICanvas/BossUI"));

        FileManager fm = GameObject.Find("FileManager").GetComponent<FileManager>();
        var player = GameObject.FindWithTag("Player");
        Character character = player.GetComponent<Character>();
        
        float fastest_overall = -1f; //negative number means that the run was not in speedrun mode
        if(Variables.floorNum == Constants.MAX_FLOOR_NUM-1)
        {
            if(Variables.isSpeedrun)
            {
                if (Variables.difficulty == 0)
                {
                    //new fastest time on easy difficutly
                    if (Variables.clock < Variables.fastest_E)
                    {
                        Variables.fastest_E = Variables.clock;
                    }
                }
                else if (Variables.difficulty == 1)
                {
                    //new fastest time on medium
                    if (Variables.clock < Variables.fastest_M)
                    {
                        Variables.fastest_M = Variables.clock;
                    }
                }
                else
                {
                    //new fastest time on hard
                    if (Variables.clock < Variables.fastest_H)
                    {
                        Variables.fastest_H = Variables.clock;
                    }
                }

                //find the minimum time across all difficulties
                fastest_overall = Mathf.Min(Variables.fastest_E, Variables.fastest_M, Variables.fastest_H);
                Debug.Log(Variables.clock + " " + Variables.fastest_E + " " + Variables.fastest_M + " " + Variables.fastest_H + " really epic");
            }

            Variables.wonGame = true;
            //Save file on new floor
            FileData currentFile = fm.GetFileData(Constants.VALID_FILE_NUMS[fm.CurrFile]);
            FileData fd;
            int runs = currentFile.NumRuns + 1;
            int wins = currentFile.NumWins + 1;
            
            //if you are in speedrun mode and set a new fastest time
            if (fastest_overall != -1f && fastest_overall < currentFile.FastestTime)
            {
                Variables.achievementTriggers[4] = true;
                fd = new FileData(Constants.VALID_FILE_NUMS[fm.CurrFile], currentFile.DateCreated, currentFile.TotalTime, fastest_overall,
                                    runs, wins, currentFile.UnlockedAchievements,
                                    false, null); //TODO get wins saved
            }
            //if player was not in speedrun mode, did not set a fastest time, or both
            else
            {
                fd = new FileData(Constants.VALID_FILE_NUMS[fm.CurrFile], currentFile.DateCreated, currentFile.TotalTime, currentFile.FastestTime,
                                    runs, wins, currentFile.UnlockedAchievements,
                                    false, null); //TODO get wins saved
            }
           
            fm.SaveFile(Constants.VALID_FILE_NUMS[fm.CurrFile], fd);
            Variables.inRun = false;
            Variables.newGame = false;
            Variables.achievementTriggers[3] = true;
            SceneManager.LoadScene("GameOver");
        }else
        {
            ++Variables.floorNum;
            
            //Save file on new floor
            FileData currentFile = fm.GetFileData(Constants.VALID_FILE_NUMS[fm.CurrFile]);
            GameState gs = new GameState(character.health,(int)character.stamina,Variables.playerClass,Variables.floorNum,Variables.floorSeed);
            FileData fd = new FileData(Constants.VALID_FILE_NUMS[fm.CurrFile], currentFile.DateCreated, currentFile.TotalTime, currentFile.FastestTime,
                                            currentFile.NumRuns, currentFile.NumWins, currentFile.UnlockedAchievements,
                                            true, gs);
            fm.SaveFile(Constants.VALID_FILE_NUMS[fm.CurrFile],fd);

            //load next floor
            FloorGenerator fg = GameObject.Find("Grid/Floor Map").GetComponent<FloorGenerator>();
            fg.NewFloor(Variables.floorNum);
        }

        
        base.Die();

    }

    public override void DamageHealth(int damage)
    {
        base.DamageHealth(damage);
    }

    void BossDisplay()
    {
        GameObject ui = GameObject.Find("CharUICanvas");
        bossUI = new GameObject("BossUI");
        bossUI.transform.SetParent(ui.transform);

        //Displays the name of the boss
        Text nameText = bossUI.AddComponent<Text>();
        nameText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        nameText.fontSize = 50;
        nameText.text = this.name;
        nameText.GetComponent<RectTransform>().sizeDelta = new Vector2(200,100);
        nameText.GetComponent<RectTransform>().localPosition = new Vector3(0,-165,0);

        //Health bar
        // this.healthBar = Instantiate(this.healthBar, bossUI.transform);


    }
}
