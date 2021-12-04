using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttacks : MonoBehaviour
{
    public int attack = 0;
    // Start is called before the first frame update
    void Start()
    {
        getPlayerAttackVal();
    }

    //getting player attack
    public void getPlayerAttackVal() {
        var player = GameObject.FindWithTag("Player");
        var playerScript = player.gameObject.GetComponent<PlayerWarrior>();
        attack = playerScript.attackDmg;
    }
    
    //Calculates the damage for the attack. MUST BE ON SWORD HIT [DIRECTION] HITBOX
    public void OnTriggerEnter2D(Collider2D collision) {
        //Debug.Log("we touched under the wonder truck");
        if (collision.tag == "Enemy" || collision.tag == "Boss") {
            //Gets the instance of the enemy or boss 
            var enemy = collision.GetComponent<WarriorEnemy>();
            if (enemy == null) {
                 enemy = collision.GetComponent<WarriorBoss>();
            }
            
            //calculating the damage done to the enemy
            int damage = attack - enemy.defense;

            //Damages the enemy's health via the player's attackDmg value
            if (damage <= 0) {
                //Always do at least one damage to an enemy
                enemy.DamageHealth(1);
            } else {
                enemy.DamageHealth(damage);
            }
            
            //Kills the enemy if their health is less 0
            if (enemy.health <= 0) {
                enemy.Die();
            }
            
            //updates enemy health bar
            enemy.healthBar.transform.localScale = new Vector3((float)enemy.health / 10, enemy.healthBar.transform.localScale.y, enemy.healthBar.transform.localScale.x);
        }
    }
}
