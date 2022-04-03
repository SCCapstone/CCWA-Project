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
* u = BERSERK MODE
* esc = pause
*----------------
*/
public class PlayerWarrior : Warrior
{
    //Timer for beserk mode and it's maximum time
    public float berserkMax = 10f;
    public float berserkTimer;

    //Rigidbody for the player character and vector for movement directions
    private Rigidbody2D rigidB;
    private Vector2 moveDirection;

    //Animator and sprite renderer for sprite animations
    private Animator animator;
    //private SpriteRenderer sRenderer;

    //Audio Source for sound effects
    private AudioSource audioSource;

    //Heart counter
    public Text textCurrHealth;
    public Text textMaxHealth;

    //Stamina counter
    public Text textCurrStamina;
    public Text textMaxStamina;

    //Counters for the attacking frames
    public float attackTime = .25f;
    public float maxAttackTime = .25f; 

    public GameObject pauseScreen;
    public GameObject manaBar;

    void Awake() {
        base.Awake();
        rigidB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        pauseScreen = GameObject.FindWithTag("paused");
        pauseScreen.SetActive(false);
        manaBar = GameObject.FindWithTag("manabar");
        manaBar.SetActive(false);
        moveSpeed = baseMoveSpeed;
        berserkTimer = berserkMax;
        name = "warrior";
    }
    
    void Update() {
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

        //Enters the player character into berserk mode
        if (Input.GetKeyDown("u")) {
            ToggleEnhanced();
            Berserk();
            berserkTimer = berserkMax;
        }

        if(isEnhanced) {
            if (berserkTimer > 0) {
                berserkTimer -= Time.deltaTime;
            } else {
                //Set back to false
                ToggleEnhanced();
                //Set back to base stats
                Berserk();
                //reset the timer
                berserkTimer = berserkMax;
            }
        }

        //Allows the user to attack
        attack();

        //Stamina regeneration
        StartCoroutine("RegenStamina");

        //loading the amount of hearts a player has
        loadHearts();

        //loading the amount of stamina a player has
        loadStamina();

        if(health <= 0) {
            base.Die();
        }
    }

    void FixedUpdate() {
        Move();  
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

    //Function that allows the player character to move with WASD
    void Move() {
        rigidB.velocity = new Vector2 (moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        //updating the sprite to face the correct way when not moving by setting the idle blend tree values to last input value
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1) {
            animator.SetFloat("LastHorizontal", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("LastVertical", Input.GetAxisRaw("Vertical"));
        }
    }

    //Lets the player character attack
    public void attack() {
        //only allows attack if there at least two stamina jars
        if (Input.GetKeyDown("j") && stamina >= 2) {
            animator.SetBool("attacking", true);
            audioSource.Play(0);
            attackTime = maxAttackTime;
            StaminaDrain(2);

        }

        //Ends the attack animation after a short period of time
        if (animator.GetBool("attacking")) {
            attackTime -= Time.deltaTime;
            if (attackTime <= 0) {
                animator.SetBool("attacking", false);
            }
        } 
    }

    //Loads the health of the character re  of a character
    public void loadHearts() {
        GameObject text1 = GameObject.FindWithTag("currenthealth");
        textCurrHealth = text1.GetComponent<Text>();
        GameObject text2 = GameObject.FindWithTag("maxhealth");
        textMaxHealth = text2.GetComponent<Text>();

        textCurrHealth.text = health.ToString();
        textMaxHealth.text = "/ "+maxHealth.ToString();
        }

    public void loadStamina() {
        GameObject text1 = GameObject.FindWithTag("currentstamina");
        textCurrStamina = text1.GetComponent<Text>();
        GameObject text2 = GameObject.FindWithTag("maxstamina");
        textMaxStamina = text2.GetComponent<Text>();
        
        int staminaAsInt = Convert.ToInt32(stamina);
        textCurrStamina.text = staminaAsInt.ToString();
        textMaxStamina.text = "/"+maxStamina.ToString();
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
