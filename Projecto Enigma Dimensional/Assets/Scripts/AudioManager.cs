using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioSource _asSounds;
    [SerializeField] AudioSource _asMusic;
    [SerializeField] AudioMixer _soundMaster;
    [SerializeField] AudioMixer _musicMaster;
    [SerializeField] AudioMixerGroup[] _musicChanges;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        if(OptionsBetweenScenes.instance != null) SetStartingVolumes();
    }
    public void SetStartingVolumes()
    {
        ChangeSoundVolume(OptionsBetweenScenes.instance.SoundVolume);
        ChangeMusicVolume(OptionsBetweenScenes.instance.MusicVolume);
    }

    public void PlaySound(AudioClip soundToPlay)
    {
        _asSounds.clip = soundToPlay;
        _asSounds.Play();
    }

    public void ChangeMusicMixer(bool habilityState)
    {
        if(habilityState == true)
        {
            _asMusic.outputAudioMixerGroup = _musicChanges[1];
        }
        else
        {
            _asMusic.outputAudioMixerGroup = _musicChanges[0];
        }
    }

    public void ChangeSoundVolume(float value)
    {
        _soundMaster.SetFloat("SoundVol", Mathf.Log10(value) * 20);
    }

    public void ChangeMusicVolume(float value)
    {
        _musicMaster.SetFloat("MusicVol", Mathf.Log10(value) * 20);        
    }
}
