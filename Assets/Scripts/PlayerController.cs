using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [Header("Variables")]
    AudioSource _as;
    Rigidbody _rb;

    Movement _movement;
    Controller _controller;
    [SerializeField] Collider _normalCollider;
    [SerializeField] Collider _crouchingCollider;


    [SerializeField] float _walkSpeed;
    [SerializeField] float _sprintSpeed;
    [SerializeField] float _timeToNextStepWalking;
    [SerializeField] float _timeToNextStepRunning;
    float _currentTimeToNextStep;
    [SerializeField] bool _isCrouching;
    [SerializeField] bool _isCTRLPressed;
    [SerializeField] bool _isPlayerOnCrouchingArea; 

    [SerializeField] AudioClip[] _stepsSounds;

    [SerializeField] CameraView _camera;
    [SerializeField] float _cameraSpeed;
    [SerializeField] float _cameraUpAngleLimitation;
    [SerializeField] float _cameraDownAngleLimitation;
    [SerializeField] float _inicialRotation;

    [SerializeField] float _interactionRange;
    GameObject _objectInView;

    [SerializeField] float _habilityDuration;
    float _habilityCurrentTime;
    bool _habilityState;
    [SerializeField] float _habilityMinUsageTime;

    [SerializeField] Light _flashLight;
    [SerializeField] MeshCollider _lightCollider;

    [SerializeField] Transform[] _checkPointPosition;
    [SerializeField] float[] _cameraRotation;

    public bool HabilityState=> _habilityState;
    public bool IsCrouching => _isCrouching;
    public bool IsInCrouchingArea => _isPlayerOnCrouchingArea;

    private bool _isPlayerBlocked;

    #endregion

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _as = GetComponent<AudioSource>();
        EventManager.Subscribe("LoadLevelByCheckPoint", CheckPointChecker);
    }

    private void Start()
    {
        _movement = new Movement(this, _rb, _walkSpeed, _sprintSpeed, _camera, _cameraSpeed, _cameraUpAngleLimitation, _cameraDownAngleLimitation, _inicialRotation);
        _controller = new Controller(this, _movement);

        _habilityCurrentTime = _habilityDuration;
    }
    private void Update()
    {
        if (_isPlayerBlocked) return;

        _controller.OnUpdate();
        CheckHabilityState();
        //CheckObjectInFront();
    }

    private void FixedUpdate()
    {
        if (_isPlayerBlocked) return;

        _controller.OnFixedUpdate();        
    }

    public void BlockPlayer(bool value)
    {
        _isPlayerBlocked = value;
    }

    private void CheckHabilityState()
    {
        if (_habilityState == true)
        {
            if (_habilityCurrentTime >= 0)
            {
                _habilityCurrentTime -= Time.deltaTime;
            }
            else
            {
                AlternateHability();
            }
        }
        else
        {
            if (_habilityCurrentTime < _habilityDuration)
            {
                _habilityCurrentTime += Time.deltaTime;
            }
        }
        UIManager.instance.UpdateHabilityStateImage(_habilityDuration, _habilityCurrentTime, IsHabilityReadyToUse(), IsHabilityReadyToCancel());
    }

    public void AlternateHability()
    {
        if (_habilityState == true)
        {                
            _habilityState = false;
        }
        else
        {
            _habilityState = true;
        }
        AudioManager.instance.ChangeMusicMixer(_habilityState);
        UIManager.instance.AlternateHabilityPanel(_habilityState);
        _camera.SetHabilityView(_habilityState);
        
    }

    public bool IsHabilityReadyToCancel()
    {
        if(_habilityCurrentTime > _habilityDuration - _habilityMinUsageTime)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool IsHabilityReadyToUse()
    {
        if(_habilityCurrentTime >= _habilityDuration)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /*
    private void CheckObjectInFront()
    {
        _objectInView = null;
        RaycastHit hit;
        int playerMask = 1 << 10;     //setea la mascara de la layer 12 que el raycast va detectar
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, _interactionRange, playerMask))
        {
            if (hit.transform.gameObject.layer == 10)
            {
                InteractableObjects tempIO = hit.transform.gameObject.GetComponent<InteractableObjects>();
                _objectInView = tempIO.gameObject;
                _uim.ShowInteractableText(tempIO.InteractionText, tempIO.IsInteractableWithMouse);
                               
            }
        }
        if (_objectInView == null)
        {
            _uim.ShowInteractableText("", false);
        }
    }*/

    public void Interact()
    {
        RaycastHit hit;
        int playerMask = 1 << 11;     //setea la mascara de la layer 12 que el raycast va detectar        
        playerMask = ~playerMask;     //invierte la mascara y hace que detecte todo menos esta mascara (hace que el player no se detecte a si mismo)
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit , _interactionRange,playerMask))
        {
            if(hit.transform.gameObject.layer == 10)
            {
                InteractableObjects tempIO = hit.transform.gameObject.GetComponent<InteractableObjects>();
                tempIO.Interact();
            }
        }
    }

    public void StepSound(bool isSprinting)
    {   
        if(_currentTimeToNextStep<= 0)
        {
            int x = Random.Range(0, _stepsSounds.Length);
            _as.clip = _stepsSounds[x];
            _as.Play();
            if(isSprinting == false)
            {
                _currentTimeToNextStep = _timeToNextStepWalking;
            }
            else
            {
                _currentTimeToNextStep = _timeToNextStepRunning;
            }
        }
        else
        {
            _currentTimeToNextStep -= Time.fixedDeltaTime;
        }
    }

    public void AlternateFlashLight()
    {
        if (_flashLight.enabled)
        {
            _flashLight.enabled = false;
            _lightCollider.enabled = false;
        }
        else
        {
            _flashLight.enabled = true;
            _lightCollider.enabled = true;
        }
    }

    public void SetControlState(bool CTRLState)
    {
        _isCTRLPressed = CTRLState;
        if (_isCTRLPressed)
        {
            SetCrouchState(true);
        }
        else
        {
            if (!_isPlayerOnCrouchingArea)
            {
                SetCrouchState(false);
            }
        }
    }

    public void SetCrouchState(bool isCrouching)
    {
        _isCrouching = isCrouching;
        _crouchingCollider.enabled = isCrouching;
        _normalCollider.enabled = !isCrouching;
        _camera.SetCrouchingStateInCamera(isCrouching);
    }

    private void CheckPointChecker(params object[] parameters)
    {
        int x = (int)parameters[0];
        if (x > 0)
        {
            x--;
            this.transform.position = _checkPointPosition[x].position;
            _movement.Yaw = _cameraRotation[x];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 13)
        {
            _isPlayerOnCrouchingArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 13)
        {
            _isPlayerOnCrouchingArea = false;
            if (!_isCTRLPressed)
            {
                SetCrouchState(false);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 13)
        {
            _isPlayerOnCrouchingArea = true;
        }        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 13)
        {
            _isPlayerOnCrouchingArea = false;
            if (!_isCTRLPressed)
            {
                SetCrouchState(false);
            }
        }        
    }
}
