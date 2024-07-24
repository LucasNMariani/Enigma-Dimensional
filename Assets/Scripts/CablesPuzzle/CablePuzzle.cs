using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CablePuzzle : MonoBehaviour
{
    CablePoint _currentCableSelected;
    private Dictionary<CableColor, bool> _cableConectedState = new Dictionary<CableColor, bool>();
    [SerializeField] List<Cable> _cablesLineRef = new List<Cable>();
    [SerializeField] TextMeshProUGUI _lightsOnText;
    [SerializeField] Light _light;

    void Start()
    {
        InitializeDictionary();
    }

    public void CableSelected(CablePoint cableCall)
    {
        if (_currentCableSelected == null) _currentCableSelected = cableCall;
        else if(cableCall.color != _currentCableSelected.color) _currentCableSelected = null;
        else if(cableCall.id != _currentCableSelected.id) ConnectCable(cableCall.color);
    }

    void InitializeDictionary()
    {
        _cableConectedState.Add(CableColor.Red, false);
        _cableConectedState.Add(CableColor.Blue, false);
        _cableConectedState.Add(CableColor.Green, false);
        _cableConectedState.Add(CableColor.Yellow, false);
    }

    void ConnectCable(CableColor color)
    {
        _currentCableSelected = null;
        if(_cableConectedState.ContainsKey(color)) _cableConectedState[color] = true;
        foreach (var cable in _cablesLineRef)
        {
            if (cable.color == color) { cable.goReference.SetActive(true); break; }
            continue;
        }
        CompletePuzzleCheck();
    } 

    void CompletePuzzleCheck()
    {
        if(_cableConectedState.All(x => x.Value == true)) 
        {
            //Animacion de que se cierre el tablero electrico
            //Sonido de que se cierra el tablero
            Debug.Log("Cables Fixed");
            _lightsOnText.text = "Luces encendidas!";
            _light.intensity = .5f;
            UIManager.instance.ClosePopUp();
            //GameManager.instance.NextObjective();
        }
        Debug.Log("Cables isn't Fixed yet");
    }
}
