using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHand : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.layer == 11)
        {
            EventManager.Trigger("PlayerLose");
        }
    }
}
