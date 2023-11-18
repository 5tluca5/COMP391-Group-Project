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
    private GameObject _bossHealthGO;

    [SerializeField]
    private Text _currencyTxt;

    [SerializeField]
    private Image[] _hearts;
    [SerializeField]
    private Sprite[] _heartSprites;

    private int _playerCurrentHealth;
    private int _playerMaxHealth;
    private int _playerPrevMaxHealth = 0;
    private int _currency;
    private MainCharacter _player;

    private void Awake()
    {
        _player = _gameManager.GetPlayer();
        SetupStats();
    }

    private void Start()
    {
        SetupView();
    }

    private void SetupStats() 
    {
        _gameManager.SubscribeAbilityLevel(AbilityType.MaxHP).Subscribe(x =>
        {
            _playerMaxHealth = (int)(GameConstant.Initial_HP + _gameManager.GetAbility(AbilityType.MaxHP).GetCurrentEffect());
            DisplayHearts();
            //DisplayCurrentHearts();
        }).AddTo(this);

        _gameManager.GetPlayer().SubscribeCurrentHP().Subscribe(x =>
        {
            _playerCurrentHealth = x;
            DisplayHearts();
        }).AddTo(this);

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
        if (_gameManager.IsGameOvered()) 
        {
            return;
        }

        if (_playerPrevMaxHealth == _playerMaxHealth)
        {
            DisplayCurrentHearts();
        }
        else 
        {
            _playerPrevMaxHealth = _playerMaxHealth;
            //To display max hearts,
            for (int i = 0; i < _playerMaxHealth; i++)
            {
                _hearts[i].gameObject.SetActive(true);
                _hearts[i].sprite = _heartSprites[0];
            }
            DisplayCurrentHearts();
        }

    }

    private void DisplayCurrentHearts()
    {
        var index = _playerCurrentHealth;

        for (int i = 0; i < index; i++)
        {
            _hearts[i].sprite = _heartSprites[2];
        }
    }

    private void DisplayCurrency() 
    {
        _currencyTxt.text = "x " + _currency.ToString();
    }

}
