using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    Camera _camera;
    Rigidbody _rb;
    [SerializeField]
    Transform _normalPosition;
    [SerializeField]
    Transform _crouchingPosition;
    bool _isCrouching;


    [SerializeField]
    Transform _player;
    float _differentialY;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _rb = FindObjectOfType<Rigidbody>();
    }

    private void Start()
    {
        _differentialY = this.transform.position.y - _player.transform.position.y;
    }

    
    private void Update()
    {
        //this.transform.position = _player.transform.position + new Vector3(0, _differentialY, 0);
        if (_isCrouching)
        {
            this.transform.position = new Vector3(_player.transform.position.x, _crouchingPosition.position.y, _player.transform.position.z);
        }
        else
        {
            this.transform.position = new Vector3(_player.transform.position.x, _normalPosition.position.y, _player.transform.position.z);
        }
    }

    public void SetHabilityView(bool habilityState)
    {
        _camera.LayerCullingToggle("NormalDimension");
        _camera.LayerCullingToggle("ParallelDimension");
        /*if(habilityState == true)
        {
            _camera.LayerCullingHide("BlackPilar");
            _camera.LayerCullingShow("RedPilar");
        }
        else
        {
            _camera.LayerCullingHide("RedPilar");
            _camera.LayerCullingShow("BlackPilar");
        }*/
    }

    public void SetCrouchingStateInCamera(bool isCrouching)
    {
        _isCrouching = isCrouching;
    }

}
