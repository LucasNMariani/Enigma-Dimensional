using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentObject : InteractableObjects
{
    bool _hasScrewdriver;

    protected override void Start()
    {
        base.Start();
        EventManager.Subscribe("GiveScrewdriver", GiveScrewdriver);
    }


    public override void Interact()
    {
        if (_hasScrewdriver == true)
        {
            //_am.PlaySound(_grabSound);

            if (_isObjectiveType == true)
            {
                ObjectiveCompleted();
            }
            Destroy(this.gameObject);
        }
    }

    private void GiveScrewdriver(params object[] parameters)
    {
        _hasScrewdriver = true;
    }
}
