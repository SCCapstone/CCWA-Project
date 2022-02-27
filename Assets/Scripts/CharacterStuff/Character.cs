using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//This is the base class for all the characters that will be in the game
public class Character : MonoBehaviour
{
    //Variables for characters
    public int health, maxHealth = 6;
    public double stamina, maxStamina = 6.0;
    public int attackDmg;
    public int defense;
    public string name;
    public bool isEnhanced = false;
    public float moveSpeed;
    public float baseMoveSpeed = 7f;
    public bool poison =false;
    public List<bool> statuses;
    public Color spriteColor;
    public SpriteRenderer sRenderer;

    //added by Nick
    public int keyAmt = 0;
     
    public void Awake()
    {
        //freezes all characters when colliding
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }
 
    void Update()
    {

    }

    //Ensnared Status Effect
    public void ensnaredStatus(int statusTimeSec){
        StartCoroutine(ensnaredCoroutine(statusTimeSec));
    }
    public IEnumerator ensnaredCoroutine(int seconds){
        float tempMoveSpeed = moveSpeed;
        if (tempMoveSpeed>2f){
            moveSpeed=moveSpeed/2;
            StaminaDrain(2.0);
        }
        yield return new WaitForSeconds(seconds);
        moveSpeed=tempMoveSpeed;
    }

    //Poisoned Status Effect
    public void poisonedStatus(int statusTicks){
        poison=true;
        ColorChange();
        StartCoroutine(poisonedCoroutine(statusTicks));
    }
    public IEnumerator poisonedCoroutine(int ticks){
        yield return new WaitForSeconds(1f);
        for(int i=0;i<ticks;i++){
            health -= 1;
            yield return new WaitForSeconds(2f);
        }
        poison = false;
        ColorChange();
    }

    public void gainAKey () {
        keyAmt += 1;
    }

    //returns true if the a key is available to be used 
    public bool useKey () {
        if (keyAmt<1) {
            return false;
        }
        else {
            keyAmt -=1;
            return true;
        }
    }

    public void increaseAttackDmg(){
        attackDmg +=1;
    }

    public void increaseDefence(){
        defense +=1;
    }

    //Is the base form of attack for the character. Ready to be overriden
    //public virtual void Attack(BoxCollider2D collision) {}

    //Recovers the health of the character by a amount of points
    public void HealHealth (int a){
        health += a;
        //check that health doesn't go over the max
        if (health > maxHealth) {
            health = maxHealth;
        }
    }

    //Damages the health of the character by a amount of points
    public virtual void DamageHealth (int a) {
        health -= a;
        StartCoroutine(damageCoroutine());
    }
    public IEnumerator damageCoroutine(){
        gameObject.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        gameObject.GetComponent<Collider2D>().enabled = true;
        
    }

    //Recovers the stamina by a amount of points
    public void StaminaRecover (double a) {
        stamina += a;
        //check that stamina recover doesn't go over max
        if (stamina > maxStamina) {
            stamina  = maxStamina;
        }
    }

    //Function to periodically regenerate stamina. Implement with StartCoroutine("RegenStamina")
    public IEnumerator RegenStamina() {
        while (stamina < maxStamina) {
            StaminaRecover(.00001f);

            //Delays the stamina Regeneration
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    //Drains the stamina based on a amount of points
    public void StaminaDrain (double a){
        stamina -= a;
    }

    //Allows the character to attack
    public virtual void attack(){}
    //Toggles the character's special state if they have one
    public void ToggleEnhanced() {
        isEnhanced = !isEnhanced;
    }

    //changes the color of a sprite for status effects
    public virtual void ColorChange() {

        if (!isEnhanced) {
            spriteColor = Color.white;
            sRenderer.material.color = spriteColor;
        } 

        if (poison) {
            spriteColor = Color.green;
            sRenderer.material.color = spriteColor;
        }
    }

    //Deals with the character running out of health
    //Virtual because multiple things die differently
    public virtual void Die() {
        if(gameObject.CompareTag("Player"))
        {
            Debug.Log("PLAYER DEAD LOL GET CLAPPED");
            if(!Variables.isDead)
            {
                Variables.isDead = true;
                Variables.wonGame = false;
                Variables.inRun = false;
                Variables.newGame = false;
                FileManager fm = GameObject.Find("FileManager").GetComponent<FileManager>();
                var player = GameObject.FindWithTag("Player");
                Character character = player.GetComponent<Character>();
                //Save file on new floor
                FileData currentFile = fm.GetFileData(Constants.VALID_FILE_NUMS[fm.CurrFile]);
                FileData fd = new FileData(Constants.VALID_FILE_NUMS[fm.CurrFile], currentFile.DateCreated, currentFile.TotalTime, currentFile.FastestTime,
                                                currentFile.NumRuns+1, currentFile.NumWins, currentFile.UnlockedAchievements,
                                                false, null); //TODO get wins saved
                Debug.Log(currentFile.NumRuns+" "+fd.NumRuns);                                        
                fm.SaveFile(Constants.VALID_FILE_NUMS[fm.CurrFile], fd);
                SceneManager.LoadScene("GameOver");
            }
        }
    } 

//Adjusting stats with the different difficulties
    public void DifficutyAdjust() {
        switch(Variables.difficulty) {
            
            //easy
            case 0:
                health/=2;
                maxHealth/=2;
                attackDmg/=2;
                defense/=2;
                break;

            //hard
            case 2:
                health*=2;
                maxHealth*=2;
                attackDmg*=2;
                defense*=2;
                break;
        }
    }
    
}
