using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Door : InteractableObjects
{
    [SerializeField]
    protected bool _isOpened;

    protected bool IsOpened { get => _isOpened;}
}
