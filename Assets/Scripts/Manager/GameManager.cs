using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using UnityEngine.UI;

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
    public EnemyManager enemyManager;
    public MainUIManager mainUIManager;
    public CountdownTimer timer;

    // Boss HP bar (Lucas: Too lazy to create a class hehe)
    public GameObject bossHpGO;
    public Slider bossHpBarSlider;

    // Game start / over
    bool isGameStarted = false;
    bool isGameOver = false;

    // Upgrade
    ReactiveProperty<int> currency = new ReactiveProperty<int>(0);
    Dictionary<AbilityType, Ability> abilities = new Dictionary<AbilityType, Ability>();

    private void Start()
    {
        
    }

    public void InitGame()
    {
        player.Init();
        SetupPlayer();

        enemyManager.SetZombieSpawning(true);

    }

    public void StartGame()
    {
        if (isGameStarted) return;

        isGameStarted = true;

        timer.SubscribeTimeRunOut().Subscribe(x =>
        {
            enemyManager.SetZombieSpawning(false);
            enemyManager.KillAllZombies();

            bossHpGO.SetActive(true);
            bossHpBarSlider.maxValue = GameConstant.Boss_HP;
            bossHpBarSlider.minValue = 0;

            enemyManager.SpawnBoss().SubscribeHP().Subscribe(x =>
            {
                bossHpBarSlider.value = x;

            }).AddTo(this);

            player.EnterBossPhase();
        }).AddTo(this);

        InvokeRepeating("AutoSave", 10f, 10f);
    }

    public bool IsGameStarted()
    {
        return isGameStarted;
    }

    public bool IsGameOvered()
    {
        return isGameOver;
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();

        currency.Value = 100;

        abilities.Clear();

        abilities.Add(AbilityType.MaxHP, new AbilityHeath(0));
        abilities.Add(AbilityType.FireRate, new AbilityFireRate(0));
        abilities.Add(AbilityType.Damage, new AbilityDamage(0));
    }

    public void LoadGame()
    {
        currency.Value = PlayerPrefs.GetInt(GameConstant.CURRENCY_KEY, 0);

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

        PlayerPrefs.SetInt(GameConstant.CURRENCY_KEY, currency.Value);

        PlayerPrefs.SetString(GameConstant.LAST_SAVE_TIME_KEY, DateTime.UtcNow.ToString());
    }

    public void SetupPlayer()
    {
        player.SetFireRate(GetFireRate());
        player.SetCurHP((int)GetMaxHP());
    }

    public bool UpgradeAbility(AbilityType type)
    {
        var cost = abilities[type].GetUpgradeCost();

        if (currency.Value < cost) return false;

        if (abilities[type].IsMaxLevel()) return false;


        if (abilities[type].LevelUp())
        {
            currency.Value -= cost;
        }

        switch (type)
        {
            case AbilityType.MaxHP:
                player.RestoreHP((int)abilities[type].GetEffectPerLevel());
                break;
            case AbilityType.FireRate:
                player.SetFireRate(GetFireRate());
                break;
        }

        AutoSave();

        return true;
    }

    public float GetUpgradeCost(AbilityType type)
    {
        return abilities[type].GetUpgradeCost();
    }

    public Ability GetAbility(AbilityType type)
    {
        return abilities[type];
    }

    public float GetFireRate()
    {
        return Mathf.Max(0.05f, GameConstant.Initial_FireRate - abilities[AbilityType.FireRate].GetCurrentEffect());
    }

    public float GetBulletDamage()
    {
        return GameConstant.Initial_Damage + abilities[AbilityType.Damage].GetCurrentEffect();
    }

    public float GetMaxHP()
    {
        return GameConstant.Initial_HP + abilities[AbilityType.MaxHP].GetCurrentEffect();
    }

    public bool IsMaxLevel(AbilityType type) 
    {
        return abilities[type].IsMaxLevel();
    }

    public MainCharacter GetPlayer() 
    {
        return player;
    }

    public ReactiveProperty<int> SubscribeCurrency() 
    {
        return currency;
    }

    public ReactiveProperty<int> SubscribeAbilityLevel(AbilityType type)
    {
        return abilities[type].SubscribeLevel();
    }

    public void CollectItem(Item item)
    {
        if (item == null) return;

        currency.Value += item.value;
    }
    public void GameOver()
    {
        // show game over page

        isGameOver = true;
        AutoSave();
        mainUIManager.OpenGameOverPage();
    }
}
