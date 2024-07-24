using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallelDimensionObject : MonoBehaviour
{
    [SerializeField]
    PlayerController _pc;
    MeshRenderer _mr;
    Collider _collider;

    private void Start()
    {
        _mr = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (_pc.HabilityState)
        {
            _mr.enabled = true;
            _collider.enabled = true;
        }
        else
        {
            _mr.enabled = false;
            _collider.enabled = false;
        }
    }
}
