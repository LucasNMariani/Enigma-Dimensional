using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : InteractableObjects
{
    MeshRenderer _mr;
    Material _normalMaterial;
    [SerializeField]
    Material _pressedMaterial;
    [SerializeField]
    Material[] _currentMaterials;
    [SerializeField]
    int _buttonMaterial;
    float _timeToStayPress = 0.1f;
    float _timePressed;

    protected bool _isPressed;
    [SerializeField]
    InteractableObjects[] _objectsToActivate;
    bool _isUsed;

    virtual protected void Awake()
    {
        _mr = GetComponent<MeshRenderer>();
        _currentMaterials = _mr.materials;
        _normalMaterial = _currentMaterials[_buttonMaterial];
    }

    public override void Interact()
    {        
        SetButtonInteracted();
        ActivateObjects();
    }

    virtual protected void Update()
    {
        if(_isPressed == true)
        {
            if(_timePressed >= _timeToStayPress)
            {
                ResetButton();
            }
            else
            {
                _timePressed += Time.deltaTime;
            }
        }
    }

    virtual protected void ResetButton()
    {
        _timePressed = 0;
        _currentMaterials[_buttonMaterial] = _normalMaterial;
        _mr.materials = _currentMaterials;
        _isPressed = false;
    }

    virtual protected void ActivateObjects()
    {
        for (int i = 0; i < _objectsToActivate.Length; i++)
        {
            _objectsToActivate[i].Interact();
        }
    }

    virtual protected void SetButtonInteracted()
    {
        _currentMaterials[_buttonMaterial] = _pressedMaterial;
        _mr.materials = _currentMaterials;
        _isPressed = true;
    }
}
