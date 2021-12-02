using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorEnemy : Warrior
{
    void Awake() {
        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Kills the enemy
    public override void Die()
    {
        if (health <= 0) {
            Destroy(gameObject);
        }

    }
}
