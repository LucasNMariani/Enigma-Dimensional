using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleKey : InteractableObjects
{
    [SerializeField] private SimpleDoor _simpleDoor;
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
            if (TutorialManager.Instance.CheckActiveOnTrigger())
            {
                EventManager.Trigger("SetYellowKey", true);
                AudioManager.instance.PlaySound(_keySound);

                _simpleDoor.HasKey = true;
                Destroy(this.gameObject);
            }

        }
    }
}
