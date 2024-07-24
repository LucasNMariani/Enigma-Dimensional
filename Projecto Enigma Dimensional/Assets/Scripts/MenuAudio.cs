using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuAudio : MonoBehaviour
{
    [SerializeField]
    Slider _soundSlider;
    [SerializeField]
    Slider _musicSlider;

    private void Start()
    {
        LoadAudioConfiguration();
    }

    public float GetSoundSliderValue()
    {
        return _soundSlider.value;
    }

    public float GetMusicSliderValue()
    {
        return _musicSlider.value;
    }

    private void LoadAudioConfiguration()
    {
        if(OptionsBetweenScenes.instance.SoundVolume != 0 && OptionsBetweenScenes.instance.MusicVolume != 0)
        {
            _soundSlider.value = OptionsBetweenScenes.instance.SoundVolume;
            _musicSlider.value = OptionsBetweenScenes.instance.MusicVolume;
        }
        else
        {
            _soundSlider.value = 1;
            _musicSlider.value = 1;
        }
    }
}
