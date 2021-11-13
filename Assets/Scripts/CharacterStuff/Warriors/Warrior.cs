using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base warrior class
public class Warrior : Character
{
    // Start is called before the first frame update
    void Awake()
    {
    //Setting base warrior stats
    health = 8;
    maxHealth = 8;
    stamina = 3.0;
    maxStamina = 3.0;
    attackDmg = 8;
    defense = 6;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Doubles attack yet halves defense for warrior characters for a set period of time
    public void Berserk() {
        //Setting if enhanced is true
        if (isEnhanced) {
            attackDmg = attackDmg*2;
            defense = defense/2;
        } else {
            //reverting back to base stats
            attackDmg = 8;
            defense = 6;
        }
       
    }


}
