using TMPro;
using UnityEngine;

public class CodeNoteLoupe : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI codeText;
    private string code;

    void Start()
    {
        if(codeText == null) codeText = GetComponent<TextMeshProUGUI>();
        code = GameManager.instance.CodeLittleBox.ToString();
    }
    
    public void ActiveCode()
    {
        if(GameManager.instance.HasLoupe) codeText.text = code;
    }

    public void DeactiveCode()
    {
        if (GameManager.instance.HasLoupe) codeText.text = string.Empty;
    }
}
