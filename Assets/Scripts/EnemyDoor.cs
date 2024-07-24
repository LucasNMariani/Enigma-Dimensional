using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDoor : Door
{
    Animator _odAnimator;
    Collider _collider;

    [SerializeField]
    AudioClip _lockedSound;
    [SerializeField]
    AudioClip _openSound;

    protected override void Start()
    {
        base.Start();
        _odAnimator = GetComponent<Animator>();
        _collider = GetComponent<Collider>();
    }

    public override void Interact()
    {
        if (!_isOpened)
        {
            AudioManager.instance.PlaySound(_lockedSound);
        }
    }

    protected override void LoadCheckPointConfiguration()
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
                _isOpened = !_isOpened;
                _collider.isTrigger = _isOpened;
                AudioManager.instance.PlaySound(_openSound);
                _odAnimator.SetBool("isOpened", _isOpened);
                if (_isObjectiveType == true)
                {
                    ObjectiveCompleted();
                }
            }
        }
    }
}
