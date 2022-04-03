using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/*Class for the player character warrior
* ----INPUTS----- 
* w a s d = movement
* j = attack
* u = JUICED
* esc = Pause
*----------------
*/

public class PlayerMage : Mage
{
    //Timer for shadow mode and it's maximum time
    public float juicedMax = 5f;
    public float juiceTimer;

    //Rigidbody for the player character and vector for movement directions
    private Rigidbody2D rigidB;
    private Vector2 moveDirection;

    //Animator for sprite animations
    private Animator animator;

    //Audio Source for sound effects
    private AudioSource audioSource;

    //Heart counter
    public Text textCurrHealth;
    public Text textMaxHealth;

    //Stamina bar to disable the UI for it
    public GameObject staminabar;

    //Mana counter
    public Text textCurrMana;
    public Text textMaxMana;
    public GameObject manaBar;
    public Image manaBottle;
    public GameObject MageProjectile;

    //Counters for the attacking frames
    public float attackTime = .35f;
    public float maxAttackTime = .35f;

    public GameObject pauseScreen;
    
    void Awake () {
        base.Awake();
        rigidB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        pauseScreen = GameObject.FindWithTag("paused");
        pauseScreen.SetActive(false);
        staminabar = GameObject.FindWithTag("staminabar");
        staminabar.SetActive(false);
        manaBar = GameObject.FindWithTag("manabar");
        moveSpeed = baseMoveSpeed;
        juiceTimer = juicedMax;
        name = "mage";
    }

    // Update is called once per frame
    void Update()
    {
        AssignWASD();
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Variables.isPaused = !Variables.isPaused;
            if (Variables.isPaused) {
                Pause();
            } else {
                Resume();
            }
        }

        //Enters the player character into juiced mode
        if (Input.GetKeyDown("u")) {
            ToggleEnhanced();
            Juiced();
            juiceTimer = juicedMax;
        }

        if(isEnhanced) {
            if (juiceTimer > 0) {
                juiceTimer -= Time.deltaTime;
            } else {
                //Set back to false
                ToggleEnhanced();
                //Set back to base mana
                Juiced();
                //reset the timer
                juiceTimer = juicedMax;
            }
        }

        //Allows the user to attack
        attack();

        //Stamina regeneration
        StartCoroutine("RegenStamina");

        //Mana regeneration
        StartCoroutine("RegenMana");

        //loading the amount of hearts a player has
        loadHearts();

        //loading the amount of stamina a player has
        //loadStamina();

        //loading the amount of mana a player has
        loadMana();

        if(health <= 0) {
            base.Die();
        }
    }

    void FixedUpdate() {
        Move();
    }

    //Getting and setting the user inputs for movement
    void Move() {
        rigidB.velocity = new Vector2 (moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        //updating the sprite to face the correct way when not moving by setting the idle blend tree values to last input value
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1) {
            animator.SetFloat("LastHorizontal", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("LastVertical", Input.GetAxisRaw("Vertical"));
        }
    }

    //Getting and setting the user inputs for movement
    void AssignWASD() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX,moveY).normalized;

        //Setting the sprite aniamtions
        //Controls the parameters in the blend tree based on the values entered from the moveDirection vector
        animator.SetFloat("Horizontal", moveDirection.x);
        animator.SetFloat("Vertical", moveDirection.y);
    }

    //Lets the player character attack
    public override void attack() {
        //only allows attack if there are at least two mana jars

        if (Input.GetKeyDown("j") && mana >= 2) {
            Vector2 characterLoc=gameObject.transform.position;
            animator.SetBool("attacking", true);
            audioSource.Play(0);
            attackTime = maxAttackTime;
            ShootBullet(characterLoc);
            ManaDrain(2);

            //TODO may need to add bullet functionality
        }

        //Ends the attack animation after a short period of time
        if (animator.GetBool("attacking")) {
            attackTime -= Time.deltaTime;
            if (attackTime <= 0) {
                animator.SetBool("attacking", false);
            }
        }
    }

    //Loads the health of the character of a character
    public void loadHearts() {
        GameObject text1 = GameObject.FindWithTag("currenthealth");
        textCurrHealth = text1.GetComponent<Text>();
        GameObject text2 = GameObject.FindWithTag("maxhealth");
        textMaxHealth = text2.GetComponent<Text>();

        textCurrHealth.text = health.ToString();
        textMaxHealth.text = "/ "+maxHealth.ToString();
    }

    //Loads the mana of the character
    public void loadMana() {
        //Enabling the mana bar for the mage
        manaBar.SetActive(true);
        GameObject text1 = GameObject.FindWithTag("currentmana");
        textCurrMana = text1.GetComponent<Text>();
        GameObject text2 = GameObject.FindWithTag("maxmana");
        textMaxMana = text2.GetComponent<Text>();

        int manaAsInt = Convert.ToInt32(mana);
        textCurrMana.text = manaAsInt.ToString();
        textCurrMana.color = Color.cyan;
        textMaxMana.text = "/ "+maxMana.ToString();
        
    }

    //Changing the color of the mana bottles if juiced
    public override void ColorChange() {
        base.ColorChange();
        GameObject bottle = GameObject.FindWithTag("manaicon");
        GameObject text1 = GameObject.FindWithTag("currentmana");
        GameObject text2 = GameObject.FindWithTag("maxmana");

        manaBottle = bottle.GetComponent<Image>();
        textCurrMana = text1.GetComponent<Text>();
        textMaxMana = text2.GetComponent<Text>();

        if (isEnhanced) {
            manaBottle.color = Color.magenta;
            textCurrMana.text = "∞";
            textMaxMana.text = "";
            textCurrMana.color = Color.magenta;
            textMaxMana.color = Color.magenta;
        } else {
            manaBottle.color = Color.white;
            textCurrMana.color = Color.cyan;
            textMaxMana.color = Color.white
            ;
        }
    }

    //Shoots bullets
    public void ShootBullet(Vector2 characterLoc) {
        Vector3 dir = new Vector2(animator.GetFloat("LastHorizontal"), animator.GetFloat("LastVertical"));
        Instantiate(MageProjectile,gameObject.transform.position , Quaternion.identity).GetComponent<MageProjectile>().Setup(dir);
    }

    //Pauses the game
    public void Pause() {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }

    //Resumes the game
    public void Resume() {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }
}
