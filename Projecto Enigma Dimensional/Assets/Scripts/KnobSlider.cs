using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnobSlider : MonoBehaviour
{
    [SerializeField]
    Safe _mySafe;
    [SerializeField]
    Transform _handle;
    Vector3 _mousePos;
    [SerializeField]
    float[] _handAngles;
    int _nearestAngle = 0;
    enum KnobTypes
    {
        MinuteHand,
        HourHand
    }
    [SerializeField]
    KnobTypes _knobType;

    private void Start()
    {
        switch (_knobType)
        {
            case KnobTypes.MinuteHand:
                SetAngles(12);
                break;
            case KnobTypes.HourHand:
                SetAngles(24);
                break;
            default:
                break;
        }
    }

    private void SetAngles(int amountOfAngles)
    {
        _handAngles = new float[amountOfAngles];
        for (int i = 0; i < _handAngles.Length; i++)
        {
            _handAngles[i] = (360 / amountOfAngles) * i * 1.0f;
        }
    }

    public void OnSlider()
    {
        float _angle;
        _mousePos = Input.mousePosition;
        Vector2 dir = _mousePos - _handle.position;
        _angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        
        Quaternion r = Quaternion.AngleAxis(_angle - 90, Vector3.forward);
        _handle.rotation = r;        
    }

    public void ExitSlider()
    {
        float nearestDif= 360;
        for (int i = 0; i < _handAngles.Length; i++)
        {
            float dif = Mathf.Abs(GetCurrentAngle() - _handAngles[i]);

            if (dif <= nearestDif)
            {
                nearestDif = dif;
                _nearestAngle = i;
            }
        }

        Quaternion r = Quaternion.AngleAxis(_handAngles[_nearestAngle], Vector3.forward);
        _handle.rotation = r;
        SetKnobValue();
    }

    public void SetKnobValue()
    {
        int x = _handAngles.Length - _nearestAngle;
        int y;
        switch (_knobType)
        {
            case KnobTypes.MinuteHand:
                y = (int)(_handAngles[x] / 6.0f);
                _mySafe.UpdateMinuteValue(y);
                break;
            case KnobTypes.HourHand:
                y = (int)(_handAngles[x] / 15.0f);
                _mySafe.UpdateHourValue(y);
                break;
            default:
                break;
        }
    }

    private float GetCurrentAngle()
    {
        return _handle.rotation.eulerAngles.z;        
    }
}
