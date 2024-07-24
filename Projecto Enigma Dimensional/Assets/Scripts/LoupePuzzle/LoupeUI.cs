using System;
using UnityEngine;

public class LoupeUI : MonoBehaviour
{
    private Action LoupeActiveAction;
    [SerializeField] private GameObject _loupeUI;

    void Start()
    {
        LoupeActiveAction = delegate { };
    }

    void Update()
    {
        LoupeActiveAction();
    }

    void LoupeActive()
    {
        //_loupeUI.transform.position = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.z);
        _loupeUI.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
    }

    void OnEnable()
    {
        if (GameManager.instance.HasLoupe)
        {
            LoupeActiveAction += LoupeActive;
            _loupeUI.SetActive(true);
        } 
    }

    void OnDisable()
    {
    }
}
