using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenedDoor : Door
{
    Animator _odAnimator;
    Collider _collider;

    protected override void Start()
    {
        base.Start();
        _odAnimator = GetComponent<Animator>();
        _collider = GetComponent<Collider>();
    }
    public override void Interact()
    {
        _isOpened = !_isOpened;
        _collider.isTrigger = _isOpened;
        _odAnimator.SetBool("isOpened", _isOpened);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 12)
        {
            if (!_isOpened)
            {
                Interact();
            }
        }
    }
}
