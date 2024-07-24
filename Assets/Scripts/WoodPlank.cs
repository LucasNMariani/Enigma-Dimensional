using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodPlank : InteractableObjects
{
    bool _hasCrowbar;

    protected override void Start()
    {
        base.Start();
        EventManager.Subscribe("SetCrowbar", SetCrowbar);
    }

    public override void Interact()
    {
        if (_hasCrowbar == true)
        {
            //_am.PlaySound(_grabSound);
            
            Destroy(this.gameObject);
        }
    }

    protected override void LoadCheckPointConfiguration()
    {
        Destroy(this.gameObject);
    }

    private void SetCrowbar(params object[] parameters)
    {
        _hasCrowbar = (bool) parameters[0];
    }
}
