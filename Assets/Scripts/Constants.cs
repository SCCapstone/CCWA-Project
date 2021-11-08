using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Holds all global constants needed to run the game
public class Constants : ScriptableObject
{ 
    public const int MAX_HEALTH = 100;
    public const int MAX_STAMINA = 100;
    public const int MAX_FLOOR_NUM = 10;
    public static readonly string[] PLAYER_CLASSES = {"warrior","mage","rogue"};
    public const string SAVE_DIRECTORY = "./saves/";
    public const string SAVE_FILE_BASE_NAME = "savefile";
}