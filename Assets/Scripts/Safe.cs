using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Safe : MonoBehaviour
{
    [SerializeField] KnobSlider _ksHours;
    [SerializeField] KnobSlider _ksMinutes;
    [SerializeField] int[] _hours;
    [SerializeField] int[] _minutes;
    [SerializeField] int _currentCode;
    [SerializeField] Animator _safeAnimator;
    [SerializeField] AudioSource _safeAudioSource;
    [SerializeField] AudioClip _unlockingSafeSound;
    int _currentHour;
    int _currentMinute;
    [SerializeField]
    Image[] _lockedBars;

    private void Start()
    {
        //GameManager.instance.NextObjective();
        _currentMinute = 0;
        _currentHour = 0;
    }
    public void UpdateMinuteValue(int value)
    {
        _currentMinute = value;
        CheckCurrentCode();
    }

    public void UpdateHourValue(int value)
    {
        _currentHour = value;
        CheckCurrentCode();
    }


    private void CheckCurrentCode()
    {
        if (_minutes[_currentCode] == _currentMinute && _hours[_currentCode] == _currentHour)
        {
            _lockedBars[_currentCode].fillAmount = 1;
            _currentCode += 1;
            _safeAudioSource.Play();
            
            if(_currentCode == _hours.Length)
            {
                _safeAnimator.SetBool("OpenSafe", true);
                UIManager.instance.ClosePopUp();
                //GameManager.instance.NextObjective();
                _safeAudioSource.clip = _unlockingSafeSound;
                _safeAudioSource.Play();
            }            
        }
    }
}
