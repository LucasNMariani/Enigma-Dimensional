using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vent : MonoBehaviour
{
    [SerializeField]
    int _screwAmount;
    [SerializeField]
    GameObject _ventPanelClosed;
    [SerializeField]
    GameObject _ventPanelOpen;
    [SerializeField]
    bool _hasCheckPointImportance;
    [SerializeField]
    int _checkPointToLoad;

    public void UnscrewVent()
    {
        _screwAmount--;
        if(_screwAmount <= 0)
        {
            EndStates();
        }
    }

    public void EndStates()
    {
        _ventPanelOpen.SetActive(true);
        Destroy(_ventPanelClosed.gameObject);
        Destroy(this.gameObject);
    }

    private void LoadCheckPoint(params object[] parameters)
    {
        Debug.Log((int)parameters[0] + "/" + _checkPointToLoad + " : " + _hasCheckPointImportance);
        if ((int)parameters[0] >= _checkPointToLoad && _hasCheckPointImportance)
        {
            EndStates();
        }
    }


}
