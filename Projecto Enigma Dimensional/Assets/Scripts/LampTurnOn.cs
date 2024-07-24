using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampTurnOn : MonoBehaviour
{
    [SerializeField]
    Light _lampLight;

    public void SetLampState(bool state)
    {
        _lampLight.enabled = state;
    }

    public bool GetLampState()
    {
        return _lampLight.enabled;
    }
}
