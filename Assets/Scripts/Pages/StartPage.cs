using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartPage : MonoBehaviour
{
    public CanvasGroup UICanvasGroup;
    public MainUIManager mainUIManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickStartButton()
    {
        UICanvasGroup.DOFade(0, 2f).onComplete += () => {
            UICanvasGroup.gameObject.SetActive(false);
            mainUIManager.OpenMainHUD();
        };
    }
}
