using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class StartPage : MonoBehaviour
{
    public CanvasGroup UICanvasGroup;
    public MainUIManager mainUIManager;
    public AudioClip menuClip;
    public AudioClip bgClip;
    public Button loadGameButton;

    bool transiting = false;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayMusic(menuClip);
        loadGameButton.interactable = GameManager.Instance.CanLoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickStartButton()
    {
        if (transiting) return;

        GameManager.Instance.NewGame();
        StartGame();
    }

    public void OnClickLoadButton()
    {
        if (transiting) return;

        GameManager.Instance.LoadGame();
        StartGame();
    }

    void StartGame()
    {
        transiting = true;

        AudioManager.Instance.StopMusic();
        UICanvasGroup.DOFade(0, 1f).onComplete += () => {

            AudioManager.Instance.PlayMusic(bgClip);
            UICanvasGroup.gameObject.SetActive(false);
            mainUIManager.OpenMainHUD();
            GameManager.Instance.StartGame();
        };
    }
}
