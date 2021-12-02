using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorBoss : WarriorEnemy
{
    // Start is called before the first frame update
    void Awake()
    {
        base.Awake();
        defense = 3;
        maxHealth = 20;
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
    }

    void BossDisplay()
    {
        GameObject ui = GameObject.Find("CharUICanvas");
        GameObject bossUI = new GameObject("BossUI");
        bossUI.transform.SetParent(ui.transform);

        //TODO add healthbar
        UnityEngine.UI.Text nameText = bossUI.AddComponent<UnityEngine.UI.Text>();
        nameText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        nameText.fontSize = 22;
        nameText.text = this.name;
        nameText.GetComponent<RectTransform>().localPosition = new Vector3(250,-200,0);
    }
}
