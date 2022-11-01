using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsControls : MonoBehaviour
{
    [SerializeField] private Dropdown resolutionDropdown;
    [SerializeField] private Toggle windowedOrFullscreen;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioMixer musicMixer;
    [SerializeField] private AudioMixer volumeMixer;

    private bool fullscreenMode = false;

    private float diffiucultyModifier;
    [SerializeField] private Toggle difficultyEasy;
    [SerializeField] private Toggle difficultyMedium;
    [SerializeField] private Toggle difficultyHard;

    private void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 0.75f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        diffiucultyModifier = PlayerPrefs.GetFloat("DiffucltyMod", 1.0f);
    }
    #region Graphic Controls
    //graphic controls
    public void OnChangeResolution()
    {
        switch (resolutionDropdown.value)
        {
            case 0:
                Screen.SetResolution(1920, 1080, fullscreenMode);
                break;

            case 1:
                Screen.SetResolution(2560, 1440, fullscreenMode);
                break;

            case 2:
                Screen.SetResolution(3840, 2160, fullscreenMode);
                break;
        }
    }

    public void OnFullscreenToggle()
    {
        if (windowedOrFullscreen.isOn)
        {
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
        }
    }
    #endregion Graphic Controls

    #region Sound Controls
    //sound controls, need to make it so bar is 0 if all the way down
    public void SetVolumeLevel(float sliderValue)
    {
        volumeMixer.SetFloat("Volume", Mathf.Log10(sliderValue) * 20);

        if (sliderValue == 0)
        {
            volumeMixer.SetFloat("Volume", -80);
        }

        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }

    public void SetMusicLevel(float sliderValue)
    {
        musicMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);

        if (sliderValue == 0)
        {
            musicMixer.SetFloat("MusicVolume", -80);
        }

        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    #endregion Sound Controls

    #region Difficulty Controls
    public void SetDifficulty(float ammount)
    {
        switch (ammount)
        {
            case 0.5f:

                if (difficultyEasy.isOn)
                {
                    difficultyMedium.isOn = false;
                    difficultyHard.isOn = false;
                    diffiucultyModifier = ammount;
                    PlayerPrefs.SetFloat("DiffucltyMod", diffiucultyModifier);
                    UpdateExistingEnemies();
                }
                break;
            case 1.0f:

                if (difficultyMedium.isOn)
                {
                    difficultyEasy.isOn = false;
                    difficultyHard.isOn = false;
                    diffiucultyModifier = ammount;
                    PlayerPrefs.SetFloat("DiffucltyMod", diffiucultyModifier);
                    UpdateExistingEnemies();
                }
                break;
            case 1.5f:

                if (difficultyHard.isOn)
                {
                    difficultyEasy.isOn = false;
                    difficultyMedium.isOn = false;
                    diffiucultyModifier = ammount;
                    PlayerPrefs.SetFloat("DiffucltyMod", diffiucultyModifier);
                    UpdateExistingEnemies();
                }
                break;

            default:
                diffiucultyModifier = 1.0f;
                break;
        }
    }

    private void UpdateExistingEnemies()
    {
        Enemy[] currentEnemies = FindObjectsOfType<Enemy>();
        foreach (Enemy slime in currentEnemies)
        {
            slime.GetComponent<EnemyStats>().OnDifficultyChange();
        }
    }
    #endregion Difficulty Controls
}
