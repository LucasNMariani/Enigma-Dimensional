using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField]
    float _movementSpeed;
    [SerializeField]
    float _rotationSpeed;
    [SerializeField]
    int _currentWayPointToGo;
    [SerializeField]
    Transform[] _wayPoints;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        transform.rotation = RotateToWaypoint(0);
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(MoveToNextWaypoint(_wayPoints));
        _rb.MoveRotation(RotateToWaypoint(_rotationSpeed));
    }

    private Quaternion RotateToWaypoint(float currentRotationSpeed)
    {
        Quaternion lookAtRotation = Quaternion.LookRotation(_wayPoints[_currentWayPointToGo].transform.position - transform.position);

        if (transform.rotation != lookAtRotation)
        {
            return Quaternion.RotateTowards(transform.rotation, lookAtRotation, currentRotationSpeed * 10 * Time.deltaTime);
        }
        else
        {
            return Quaternion.RotateTowards(transform.rotation, transform.rotation, 0);
        }
    }

     private Vector3 MoveToNextWaypoint(Transform[] waypointPosition)
    {
        if(Vector3.Distance(waypointPosition[_currentWayPointToGo].position, this.transform.position) <= 0.25f)
        {
            if (_currentWayPointToGo < _wayPoints.Length - 1)
            {
                _currentWayPointToGo++;
            }
            else
            {
                _currentWayPointToGo = 0;
            }
        }
        Vector3 difference = waypointPosition[_currentWayPointToGo].position - transform.position;
        return _rb.position + new Vector3(difference.normalized.x, 0, difference.normalized.z) * _movementSpeed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.gameObject.layer == 11)
        {
            EventManager.Trigger("PlayerLose");
        }
    }
}
