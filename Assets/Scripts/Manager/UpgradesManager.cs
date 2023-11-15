using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesManager : MonoBehaviour
{
    [SerializeField]
    private GameManager _gameManager;

    [SerializeField]
    private Text _upgradeHealthCost;
    [SerializeField]
    private Text _upgradeFireRateCost;
    [SerializeField]
    private Text _upgradeBulletDmgCost;

    [SerializeField]
    private Button _upgradeHealthBtn;
    [SerializeField]
    private Button _upgradeFireRateBtn;
    [SerializeField]
    private Button _upgradeBulletDmgBtn;


    private void Start()
    {
        _upgradeHealthBtn.onClick.AddListener(OnUpgradeHealthButtonPressed);
        _upgradeFireRateBtn.onClick.AddListener(OnUpgradeFireRateButtonPressed);
        _upgradeBulletDmgBtn.onClick.AddListener(OnUpgradeBulletDmgButtonPressed);
        SetupUpgradeView();
    }

    private void SetupUpgradeInfo(AbilityType type) 
    {
        switch (type) 
        {
            case AbilityType.MaxHP:
           
                if (_gameManager.IsMaxLevel(AbilityType.MaxHP))
                    _upgradeHealthCost.text = "MAX LEVEL";
                else
                    _upgradeHealthCost.text = "x" + _gameManager.GetUpgradeCost(AbilityType.MaxHP).ToString();
           
                break;

            case AbilityType.FireRate:

                if (_gameManager.IsMaxLevel(AbilityType.FireRate))
                    _upgradeFireRateCost.text = "MAX LEVEL";
                else
                    _upgradeFireRateCost.text = "x" + _gameManager.GetUpgradeCost(AbilityType.FireRate).ToString();
                
                break;

            case AbilityType.Damage:

                if (_gameManager.IsMaxLevel(AbilityType.Damage))
                    _upgradeBulletDmgCost.text = "MAX LEVEL";
                else
                    _upgradeBulletDmgCost.text = "x" + _gameManager.GetUpgradeCost(AbilityType.Damage).ToString();

                break;
        }
    }

    private void SetupUpgradeView() 
    {
        SetupUpgradeInfo(AbilityType.MaxHP);
        SetupUpgradeInfo(AbilityType.FireRate);
        SetupUpgradeInfo(AbilityType.Damage);
    }

    private void OnUpgradeHealthButtonPressed() 
    {
        _gameManager.UpgradeAbility(AbilityType.MaxHP);
        SetupUpgradeInfo(AbilityType.MaxHP);
    }

    private void OnUpgradeFireRateButtonPressed()
    {
        _gameManager.UpgradeAbility(AbilityType.FireRate);
        SetupUpgradeInfo(AbilityType.FireRate);
    }

    private void OnUpgradeBulletDmgButtonPressed()
    {
        _gameManager.UpgradeAbility(AbilityType.Damage);
        SetupUpgradeInfo(AbilityType.Damage);
    }
}
