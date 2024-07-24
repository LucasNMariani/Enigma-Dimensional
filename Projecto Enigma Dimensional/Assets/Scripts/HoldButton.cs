using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldButton : Button
{
    [SerializeField]
    float _holdTime;
    [SerializeField]
    float _currentHoldedTime;
    public override void Interact()
    {
        SetButtonInteracted();
        _aSource.Play();
    }

    protected override void Update()
    {
        if(_isPressed == true)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                ResetButton();
                _currentHoldedTime = 0;
            }
            else
            {
                if(_currentHoldedTime >= _holdTime)
                {
                    ActivateObjects();
                }
                else
                {
                    _currentHoldedTime += Time.deltaTime;
                }
            }
        }
    }

    
}
