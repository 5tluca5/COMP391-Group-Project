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


    private float _playerCurrentHealth;
    private float _playerMaxHealth;
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
            _playerMaxHealth = GameConstant.Initial_HP + _gameManager.GetAbility(AbilityType.MaxHP).GetCurrentEffect();
            DisplayHearts();
            
        }).AddTo(this);

        //_playerMaxHealth = _player.GetMaxHP();
        _playerCurrentHealth = _player.GetCurrentHP();

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
        // round up if max health is .5
        int roundedUp = (int)Math.Ceiling(_playerMaxHealth);

        //To display max hearts,
        for (int i = 0; i < roundedUp; i++) 
        {
            _hearts[i].gameObject.SetActive(true);
        }
    }

    private void DisplayCurrency() 
    {
        _currencyTxt.text = "x " + _currency.ToString();
    }

    public void OnPlayerHealthUpgrade() 
    {

        _playerMaxHealth = _player.GetMaxHP();
        DisplayHearts();
    }

}
