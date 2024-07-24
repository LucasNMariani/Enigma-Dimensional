using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] int _objectiveToActivate;
    [SerializeField] float _timeToEnd;
    float _currentTime;
    bool _isActive;

    private void Start()
    {
        _currentTime = _timeToEnd;
    }

    private void Update()
    {
        if(_isActive == true && UIManager.instance.InGameMenuPanel.activeSelf == false)
        {
            if(_currentTime > 0)
            {
                _currentTime -= Time.deltaTime;
                UIManager.instance.UpdateTimer((int)_currentTime);
            }
            else
            {
                EventManager.Trigger("PlayerLose");
            }
        }
    }

    public void CheckObjective(int objectiveLevel)
    {
        if(_objectiveToActivate == objectiveLevel)
        {
            _isActive = true;
            UIManager.instance.OpenTimerPanel();
        }
    }
}
