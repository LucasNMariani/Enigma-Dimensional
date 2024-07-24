using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.C) && Input.GetKeyDown(KeyCode.I))
        {
            EventManager.Trigger("SetCrowbar", true);
            EventManager.Trigger("SetLoupe", true);
            EventManager.Trigger("SetScrewdriver", true);
            EventManager.Trigger("SetYellowKey", true);
        }

        if(Input.GetKey(KeyCode.C) && Input.GetKeyDown(KeyCode.O))
        {
            EventManager.Trigger("NextObjective");
        }
    }
}
