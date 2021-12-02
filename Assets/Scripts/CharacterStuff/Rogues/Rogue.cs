using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rogue : Character
{
    // Start is called before the first frame update
    public void Awake()
    {
        //Setting base rogue stats
        health = 6;
        maxHealth = 6;
        stamina = 6.0;
        maxStamina = 6.0;
        attackDmg = 6;
        defense = 4;
        baseMoveSpeed = 10f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Allows the user to enter a stealth mode. 
    //Just disables their pathfinding scripts for the time being
    public void shadowMode() {
        // getting all the enemies on the screen
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        

        ColorChange();
        if (allEnemies.Length != 0)
        {
            //disabling their pathfinding script
            if (isEnhanced) {
                for (int i = 0; i < allEnemies.Length; ++i) {
                    allEnemies[i].GetComponent<EnemyPathfinding>().enabled = false;
                }
            } else {
                for (int i = 0; i < allEnemies.Length; ++i) {
                    allEnemies[i].GetComponent<EnemyPathfinding>().enabled = true;
                }
            }
        }
    }

    //changes the color to black for shadowy goodness
    public override void ColorChange()
    {
        base.ColorChange();
        if (isEnhanced) {
            spriteColor = Color.black;
            sRenderer.material.color = spriteColor;
        }
    }
}
