using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainHUD;
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
        _mainHUD.SetActive(true);
    }

    public void OpenUpgradeUI() 
    {
        _upgradeUI.SetActive(true);
    }

    public void CloseMainHUD() 
    {
        _mainHUD.SetActive(false);
    }

    public void CloseUpgradeUI() 
    {
        _upgradeUI.SetActive(false);
    }

    public void OnClickBackButton() 
    {
        Time.timeScale = 1;
        _mainHUD.SetActive(true);
        _upgradeUI.SetActive(false);
    }
}
