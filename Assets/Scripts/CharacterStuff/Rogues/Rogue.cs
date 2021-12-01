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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void shadowMode() {
        if (isEnhanced) {
            
        }
    }
}
