
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("UI Sliders")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [Header("Exposed Parameters")]
    [SerializeField] private string musicParameter = "MusicVolume";
    [SerializeField] private string sfxParameter = "SFXVolume";

    private const string MUSIC_KEY = "MusicVolume";
    private const string SFX_KEY = "SFXVolume";

    private void Start()
    {
        musicSlider.minValue = 0f;
        musicSlider.maxValue = 1f;

        sfxSlider.minValue = 0f;
        sfxSlider.maxValue = 1f;

        musicSlider.value = PlayerPrefs.GetFloat(MUSIC_KEY, 0.75f);
        sfxSlider.value = PlayerPrefs.GetFloat(SFX_KEY, 0.85f);

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        SetMusicVolume(musicSlider.value);
        SetSFXVolume(sfxSlider.value);
    }

    private void SetMusicVolume(float volume)
    {
        float db = volume <= 0.0001f ? -80f : Mathf.Log10(volume) * 20f;

        audioMixer.SetFloat(musicParameter, db);
        PlayerPrefs.SetFloat(MUSIC_KEY, volume);
    }

    private void SetSFXVolume(float volume)
    {
        float db = volume <= 0.0001f ? -80f : Mathf.Log10(volume) * 20f;

        audioMixer.SetFloat(sfxParameter, db);
        PlayerPrefs.SetFloat(SFX_KEY, volume);
    }

    private void OnDestroy()
    {
        if (musicSlider != null)
            musicSlider.onValueChanged.RemoveListener(SetMusicVolume);

        if (sfxSlider != null)
            sfxSlider.onValueChanged.RemoveListener(SetSFXVolume);

        PlayerPrefs.Save();
    }
}
