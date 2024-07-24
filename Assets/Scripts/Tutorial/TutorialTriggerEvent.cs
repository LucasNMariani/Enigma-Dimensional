using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriggerEvent : MonoBehaviour
{
    private bool _activated;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11 && !_activated)
        {
            if (TutorialManager.Instance.CheckActiveOnTrigger())
            {
                _activated = true;
                gameObject.SetActive(false);
            }
        }
    }
}