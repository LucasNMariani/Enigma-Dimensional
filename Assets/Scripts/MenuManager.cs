using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    MenuAudio _ma;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame(string startLevel)
    {
        if(OptionsBetweenScenes.instance != null) OptionsBetweenScenes.instance.SaveAudioConfiguration(_ma.GetSoundSliderValue(), _ma.GetMusicSliderValue());
        SceneManager.LoadScene(startLevel);
    }

    public void Exit()
    {
        Application.Quit();
    }



}
