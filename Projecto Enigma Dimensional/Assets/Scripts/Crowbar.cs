using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowbar : InteractableObjects
{
    [SerializeField] AudioClip _grabSound;
    
    public override void Interact()
    {
        EventManager.Trigger("SetCrowbar", true);
        //_am.PlaySound(_grabSound);

        if (_isObjectiveType == true)
        {
            ObjectiveCompleted();
        }
        Destroy(this.gameObject);
    }
}
