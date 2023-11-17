using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstant
{
    //PlayerPrefs Keys
    public const string LAST_SAVE_TIME_KEY = "LAST_SAVE_TIME_KEY";
    public const string ABILITY_LEVEL_KEY = "ABILITY_{0}_LEVEL";
    public const string CURRENCY_KEY = "CURRENCY_KEY";

    //Initial values
    public const float Initial_HP = 5f;
    public const float Initial_FireRate = 0.25f;
    public const float Initial_Damage = 1;
    public const float Zombie_HP = 4;
    public const float Zombie_Damage = 1;

    public const float Wave_Timer = 180;

    //Resources
    public const int Drop_Resource_Max = 3;
    public static readonly int[] Drop_Resource_Ratio = { 1, 2, 99 };
}
