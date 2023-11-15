using System;
<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        //SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }


    // Reference
    public MainCharacter player;

    Dictionary<AbilityType, Ability> abilities = new Dictionary<AbilityType, Ability>();
    int currency = 100;

    private void Start()
    {
        InvokeRepeating("AutoSave", 5f, 5f);

        NewGame();
        player.SetFireRate(GetFireRate());
    }

    public void NewGame()
    {
        abilities.Clear();

        abilities.Add(AbilityType.MaxHP, new AbilityHeath(0));
        abilities.Add(AbilityType.FireRate, new AbilityFireRate(0));
        abilities.Add(AbilityType.Damage, new AbilityDamage(0));
    }

    public void LoadGame()
    {
        abilities.Clear();

        int hpLevel = PlayerPrefs.GetInt(string.Format(GameConstant.ABILITY_LEVEL_KEY, AbilityType.MaxHP.ToString()), 0);
        int frLevel = PlayerPrefs.GetInt(string.Format(GameConstant.ABILITY_LEVEL_KEY, AbilityType.FireRate.ToString()), 0);
        int dmgLevel = PlayerPrefs.GetInt(string.Format(GameConstant.ABILITY_LEVEL_KEY, AbilityType.Damage.ToString()), 0);

        abilities.Add(AbilityType.MaxHP, new AbilityHeath(hpLevel));
        abilities.Add(AbilityType.FireRate, new AbilityFireRate(frLevel));
        abilities.Add(AbilityType.Damage, new AbilityDamage(dmgLevel));

        currency = PlayerPrefs.GetInt(GameConstant.CURRENCY_KEY, 0);
    }

    void AutoSave()
    {
        foreach(var kvp in abilities)
        {
            PlayerPrefs.SetInt(string.Format(GameConstant.ABILITY_LEVEL_KEY, kvp.Key.ToString()), kvp.Value.GetLevel());
        }

        PlayerPrefs.SetInt(GameConstant.CURRENCY_KEY, currency);
    }

    public bool UpgradeAbility(AbilityType type)
    {
        var cost = abilities[type].GetUpgradeCost();

        if (currency < cost) return false;

        if (abilities[type].IsMaxLevel()) return false;


        if (abilities[type].LevelUp())
        {
            currency -= cost;
        }

        return true;
    }

    public float GetUpgradeCost(AbilityType type)
    {
        return abilities[type].GetUpgradeCost();
    }

    public float GetFireRate()
    {
        return Mathf.Max(0.05f, GameConstant.Initial_FireRate - abilities[AbilityType.FireRate].GetCurrentEffect());
    }

    public float GetBulletDamage()
    {
        return GameConstant.Initial_Damage + abilities[AbilityType.Damage].GetCurrentEffect();
    }

    public bool IsMaxLevel(AbilityType type) 
    {
        return abilities[type].IsMaxLevel();
    }

    public MainCharacter GetPlayer() 
    {
        return player;
    }

    public int GetCurrency() 
    {
        return currency;
    }
}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        //SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }

    // Reference
    public MainCharacter player;

    // Game start / over
    bool isGameStarted = false;

    // Upgrade
    int currency = 0;
    Dictionary<AbilityType, Ability> abilities = new Dictionary<AbilityType, Ability>();

    private void Start()
    {
        
    }

    public void StartGame()
    {
        if (isGameStarted) return;

        isGameStarted = true;

        SetupPlayer();

        InvokeRepeating("AutoSave", 10f, 10f);
    }

    public bool IsGameStarted()
    {
        return isGameStarted;
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();

        currency = 0;

        abilities.Clear();

        abilities.Add(AbilityType.MaxHP, new AbilityHeath(0));
        abilities.Add(AbilityType.FireRate, new AbilityFireRate(0));
        abilities.Add(AbilityType.Damage, new AbilityDamage(0));
    }

    public void LoadGame()
    {
        currency = PlayerPrefs.GetInt(GameConstant.CURRENCY_KEY, 0);

        abilities.Clear();

        int hpLevel = PlayerPrefs.GetInt(string.Format(GameConstant.ABILITY_LEVEL_KEY, AbilityType.MaxHP.ToString()), 0);
        int frLevel = PlayerPrefs.GetInt(string.Format(GameConstant.ABILITY_LEVEL_KEY, AbilityType.FireRate.ToString()), 0);
        int dmgLevel = PlayerPrefs.GetInt(string.Format(GameConstant.ABILITY_LEVEL_KEY, AbilityType.Damage.ToString()), 0);

        abilities.Add(AbilityType.MaxHP, new AbilityHeath(hpLevel));
        abilities.Add(AbilityType.FireRate, new AbilityFireRate(frLevel));
        abilities.Add(AbilityType.Damage, new AbilityDamage(dmgLevel));
    }

    public bool CanLoadGame()
    {
        return PlayerPrefs.HasKey(GameConstant.LAST_SAVE_TIME_KEY);
    }

    void AutoSave()
    {
        foreach(var kvp in abilities)
        {
            PlayerPrefs.SetInt(string.Format(GameConstant.ABILITY_LEVEL_KEY, kvp.Key.ToString()), kvp.Value.GetLevel());
        }

        PlayerPrefs.SetInt(GameConstant.CURRENCY_KEY, currency);

        PlayerPrefs.SetString(GameConstant.LAST_SAVE_TIME_KEY, DateTime.UtcNow.ToString());
    }

    public void SetupPlayer()
    {
        player.SetFireRate(GetFireRate());
    }

    public bool UpgradeAbility(AbilityType type)
    {
        var cost = abilities[type].GetUpgradeCost();

        if (currency < cost) return false;

        if (abilities[type].IsMaxLevel()) return false;


        if (abilities[type].LevelUp())
        {
            currency -= cost;
        }

        AutoSave();

        return true;
    }

    public float GetUpgradeCost(AbilityType type)
    {
        return abilities[type].GetUpgradeCost();
    }

    public float GetFireRate()
    {
        return Mathf.Max(0.05f, GameConstant.Initial_FireRate - abilities[AbilityType.FireRate].GetCurrentEffect());
    }

    public float GetBulletDamage()
    {
        return GameConstant.Initial_Damage + abilities[AbilityType.Damage].GetCurrentEffect();
    }
}
>>>>>>> 3eef2c578976937bed091b5f74b29c4468d9de66
