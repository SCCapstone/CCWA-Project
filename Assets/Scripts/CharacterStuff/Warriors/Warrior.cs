using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base warrior class
public class Warrior : Character
{
    // Start is called before the first frame update
    public void Awake()
    {
    //Setting base warrior stats
    base.Awake();
    health = 8;
    maxHealth = 8;
    stamina = 3.0;
    maxStamina = 3.0;
    attackDmg = 8;
    defense = 6;
    }

    //Doubles attack yet halves defense for warrior characters for a set period of time
    public void Berserk() {
        //Setting if enhanced is true
        if (isEnhanced) {
            attackDmg = attackDmg*2;
            defense = defense/2;
            ColorChange();
        } else {
            //reverting back to base stats
            attackDmg = 8;
            defense = 6;
            ColorChange();
        }
    }

    //change the color to a bright pink
    public override void ColorChange()
    {
        base.ColorChange();
        if (isEnhanced) {
            spriteColor = Color.red;
            sRenderer.material.color = spriteColor;
        }
    }


}
