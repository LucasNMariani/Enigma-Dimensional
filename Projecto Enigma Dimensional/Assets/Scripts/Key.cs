using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : InteractableObjects
{
    [SerializeField]
    AudioClip _keySound;
    bool _hasYellowKey;
    protected override void Start()
    {
        base.Start();
    }
    public override void Interact()
    {
        if (_hasYellowKey == false)
        {
            EventManager.Trigger("SetYellowKey", true);
            AudioManager.instance.PlaySound(_keySound);

            if (_isObjectiveType == true)
            {
                ObjectiveCompleted();
            }
            Destroy(this.gameObject);
        }
    }

}
