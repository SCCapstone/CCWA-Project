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
        BossDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Die()
    {
        Destroy(GameObject.Find("CharUICanvas/BossUI"));

        FileManager fm = GameObject.Find("FileManager").GetComponent<FileManager>();
        var player = GameObject.FindWithTag("Player");
        Character character = player.GetComponent<Character>();

        if(Variables.floorNum == Constants.MAX_FLOOR_NUM-1)
        {
            Variables.wonGame = true;
            //Save file on new floor
            FileData currentFile = fm.GetFileData(Constants.VALID_FILE_NUMS[fm.CurrFile]);
            FileData fd = new FileData(Constants.VALID_FILE_NUMS[fm.CurrFile], currentFile.DateCreated, currentFile.TotalTime, currentFile.FastestTime,
                                            currentFile.NumRuns, currentFile.NumWins++, currentFile.UnlockedAchievements,
                                            false, null); //TODO get wins saved
            fm.SaveFile(Constants.VALID_FILE_NUMS[fm.CurrFile], fd);
            Variables.inRun = false;
            Variables.newGame = false;
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
