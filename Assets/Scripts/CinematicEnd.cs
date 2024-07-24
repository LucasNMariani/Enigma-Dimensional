using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicEnd : MonoBehaviour
{
    [SerializeField]
    PlayableDirector _pd;
    bool _hasEnded;
    [SerializeField]
    GameObject _player;
    [SerializeField]
    GameObject _playerSkin;
    [SerializeField]
    GameObject _cinematicCamera;
    [SerializeField]
    GameObject _canvas;

    private void Awake()
    {
        if (OptionsBetweenScenes.instance.LevelCheckPoint == 0)
        {
            _pd.Play();
        }
        else
        {
            StartGame();
        }
    }

    private void Update()
    {
        if (!_hasEnded)
        {
            if(_pd.state == PlayState.Paused)
            {
                StartGame();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _pd.Stop();
                StartGame();
            }            
        }        
    }

    private void StartGame()
    {
        _playerSkin.SetActive(false);
        _hasEnded = true;
        _player.SetActive(true);
        _cinematicCamera.SetActive(false);
        _canvas.SetActive(true);
    }


}
