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
    public const float Initial_HP = 10f;
    public const float Initial_FireRate = 0.25f;
    public const float Initial_Damage = 1;
    public const float Zombie_HP = 4;
    public const float Zombie_Damage = 1;
    public const float Boss_HP = 50000;
    public const float Boss_Damage = 3;

    public const float Wave_Timer = 18;

    //Resources
    public const int Drop_Resource_Max = 3;
    public static readonly int[] Drop_Resource_Ratio = { 1, 2, 99 };
}
