using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommonDoor : Door
{
    [SerializeField]
    bool _needKey;
    [SerializeField]
    Animator _animator;
    bool _hasYellowKey;
    [SerializeField] string _animatorOpenName;

    protected override void Start()
    {
        base.Start();
        EventManager.Subscribe("SetYellowKey", SetYellowKey);
        if(!_animator) _animator = GetComponent<Animator>();
    }

    public override void Interact()
    {
        if(_isOpened == false)
        {
            if(_needKey == true)
            {
                if(_hasYellowKey == true)
                {
                    OpenDoor();
                    EventManager.Trigger("SetYellowKey", false);
                    //EventManager.UnSubscribe("SetYellowKey", SetYellowKey);
                }
            }
            else
            {
                OpenDoor();
            }
        }
    }

    private void OpenDoor()
    {
        _isOpened = true;
        _aSource.Play();        
        if (_isObjectiveType == true)
        {
            ObjectiveCompleted();
        }
    }

    private void Update()
    {
        if (_isOpened == true)
        {
            _animator.SetBool(_animatorOpenName, true);
        }
    }

    private void SetYellowKey(params object[] parameters)
    {
        _hasYellowKey = (bool)parameters[0];
    }


}
