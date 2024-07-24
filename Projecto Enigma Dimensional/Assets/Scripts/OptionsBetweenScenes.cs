using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsBetweenScenes : MonoBehaviour
{
    public static OptionsBetweenScenes instance;

    string _previousScene;
    [SerializeField]
    int _levelCheckPoint;

    [SerializeField]
    float _soundVolume, _musicVolume;

    public float SoundVolume { get => _soundVolume;}
    public float MusicVolume { get => _musicVolume;}
    public string PreviousScene { get => _previousScene; set => _previousScene = value; }
    public int LevelCheckPoint { get => _levelCheckPoint; set => _levelCheckPoint = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        if (instance == this) return;
        Destroy(gameObject);
    }

    public void SaveAudioConfiguration(float soundVolume, float musicVolume)
    {
        _soundVolume = soundVolume;
        _musicVolume = musicVolume;
    }    


}
