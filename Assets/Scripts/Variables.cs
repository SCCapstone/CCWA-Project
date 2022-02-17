using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Variables:ScriptableObject
{
    public static bool wonGame = false;
    public static bool isPaused = false;
    public static bool isDead = false;

    //File Stuff
    public static bool newGame;

    //Game state Stuff
    public static bool inRun = false;
    public static int playerHealth;
    public static int playerStamina;
    public static string playerClass;
    public static int floorNum;
    public static string floorSeed;

    public static string characterType;

    public static Room currentRoom;

    public static Floor currFloor;
    
    //Stack of strings for back button navigation in Scene Controller file
    public static Stack<string> menuNavStack = new Stack<string>();
}