using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilarButton : Button
{
    [SerializeField]
    LampPuzzle _lp;
    [SerializeField]
    LampTurnOn _lamp;

    bool _isLampPlaced;

    public enum LampID
    {
        Lamp1,
        Lamp2,
        Lamp3,
        Lamp4,
        Lamp5
    }
    [SerializeField] LampID _thisLampID;


    protected override void Start()
    {
        base.Start();
    }

    public override void Interact()
    {
        base.Interact();
        _aSource.Play();
        if (!_isLampPlaced)
        {
            if (GameManager.instance.RemoveLamp())
            {
                _isLampPlaced = true;
                _lamp.gameObject.SetActive(true);
            }
        }
        else
        {
            _lp.ActivateLamp((int)_thisLampID);
        }
    }
}
