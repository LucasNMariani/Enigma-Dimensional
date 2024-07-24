using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bell : InteractableObjects
{
    [SerializeField]
    AudioClip _keySound;


    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void Interact()
    {
        AudioManager.instance.PlaySound(_keySound);
        EventManager.Trigger("BellRing");
    }
}
