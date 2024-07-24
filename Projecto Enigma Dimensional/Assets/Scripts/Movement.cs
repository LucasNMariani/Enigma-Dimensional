using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement
{
    PlayerController _pController;
    Transform _pTransform;
    Rigidbody _pRigidBody;
    CameraView _pCamera;
    float _walkSpeed, _sprintSpeed;
    float _cameraSpeed;
    float _cameraUpAngleLimitation, _cameraDownAngleLimitation;

    float _yaw, _pitch;

    public float Yaw {set => _yaw = value; }

    public Movement(PlayerController pc, Rigidbody rb, float walkSpeed, float sprintSpeed, CameraView pCamera, float cameraSpeed, float camUpLimitaton,float camDownLimitation, float inicialYaw)
    {
        _pController = pc;
        _pTransform = pc.transform;
        _pRigidBody = rb;
        _walkSpeed = walkSpeed;
        _sprintSpeed = sprintSpeed;
        _pCamera = pCamera;
        _cameraSpeed = cameraSpeed;
        _cameraUpAngleLimitation = camUpLimitaton;
        _cameraDownAngleLimitation = camDownLimitation;
        _yaw = inicialYaw;
    }


    public void Walk(float axisX, float axisZ)
    {
        _pRigidBody.MovePosition(_pRigidBody.position + (_pTransform.right * axisX + _pTransform.forward * axisZ).normalized * _walkSpeed * Time.fixedDeltaTime);
        _pController.StepSound(false);        
    }

    public void Sprint(float axisX, float axisZ)
    {
        _pRigidBody.MovePosition(_pRigidBody.position + (_pTransform.right * axisX + _pTransform.forward * axisZ).normalized * _sprintSpeed * Time.fixedDeltaTime);
        _pController.StepSound(true);
    }
    public void Crouch(float axisX, float axisZ)
    {
        _pRigidBody.MovePosition(_pRigidBody.position + (_pTransform.right * axisX + _pTransform.forward * axisZ).normalized * _walkSpeed/2 * Time.fixedDeltaTime);
        
    }

    public void CameraMovement(float mouseAxisX, float mouseAxisZ)
    {
        _yaw += _cameraSpeed * mouseAxisX * Time.fixedDeltaTime;
        _pitch -= _cameraSpeed * mouseAxisZ * Time.fixedDeltaTime;
        _pitch = Mathf.Clamp(_pitch, _cameraUpAngleLimitation, _cameraDownAngleLimitation);
        _pCamera.transform.eulerAngles = new Vector3(_pitch, _yaw, 0);
        _pRigidBody.MoveRotation(Quaternion.Euler(new Vector3(0, _yaw, 0)));
    }


}
