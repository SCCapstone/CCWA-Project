using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        base.Die();
    }

    public override void DamageHealth(int damage)
    {
        base.DamageHealth(damage);
        bossUI.GetComponent<Slider>().value = health;
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
