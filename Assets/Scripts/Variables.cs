using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Variables:ScriptableObject
{
    public static bool wonGame = false;
    public static bool isPaused = false;
    public static bool isDead = false;
    public static bool isInvincible = false;

    //File Stuff
    public static bool newGame;
    public static float fastest_E = 359999;
    public static float fastest_M = 359999;
    public static float fastest_H = 359999;

    //Game state Stuff
    public static bool inRun = false;
    public static int playerHealth;
    public static int playerStamina;
    public static string playerClass;
    public static int floorNum;
    public static string floorSeed;

    public static string characterType;
    public static Color skinColor = Color.clear;

    public static Room currentRoom;

    public static Floor currFloor;

    public static float clock;
    public static bool isSpeedrun;

    public static int difficulty;
    public static int score;

    public static bool[] achievementTriggers = new bool[5];
    
    //Stack of strings for back button navigation in Scene Controller file
    public static Stack<string> menuNavStack = new Stack<string>();
}