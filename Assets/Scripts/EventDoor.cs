using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDoor : Door
{
    Animator _odAnimator;
    Collider _collider;

    [SerializeField] bool _isLocked;
    [SerializeField] AudioClip _lockedSound;
    [SerializeField] AudioClip _openSound;

    protected override void Start()
    {
        base.Start();
        _odAnimator = GetComponent<Animator>();
        _collider = GetComponent<Collider>();

        if (FindObjectOfType<GeneratorButton>())
        {
            GeneratorButton.instance.GeneratorButtonEvent += UnlockDoor;
        }
    }

    public override void Interact()
    {
        if (!_isLocked)
        {            
            _isOpened = !_isOpened;
            _collider.isTrigger = _isOpened;
            AudioManager.instance.PlaySound(_openSound);
            _odAnimator.SetBool("isOpened", _isOpened);
        }
        else
        {
            AudioManager.instance.PlaySound(_lockedSound);
        }
    }

    private void UnlockDoor()
    {
        _isLocked = false;
        Interact();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 12)
        {
            Interact();
        }
    }
}
