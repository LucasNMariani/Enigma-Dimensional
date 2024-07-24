using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    [SerializeField]
    GameObject _pausePanel;
    [SerializeField]
    GameObject _volumePanel;
    [SerializeField]
    Image[] _inGameMenuLens;
    [SerializeField]
    Slider _soundSlider;
    [SerializeField]
    Slider _musicSlider;

    private void Start()
    {
        if(OptionsBetweenScenes.instance != null) LoadVolumeSliders(
            OptionsBetweenScenes.instance.SoundVolume, 
            OptionsBetweenScenes.instance.MusicVolume);
    }

    public void GoToMainMenu()
    {
        OptionsBetweenScenes.instance.SaveAudioConfiguration(_soundSlider.value, _musicSlider.value);
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ResetLens()
    {
        for (int i = 0; i < _inGameMenuLens.Length; i++)
        {
            _inGameMenuLens[i].enabled = false;
        }
    }

    public void OpenVolumePanel()
    {
        _volumePanel.SetActive(true);
        _pausePanel.SetActive(false);
        ResetLens();
    }

    public void CloseVolumePanel()
    {
        _volumePanel.SetActive(false);
        _pausePanel.SetActive(true);
        ResetLens();
    }

    public void ChangeMusicVolume()
    {
        AudioManager.instance.ChangeMusicVolume(_musicSlider.value);
    }

    public void ChangeSoundVolume()
    {
        AudioManager.instance.ChangeSoundVolume(_soundSlider.value);
    }

    public void LoadVolumeSliders(float soundValue, float musicValue)
    {
        _soundSlider.value = soundValue;
        _musicSlider.value = musicValue;
    }

    public bool IsVolumePanelOpened()
    {
        return _volumePanel.activeSelf;
    }
}
