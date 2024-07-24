using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseOver : MonoBehaviour
{
    [SerializeField]
    GameObject Lupa;
    public void OnOver()
    {
        Lupa.SetActive(true);
    }
    public void OnExit()
    {
        Lupa.SetActive(false);
    }
}
