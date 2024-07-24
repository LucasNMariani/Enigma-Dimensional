using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    [SerializeField]
    int[] _levelObjectiveNeededPerCheckpoint;
    int _currentLevelObjective;
    int _currentCheckPoint;

    private void Awake()
    {
        EventManager.Subscribe("UpdateLevelObjective", UpdateLevelObjective);
    }

    private void Start()
    {
        StartCoroutine("xd");
    }

    IEnumerator xd()
    {
        yield return new WaitForSeconds(0.01f);
        LoadLevelConfiguration();
    }

    private void UpdateLevelObjective(params object[] parameters)
    {
        _currentLevelObjective = (int)parameters[0];
        CheckIfCheckPointReached();
    }

    private void CheckIfCheckPointReached()
    {
        if(_levelObjectiveNeededPerCheckpoint.Length != 0)
        {
            int tempChecker = 0;
            for (int i = 0; i < _levelObjectiveNeededPerCheckpoint.Length; i++)
            {
                if(_currentLevelObjective >= _levelObjectiveNeededPerCheckpoint[i])
                {
                    tempChecker = i+1;
                }
            }
            if (tempChecker > 0)
            {
                _currentCheckPoint = tempChecker;
                LevelLoader.instance.UpdateCheckPoint(_currentCheckPoint);
            }
        }
    }

    private void LoadLevelConfiguration()
    {
        if(OptionsBetweenScenes.instance != null) _currentCheckPoint = OptionsBetweenScenes.instance.LevelCheckPoint;
        if(_currentCheckPoint > 0)
        {
            EventManager.Trigger("LoadLevelByCheckPoint", _currentCheckPoint, _levelObjectiveNeededPerCheckpoint[_currentCheckPoint-1]);
        }
    }
}
