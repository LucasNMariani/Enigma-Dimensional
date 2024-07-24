using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampPuzzle : MonoBehaviour
{
    [SerializeField]
    LampTurnOn[] _lamps;
    bool _allLampsTurnedOn;
    bool _allLampsPlaced;


    public void ActivateLamp(int lampID)
    {
        if (!_allLampsPlaced)
        {
            for (int i = 0; i < _lamps.Length; i++)
            {
                if (_lamps[i].gameObject.activeSelf)
                {
                    _allLampsPlaced = true;
                    
                }
                else
                {
                    _allLampsPlaced = false;
                    break;
                }
            }
            if (_allLampsPlaced)
            {
                EventManager.Trigger("NextObjective");
            }
        }

        if (_allLampsPlaced)
        {
            _lamps[lampID].SetLampState(!_lamps[lampID].GetLampState());
            ActivateNextLamp(lampID + 1);
            ActivatePreviousLamp(lampID - 1);
            CheckIfAllLampsTurnedOn();
        }
    }

    private void ActivateNextLamp(int lampID)
    {
        if(lampID >= _lamps.Length)
        {
            lampID = 0;
        }
        _lamps[lampID].SetLampState(!_lamps[lampID].GetLampState());
    }

    private void ActivatePreviousLamp(int lampID)
    {
        if (lampID < 0)
        {
            lampID = _lamps.Length-1;
        }
        _lamps[lampID].SetLampState(!_lamps[lampID].GetLampState());
    }

    private void CheckIfAllLampsTurnedOn()
    {
        for (int i = 0; i < _lamps.Length; i++)
        {
            if (!_lamps[i].GetLampState())
            {
                _allLampsTurnedOn = false;
                break;
            }
            else
            {
                _allLampsTurnedOn = true;
                EventManager.Trigger("NextObjective");
            }
        }
        if (_allLampsTurnedOn)
        {
            EventManager.Trigger("NextObjective");
        }
    }

}
