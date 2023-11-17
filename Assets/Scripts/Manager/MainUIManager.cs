using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour
{
    [SerializeField]
    private PlayerStatsHUD _playerHUD;
    [SerializeField]
    private CountdownTimer _countdownTimer;
    [SerializeField]
    private GameObject _upgradeUI;
    [SerializeField]
    private Button _backBtn;

    public void Start()
    {
        _backBtn.onClick.AddListener(OnClickBackButton);
    }

    public void OpenMainHUD() 
    {
        Debug.Log("click");
        _playerHUD.gameObject.SetActive(true);
        _playerHUD.SetupView();
    }

    public void OpenUpgradeUI() 
    {
        _upgradeUI.SetActive(true);
    }

    public void CloseMainHUD() 
    {
        _playerHUD.gameObject.SetActive(false);
    }

    public void CloseUpgradeUI() 
    {
        _upgradeUI.SetActive(false);
    }

    public void OnClickBackButton() 
    {
        Time.timeScale = 1;
        _playerHUD.gameObject.SetActive(true);
        _upgradeUI.SetActive(false);
    }

    public void ShowTimer()
    {
        _countdownTimer.gameObject.SetActive(true);
    }

    public void OpenPauseMenu() 
    {
    
    }

    public void ClosePauseMenu() 
    {
    
    }
}
