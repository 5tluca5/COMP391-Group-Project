using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainHUD;
    [SerializeField]
    private GameObject _upgradeUI;


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
}
