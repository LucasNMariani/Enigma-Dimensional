using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class CodeLittleBox : MonoBehaviour
{
    private char[] _charsPerCode;
    private int _codeCharCount = 0;

    void Start()
    {
        _charsPerCode = GameManager.instance.CodeLittleBox.ToCharArray();
    }

    public void ButtonCodePressed(int code)
    {
        Debug.Log($"Codigo ingresado {code} -- Letra actual a comparar {_charsPerCode[_codeCharCount]}");
        //if ((_charsPerCode[_codeCharCount]) - '0' == code) { _codeCharCount++; Debug.Log($"Numero ingresado {code}"); }
        if(int.Parse(_charsPerCode[_codeCharCount].ToString()) == code) 
        { 
            _codeCharCount++; 
            Debug.Log($"Numero ingresado {code}"); 
        }
        else { _codeCharCount = 0; Debug.Log("Wrong code, reset count"); return; }
        if (_codeCharCount == _charsPerCode.Length)
        {
            Debug.Log("El codigo es correcto ganó el nivel");
			UIManager.instance.ClosePopUp();
            LevelLoader.instance.LoadNextLevel();
        }
    }
}
