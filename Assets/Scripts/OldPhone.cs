using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldPhone : InteractableObjects
{
    [SerializeField] private Animator _myAnimator;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _waitForCall = 7;

    [SerializeField] private PhoneState _currentPhoneState;

    private float _callingTimer;

    private void Start()
    {
        Invoke("ChangePhoneState", _waitForCall);
    }

    private void Update()
    {
        if (_currentPhoneState.Equals(PhoneState.Calling))
        {
            _callingTimer += .1f * Time.deltaTime;
            _myAnimator.SetFloat("CallingState", _callingTimer);

            if (_callingTimer > 1)
            {
                _callingTimer = 0;
                ChangePhoneState();
            }
        }
    }

    public void ChangePhoneState()
    {
        switch (_currentPhoneState)
        {
            case PhoneState.Idle:
                _currentPhoneState = PhoneState.Calling;
                _audioSource.Play();
                break;
            case PhoneState.Calling:
                _currentPhoneState = PhoneState.WaitingAnswer;
                break;
            case PhoneState.WaitingAnswer:
                _currentPhoneState = PhoneState.Answering;
                _myAnimator.SetBool("Answering", true);
                _audioSource.Stop();
                break;
            case PhoneState.Answering:
                _currentPhoneState = PhoneState.Idle;
                break;
            default:
                break;
        }
    }

    public override void Interact()
    {
        Debug.Log("Interactt");
        ChangePhoneState();
    }
}

public enum PhoneState
{
    Idle,
    Calling,
    WaitingAnswer,
    Answering
}
