using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampGrab : InteractableObjects
{
    

    protected override void Start()
    {
        base.Start();
    }

    public override void Interact()
    {
        EventManager.Trigger("GiveLamp");
        Destroy(this.gameObject);
    }
}
