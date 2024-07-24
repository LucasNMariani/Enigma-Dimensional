using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    [SerializeField]
    string _objectiveText;    

    public string ObjectiveText => _objectiveText;
}
