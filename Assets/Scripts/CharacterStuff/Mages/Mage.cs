using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Character
{
    public double mana, maxMana;
    // Start is called before the first frame update
    public void Awake()
    {
        base.Awake();
        //Setting base mage stats
        health = 4;
        maxHealth = 4;
        mana = 5.0;
        maxMana = 5.0;
        defense = 4;
        baseMoveSpeed = 10.0f;
    }

    //Allows the mage to cast with no mana cost for a set period of time
    public void Juiced() {
        //Setting if enhanced is true
        if(isEnhanced) {
            mana = 5.0;
            ColorChange();
        } else {
            ColorChange();
        }
    }

    public override void ColorChange() {
        base.ColorChange();
        if (isEnhanced) {
            spriteColor = Color.white;
            sRenderer.material.color = spriteColor;
        }
    }

    //allows the recovery of mana
    public void ManaRecover (double a) {
        mana += a;
        //checking that mana recovery doesn't exceed max
        if (mana > maxMana) {
            mana = maxMana;
        }
    }

    public async void ManaDrain (double a) {
        mana -= a;
    }

    //function to regenerate mana. Implement with StartCoroutine("RegenMana")
    public IEnumerator RegenMana() {
        while (mana < maxMana) {
            ManaRecover(.0001f);

            //Delaying the mana regeneration
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
