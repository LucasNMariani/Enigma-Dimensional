using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckButtonOrder : MonoBehaviour
{
    public enum letterCode
    {
        A,
        B,
        C,
        D,
        E,
        F
    }
    [SerializeField]
    letterCode[] _lettersToActivate;
    int _currentLetterToPress;

    public void CheckLetter(letterCode letter)
    {
        if(letter == _lettersToActivate[_currentLetterToPress])
        {
            if(_currentLetterToPress < _lettersToActivate.Length-1)
            {
                _currentLetterToPress++;
            }
            else
            {
                InteractWithObjects();
            }
        }
        else
        {
            _currentLetterToPress = 0;
        }
    }

    private void InteractWithObjects()
    {
        EventManager.Trigger("OpenSlidingDoors");
    }


}
