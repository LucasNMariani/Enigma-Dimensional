using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenedVent : MonoBehaviour
{
    private void Start()
    {
        EventManager.Trigger("NextObjective");
    }
}
