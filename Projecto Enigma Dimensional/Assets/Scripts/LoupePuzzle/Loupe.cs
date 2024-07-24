using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loupe : InteractableObjects
{
    public override void Interact()
    {
        EventManager.Trigger("SetLoupe", true);
        Destroy(gameObject);
    }
}
