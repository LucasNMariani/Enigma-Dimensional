using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : InteractableObjects
{
    [SerializeField] GameObject _popUpPanel;

    protected override void Start()
    {
        base.Start();
    }
    public override void Interact()
    {
        UIManager.instance.OpenPopUp(_popUpPanel);
    }

    protected override void LoadCheckPointConfiguration()
    {
        if (_popUpPanel.gameObject.GetComponent<Vent>())
        {
            Vent tempVent = _popUpPanel.gameObject.GetComponent<Vent>();
            Interact();
            UIManager.instance.ClosePopUp();
            tempVent.EndStates();
        }
    }



}
