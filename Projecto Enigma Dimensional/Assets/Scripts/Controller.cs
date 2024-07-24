using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller
{
    Movement _movement;
    PlayerController _playerController;

    public Controller(PlayerController pc, Movement m)
    {
        _playerController = pc;
        _movement = m;
    }

    public void OnUpdate()
    {
        if(UIManager.instance.IsMouseLocked == false)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {                
               if(_playerController.HabilityState == true)
                {
                    if(_playerController.IsHabilityReadyToCancel() == true)
                    {
                        _playerController.AlternateHability();
                    }
                }
                else
                {
                    if(_playerController.IsHabilityReadyToUse() == true)
                    {
                        _playerController.AlternateHability();
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _playerController.Interact();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                _playerController.AlternateFlashLight();
            }

            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
            {
                _playerController.SetControlState(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            UIManager.instance.AlteranteObjectivePanel();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.instance.EscapePriorityControl();
        }

        if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl))
        {
            _playerController.SetControlState(false);
        }
    }

    public void OnFixedUpdate()
    {
        if(UIManager.instance.IsMouseLocked == false)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            
            if (x != 0 || z != 0)
            {
                if (_playerController.IsCrouching)
                {
                    _movement.Crouch(x, z);
                }
                else
                {
                    if((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
                    {
                        _movement.Sprint(x,z);
                    }
                    else
                    {
                        _movement.Walk(x,z);
                    }
                }
            }

            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            _movement.CameraMovement(mouseX, mouseY);
        }
    }
}
