using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class PlayerStatsHUD : MonoBehaviour
{
    [SerializeField]
    private GameManager _gameManager;

    [SerializeField]
    private Text _currencyTxt;


    [SerializeField]
    private Image[] _hearts;
    [SerializeField]
    private Sprite[] _heartSprites;


    private int _playerCurrentHealth;
    private int _playerMaxHealth;
    private int _currency;
    private MainCharacter _player;

    private void Awake()
    {
        _player = _gameManager.GetPlayer();
        GetStats();
    }

    private void Start()
    {
        SetupView();
    }

    private void GetStats() 
    {
        _gameManager.SubscribeAbilityLevel(AbilityType.MaxHP).Subscribe(x =>
        {
            _playerMaxHealth = (int)(GameConstant.Initial_HP + _gameManager.GetAbility(AbilityType.MaxHP).GetCurrentEffect());
        }).AddTo(this);
        _player.SubscribeCurrentHP().Subscribe( x => {
            _playerCurrentHealth = x;
            DisplayHearts();
        }).AddTo(this);
        //_playerMaxHealth = _player.GetMaxHP();
        //_playerCurrentHealth = _player.GetCurrentHP();

        _gameManager.SubscribeCurrency().Subscribe(x =>
        {
            _currency = x;
            DisplayCurrency();
        }).AddTo(this);
    }

    public void SetupView() 
    {
        DisplayCurrency();
        DisplayHearts();
    }

    private void DisplayHearts() 
    {
        //To display max hearts,
        for (int i = 0; i < _playerMaxHealth; i++) 
        {
            _hearts[i].gameObject.SetActive(true);
            _hearts[i].sprite = _heartSprites[0];
        }

        //Debug.Log("Current Health: " + _playerCurrentHealth);
        //for (int i = 1; i <= _playerCurrentHealth; i++)
        //{
        //    Debug.Log("After Setup");
        //    _hearts[i-1].sprite = _heartSprites[2];
        //}
    }

    private void DisplayCurrency() 
    {
        _currencyTxt.text = "x " + _currency.ToString();
    }

    public void OnPlayerGetHit() 
    {
        _player.SubscribeCurrentHP().Subscribe(x =>
        {
            _playerCurrentHealth = x;
            DisplayHearts();
        }).AddTo(this);
    }
}
