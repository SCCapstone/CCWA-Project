using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorEnemy : Warrior
{
    public GameObject healthBar;
    
    void Awake() {
        base.Awake();
        defense = 2;
        DifficutyAdjust();
    }

    //Kills the enemy
    public override void Die()
    {
        Variables.achievementTriggers[1] = true;
        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collidedWith) {
        var collision = collidedWith.collider;
        string name = collision.name;
        
        switch(name) {
            case "Rogue(Clone)":
                var rogue = collision.GetComponent<PlayerRogue>();
                //calculating the damage done to player
                int rDamage = attackDmg - rogue.defense;
                //Damages the player's health via the enemy's attackDmg value
                if (rDamage <= 0) {
                    //Always do at least one damage to a player
                    rogue.DamageHealth(1);
                } else {
                    rogue.DamageHealth(rDamage);
                }
                //Kills the player if their health is less 0
                if (rogue.health <= 0) {
                    rogue.Die();
                }
            break;

            case "Warrior(Clone)":
                var warrior = collision.GetComponent<PlayerWarrior>();
                //calculating the damage done to player
                int wDamage = attackDmg - warrior.defense;
                
                //Damages the player's health via the enemy's attackDmg value
                if (wDamage <= 0) {
                    //Always do at least one damage to a player
                    warrior.DamageHealth(1);
                } else {
                    warrior.DamageHealth(wDamage);
                }
                //Kills the player if their health is less 0
                if (warrior.health <= 0) {
                    warrior.Die();
                }
            break;

            case "Mage(Clone)":
                var mage = collision.GetComponent<PlayerMage>();
                //calculating the damage done to player
                int mDamage = attackDmg - mage.defense;
                
                //Damages the player's health via the enemy's attackDmg value
                if (mDamage <= 0) {
                    //Always do at least one damage to a player
                    mage.DamageHealth(1);
                } else {
                    mage.DamageHealth(mDamage);
                }
                //Kills the player if their health is less 0
                if (mage.health <= 0) {
                    mage.Die();
                }
            break;
            }
        }
}

   