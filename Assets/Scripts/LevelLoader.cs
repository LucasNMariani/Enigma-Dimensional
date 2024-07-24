using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;
    [SerializeField]
    Animator _transition;
    [SerializeField]
    float transitionTime = 1;
    int _currentCheckPoint;

    // Update is called once per frame

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
        EventManager.Subscribe("PlayerLose", LoadLoseScene);
    }

    public void ReloadScene()
    {
        StartCoroutine(LoadLevelRoutine(SceneManager.GetActiveScene().name));
    }

    public void LoadLoseScene(params object[] parameters)
    {        
        OptionsBetweenScenes.instance.PreviousScene = SceneManager.GetActiveScene().name;
        OptionsBetweenScenes.instance.LevelCheckPoint = _currentCheckPoint;
        StartCoroutine(LoadLevelRoutine("LoseScene"));
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadNextLevel()
    {
        if(OptionsBetweenScenes.instance != null) OptionsBetweenScenes.instance.LevelCheckPoint = 0;
        switch (SceneManager.GetActiveScene().name)
        {
            case "Level1":
                StartCoroutine(LoadLevelRoutine("Level2"));
                break;
            case "Level2":
                StartCoroutine(LoadLevelRoutine("Level3"));
                break;
            case "Level3":
                StartCoroutine(LoadLevelRoutine("EndGameLore"));
                break;
            case "Tutorial":
                StartCoroutine(LoadLevelRoutine("Level1"));
                break;
            default:
                break;
        }
    }

    IEnumerator LoadLevelRoutine(string levelName)
    {
        EventManager.ResetEvents();
        _transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);        
        SceneManager.LoadScene(levelName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        OptionsBetweenScenes.instance.LevelCheckPoint = 0;
        SceneManager.LoadScene("MainMenu");
        EventManager.ResetEvents();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.L) && Input.GetKeyDown(KeyCode.N)) LoadNextLevel();
    }

    public void UpdateCheckPoint(int checkPoint)
    {
        _currentCheckPoint = checkPoint;
    }
}
