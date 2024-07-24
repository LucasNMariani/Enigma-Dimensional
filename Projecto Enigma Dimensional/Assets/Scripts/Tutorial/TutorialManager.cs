using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;
    [SerializeField] private TutorialPopUp[] _tutorialPopUps;
    [SerializeField] private PlayerController _playerController;
    private bool _isBlocked = false;
    [SerializeField] private float _timeToFirstPopUp = 2f;
    [SerializeField] private float _timebetweenPopUps = 2f;

    private int _popupIndex = -1;

    private void Awake()
    {
        if (!Instance) Instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        Invoke("ChangePopupAlert", _timeToFirstPopUp);
    }

    private void Update()
    {
        if(_popupIndex > -1 && _popupIndex < _tutorialPopUps.Length)
        {
            if (!_tutorialPopUps[_popupIndex].needTrigger && _tutorialPopUps[_popupIndex].requiredKeys.Length > 0)
            {
                if(_tutorialPopUps[_popupIndex].requiredKeys.Any(x => Input.GetKeyDown(x)))
                {
                    SuccesAction();
                }
            }
        }

    }

    private void SuccesAction()
    {
        if(_isBlocked)
            StartCoroutine(SuccessRoutine());
    }

    private void ChangePopupAlert()
    {
        _popupIndex++;

        if(_popupIndex < _tutorialPopUps.Length && !_tutorialPopUps[_popupIndex].needTrigger)
        {
            _tutorialPopUps[_popupIndex].gameObject.SetActive(true);

            _isBlocked = !_isBlocked;
            _playerController?.BlockPlayer(_isBlocked);
        }
    }

    public bool CheckActiveOnTrigger()
    {
        if (_tutorialPopUps[_popupIndex].needTrigger)
        {
            _tutorialPopUps[_popupIndex].needTrigger = false;
            _tutorialPopUps[_popupIndex].gameObject.SetActive(true);

            _isBlocked = !_isBlocked;
            _playerController?.BlockPlayer(_isBlocked);

            if (_tutorialPopUps[_popupIndex].requiredKeys.Length == 0)
            {
                StartCoroutine(WaitingSuccessRoutine());
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator SuccessRoutine()
    {
        _isBlocked = !_isBlocked;
        _playerController?.BlockPlayer(_isBlocked);

        _tutorialPopUps[_popupIndex].gameObject.SetActive(false);

        yield return new WaitForSeconds(_timebetweenPopUps);

        ChangePopupAlert();
    }

    private IEnumerator WaitingSuccessRoutine()
    {
        _isBlocked = !_isBlocked;
        _playerController?.BlockPlayer(_isBlocked);

        yield return new WaitForSeconds(_timebetweenPopUps);

        _tutorialPopUps[_popupIndex].gameObject.SetActive(false);

        ChangePopupAlert();
    }
}