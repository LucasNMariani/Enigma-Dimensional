using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour
{
    public CableColor color;
    public GameObject goReference;

    void Start()
    {
        goReference = GetComponent<GameObject>();
    }
}
