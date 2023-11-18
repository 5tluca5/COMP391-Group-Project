using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class PauseSettingPage : MonoBehaviour
{
    [SerializeField]
    private Button _closeBtn;
    [SerializeField]
    private Button _musicBtn;
    [SerializeField]
    private Button _effectBtn;
    [SerializeField]
    private Slider _musicSlider;
    [SerializeField]
    private Slider _effectSlider;

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

        _musicSlider.value = musicSource.volume;
        _effectSlider.value = effectsSource.volume;

        _musicSlider.OnValueChangedAsObservable().Subscribe(_ =>
        {
            OnMusicValueChanged(_);
        }).AddTo(this);
        _effectSlider.OnValueChangedAsObservable().Subscribe(_ =>
        {
            OnEffectValueChanged(_);
        }).AddTo(this);
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

    public void OnMusicValueChanged(float value)
    {
        musicSource.volume = value;
    }
    public void OnEffectValueChanged(float value)
    {
        effectsSource.volume = value;
    }
}
