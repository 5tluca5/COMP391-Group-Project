using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseSettingPage : MonoBehaviour
{
    [SerializeField]
    private Button _closeBtn;
    [SerializeField]
    private Button _musicBtn;
    [SerializeField]
    private Button _effectBtn;

    [SerializeField]
    private AudioSource musicSource;

    [SerializeField]
    private AudioSource effectsSource;

    // Start is called before the first frame update
    void Start()
    {
        _closeBtn.onClick.AddListener(ClosePage);

        _musicBtn.onClick.AddListener(ToggleMusic);

        _effectBtn.onClick.AddListener(ToggleSFX);
    }

    private void ClosePage() 
    {
        gameObject.SetActive(false);
    }
    private void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
    private void ToggleSFX()
    {

        effectsSource.mute = !effectsSource.mute;
    }
}
