using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjects : MonoBehaviour
{
    protected AudioSource _aSource;
    [SerializeField]
    protected bool _isObjectiveType;
    [SerializeField]
    string _interactionText = "Interactuable";
    [SerializeField]
    bool _isInteractableWithMouse;
    [SerializeField]
    bool _hasCheckPointImportance;
    [SerializeField]
    int _checkPointToLoad;

    public string InteractionText { get => _interactionText;}
    public bool IsInteractableWithMouse { get => _isInteractableWithMouse;}

    virtual protected void Awake()
    {
        EventManager.Subscribe("LoadLevelByCheckPoint", CheckPointChecker);
    }

    virtual protected void Start()
    {
        _aSource = GetComponent<AudioSource>();
    }

    public virtual void Interact()
    {
    }

    virtual protected void ObjectiveCompleted()
    {
        EventManager.Trigger("NextObjective");
    }

    virtual protected void LoadCheckPointConfiguration()
    {
        Interact();
    }

    private void CheckPointChecker(params object[] parameters)
    {
        //Debug.Log(this.gameObject.name);
        if ((int)parameters[0] >= _checkPointToLoad && _hasCheckPointImportance)
        {
            LoadCheckPointConfiguration();
        }
    }


}
