using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables:ScriptableObject
{
    public static bool wonGame = false;
    public static bool isPaused = false;

    //File Stuff
    public static bool newGame;

    //Game state Stuff
    public static bool inRun = false;
    public static int playerHealth;
    public static int playerStamina;
    public static string playerClass;
    public static int floorNum;
    public static string floorSeed;
    
}