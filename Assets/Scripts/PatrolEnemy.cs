using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolEnemy : MonoBehaviour
{
    [SerializeField]
    PlayerController _pController;
    [SerializeField]
    NavMeshAgent _myAgent;
    [SerializeField]
    Transform[] _waypoints;
    int _currentWaypoint;
    [SerializeField]
    float[] _timeToStayInWaypoint;
    float _currentTimeWaitingToNextWaypoint;


    [SerializeField]
    Transform _eyesPosition;
    [SerializeField]
    float _detectionRange;
    bool _isPlayerOnView;
    [SerializeField]
    float _timePlayerOnViewToStartChasing;
    float _currentTimePlayerOnView;
    [SerializeField]
    float _timeToStopChasingPlayer;
    float _currentTimeToStopChasingPlayer;

    bool _isChasingPlayer;

    float _speed;
    [SerializeField]
    float _chasingSpeed;

    [SerializeField]
    int _objectiveToStartChasing;

    [SerializeField]
    Animator _animator;

    [SerializeField]
    AudioSource _as;
    [SerializeField]
    AudioClip _heySound;

    [SerializeField]
    bool _needObjectiveToStartMoving;
    int _currentLevelObjective;
    bool _startMoving;
    private void Start()
    {
        _speed = _myAgent.speed;

        EventManager.Subscribe("UpdateLevelObjective", UpdateLevelObjective);
        EventManager.Subscribe("BellRing", StartMoving);
        EventManager.Subscribe("LoadLevelByCheckPoint", StartMoving);
    }
    private void Update()
    {
        if (_needObjectiveToStartMoving)
        {
            if(_currentLevelObjective >= _objectiveToStartChasing)
            {
                EnemyMovement();
            }
        }
        else
        {
            if (_startMoving)
            {
                EnemyMovement();
            }
        }
    }

    private void UpdateLevelObjective(params object[] parameters)
    {
        _currentLevelObjective = (int)parameters[0];
    }

    private void EnemyMovement()
    {
        CheckToStartChasingPlayer();

        if (_isChasingPlayer)
        {
            ChaseMovement();
        }
        else
        {
            PatrolMovement();
        }
    }

    private void StartMoving(params object[] parameters)
    {
        _startMoving = true;
    }

    private void PatrolMovement()
    {
        _myAgent.speed = _speed;

        if (_myAgent.remainingDistance <= _myAgent.stoppingDistance)
        {
            if(_currentTimeWaitingToNextWaypoint >= _timeToStayInWaypoint[_currentWaypoint])
            {
                _currentWaypoint += 1;
                if (_currentWaypoint >= _waypoints.Length)
                {
                    _currentWaypoint = 0;
                }
                _currentTimeWaitingToNextWaypoint = 0;
            }
            else
            {
                _currentTimeWaitingToNextWaypoint += Time.deltaTime;
            }
            _animator.SetBool("isMoving", false);
            _myAgent.SetDestination(_waypoints[_currentWaypoint].position);
        }
        else
        {
            _animator.SetBool("isMoving", true);
        }
    }

    private void ChaseMovement()
    {
        _myAgent.SetDestination(_pController.transform.position);
        _myAgent.speed = _myAgent.speed + (_chasingSpeed / 2.0f);
        _myAgent.speed = Mathf.Clamp(_myAgent.speed + (Time.deltaTime * 0.70f), _speed, _chasingSpeed);
    }

    private void CheckToStartChasingPlayer()
    {
        if (_isPlayerOnView == true)
        {
            if (_currentTimePlayerOnView >= _timePlayerOnViewToStartChasing)
            {
                if(_isChasingPlayer == false)
                {
                    _as.clip = _heySound;
                    _as.Play();
                    _animator.SetBool("isRunning", true);
                    _animator.SetBool("isMoving", true);
                }
                _isChasingPlayer = true;
            }
            else
            {
                _animator.SetBool("isRunning", false);                
                _isChasingPlayer = false;
                _currentTimePlayerOnView += Time.deltaTime;
            }
        }
        else
        {
            _currentTimePlayerOnView = 0;
            if (_currentTimeToStopChasingPlayer > 0f)
            {
                _currentTimeToStopChasingPlayer -= Time.deltaTime;
            }
            else
            {
                if (_isChasingPlayer)
                {
                    _animator.SetBool("isRunning", false);
                    _isChasingPlayer = false;
                    int nearestWaypoint = 0;
                    for (int i = 0; i < _waypoints.Length; i++)
                    {
                        if (Vector3.Distance(this.transform.position, _waypoints[i].position) <=  Vector3.Distance(this.transform.position, _waypoints[nearestWaypoint].position))
                        {
                            nearestWaypoint = i;
                        }
                    }
                    _currentWaypoint = nearestWaypoint;
                }
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == 11)
        {
            RaycastHit hit;
            Vector3 dir =  other.transform.position - _eyesPosition.transform.position;
            int enemyMask = 1 << 12;     //setea la mascara de la layer 12 que el raycast va detectar
            enemyMask = ~enemyMask;     //invierte la mascara y hace que detecte todo menos esta mascara (hace que el enemigo no se detecte a si mismo)
            Debug.DrawRay(_eyesPosition.transform.position, dir, Color.red, 2f);
            if (Physics.Raycast(_eyesPosition.transform.position, dir, out hit, _detectionRange,enemyMask))
            {
                if (hit.transform.gameObject.layer == 11)
                {
                    _isPlayerOnView = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 11)
        {
            _isPlayerOnView = false;
            _currentTimeToStopChasingPlayer = _timeToStopChasingPlayer;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.layer == 11)
        {
            EventManager.Trigger("PlayerLose");
        }
    }
}
