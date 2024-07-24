using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaButton : Button
{
    [SerializeField]
    CheckButtonOrder.letterCode _letter;
    [SerializeField]
    CheckButtonOrder _buttonChecker;

    public override void Interact()
    {
        _buttonChecker.CheckLetter(_letter);
        SetButtonInteracted();        
        _aSource.Play();
        
    }
}
