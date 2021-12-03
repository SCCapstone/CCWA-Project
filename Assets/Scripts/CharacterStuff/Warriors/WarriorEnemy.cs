using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorEnemy : Warrior
{
    public GameObject healthBar;
    
    void Awake() {
        base.Awake();
        defense = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Kills the enemy
    public override void Die()
    {
        Destroy(gameObject);
    }

    public override void DamageHealth(int collision)
    {
        base.DamageHealth(collision);
    }
}
