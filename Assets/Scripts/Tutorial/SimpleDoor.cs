using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDoor : InteractableObjects
{
    bool _hasYellowKey;

    public bool HasKey { get => _hasYellowKey;  set { _hasYellowKey = value; } }

    protected override void Start()
    {
        base.Start();
    }

    public override void Interact()
    {
        if (_hasYellowKey)
        {
            //Change level
            Debug.Log("TUTORIAL COMPLETADO");
            LevelLoader.instance.MainMenu();
        }
        else
        {
            Debug.Log("NO TIENE LLAVE");
        }
    }
}
