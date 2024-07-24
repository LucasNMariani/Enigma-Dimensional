using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screwdriver : InteractableObjects
{
    [SerializeField] AudioClip _grabSound;
    
    public override void Interact()
    {
        EventManager.Trigger("SetScrewdriver", true);

        if (_isObjectiveType == true)
        {
            ObjectiveCompleted();
        }
        Destroy(this.gameObject);
        
    }
}
