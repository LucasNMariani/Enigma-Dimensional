using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : Door
{
    [SerializeField]
    Transform _movePosition;
    [SerializeField]
    float _movementSpeed;
    float _step;

    protected override void Start()
    {
        base.Start();
        EventManager.Subscribe("OpenSlidingDoors", EventManagerInteraction);
    }


    void Update()
    {
        if(_isOpened == true)
        {
            if (Vector3.Distance(transform.position, _movePosition.position) > 0.1f)
            {
                _step = _movementSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, _movePosition.position, _step);
                if (_aSource.isPlaying == false)
                {
                    _aSource.Play();
                }
            }
            
        }
    }

    private void EventManagerInteraction(params object[] parameters)
    {
        Interact();
        EventManager.UnSubscribe("OpenSlidingDoors", EventManagerInteraction);
    }

    public override void Interact()
    {
        _isOpened = true;        
        if (_isObjectiveType == true)
        {
            ObjectiveCompleted();
        }
    }
}
