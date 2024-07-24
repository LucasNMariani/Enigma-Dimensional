using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evidence : InteractableObjects
{
    [SerializeField] AudioClip _evidenceSound;

    protected override void Start()
    {
        base.Start();
    }

    public override void Interact()
    {
        AudioManager.instance.PlaySound(_evidenceSound);

        if (_isObjectiveType == true)
        {
            ObjectiveCompleted();
        }
        
        Destroy(this.gameObject);
    }
}
