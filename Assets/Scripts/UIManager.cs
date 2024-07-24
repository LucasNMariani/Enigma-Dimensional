using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] GameObject _habilityPanel;
    [SerializeField] Image _habilityStateImage;
    [SerializeField] Image _habilityBackground;
    [SerializeField] Color _habilityColorWhenIsReady;
    [SerializeField] Color _habilityColorWhenHabilityLocked;
    [SerializeField] Image _yellowKey;
    [SerializeField] Image _screwdriver;
    [SerializeField] Image _loupeImage;
    [SerializeField] Image _crowbar;
    [SerializeField] Text _objectiveText;
    [SerializeField] GameObject _objectivePanel;
    [SerializeField] GameObject _infoPanel;
    [SerializeField] Color _colorToChangeObjectivesPanels;
    Color _originalPanelColor;
    [SerializeField] GameObject _timerPanel;
    [SerializeField] Text _timerText;

    [SerializeField] GameObject _inGameMenuPanel;
    InGameMenu _inGameMenuScript;
    bool _isMouseLocked;

    GameObject _currentPopUp;

    [SerializeField] Image _lampImage;
    [SerializeField] Text _lampAmountText;

    [SerializeField] Text _interactionText;
    [SerializeField] Image _mouseImage;

    public bool IsMouseLocked { get => _isMouseLocked; set => _isMouseLocked = value; }
    public GameObject InGameMenuPanel => _inGameMenuPanel;

    void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        _inGameMenuScript = _inGameMenuPanel.GetComponent<InGameMenu>();
        EventManager.Subscribe("SetLoupe", SetLoupe);
        EventManager.Subscribe("SetScrewdriver", SetScrewdriver);
        EventManager.Subscribe("SetCrowbar", SetCrowbar);
        EventManager.Subscribe("SetYellowKey", SetYellowKey);
    }

    public void AlternateHabilityPanel(bool state)
    {
        _habilityPanel.SetActive(state);
    }

    public void UpdateHabilityStateImage(float maxValue, float currentValue, bool isHabilityUsable, bool isHabilityCancellable)
    {
        _habilityStateImage.fillAmount = 1.0f / maxValue * currentValue;
        if (isHabilityUsable)
        {
            _habilityBackground.color = _habilityColorWhenIsReady;
        }
        else if(!isHabilityCancellable)
        {
            _habilityBackground.color = _habilityColorWhenHabilityLocked;
        }
        else
        {
            _habilityBackground.color = Color.white;
        }
    }

    public void SetYellowKey(params object[] parameters)
    {
        _yellowKey.enabled = (bool)parameters[0];
    }

    public void SetScrewdriver(params object[] parameters)
    {
        _screwdriver.enabled = (bool)parameters[0];
    }
    
    public void SetLoupe(params object[] parameters)
    {
        _loupeImage.enabled = (bool)parameters[0];
    }

    public void SetCrowbar(params object[] parameters)
    {
        _crowbar.enabled = (bool)parameters[0];
    }

    #region Objective
    public void SetNewObjective(string newObjective)
    {
        _objectiveText.text = newObjective;
        ChangeObjectiveTextColors();
    }

    private void ChangeObjectiveTextColors()
    {
        _originalPanelColor = _objectivePanel.GetComponent<Image>().color;
        _objectivePanel.GetComponent<Image>().color = _colorToChangeObjectivesPanels;
        _infoPanel.GetComponent<Image>().color = _colorToChangeObjectivesPanels;
        Invoke("ReturnObjectiveTextColorsToNormal", 3.0f);
    }

    private void ReturnObjectiveTextColorsToNormal()
    {
        _objectivePanel.GetComponent<Image>().color = _originalPanelColor;
        _infoPanel.GetComponent<Image>().color = _originalPanelColor;
    }

    public void AlteranteObjectivePanel()
    {
        _objectivePanel.SetActive(!_objectivePanel.activeSelf);
        _infoPanel.SetActive(!_infoPanel.activeSelf);
    }
    #endregion

    public void ChangeMouseCursorLockState(bool isMouseLocked)
    {
        if(isMouseLocked == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _isMouseLocked = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _isMouseLocked = true;
        }
    }

    public void OpenPopUp(GameObject popUp)
    {
        _currentPopUp = popUp;
        _currentPopUp.SetActive(true);
        ChangeMouseCursorLockState(false);
    }

    public void ClosePopUp()
    {
        _currentPopUp.SetActive(false);
        ChangeMouseCursorLockState(true);
    }

    public void AlternateInGameMenuPanel()
    {
        if (_inGameMenuPanel.activeSelf == true)
        {
            ChangeMouseCursorLockState(true);
            _inGameMenuPanel.SetActive(false);

        }
        else
        {
            _inGameMenuPanel.SetActive(true);
            ChangeMouseCursorLockState(false);
            _inGameMenuScript.ResetLens();
        }
    }

    public void EscapePriorityControl()
    {
        if(_currentPopUp != null)
        {
            if(_currentPopUp.activeSelf != true)
            {
                AlternateInGameMenuPanel();
            }
            else
            {
                ClosePopUp();
            }
        }
        else
        {
            if(_inGameMenuScript.IsVolumePanelOpened() == true)
            {
                _inGameMenuScript.CloseVolumePanel();
            }
            else
            {
                AlternateInGameMenuPanel();
            }
        }
    }

    public void OpenTimerPanel()
    {
        _timerPanel.SetActive(true);
    }

    public void UpdateTimer(int time)
    {
        int seconds = time % 60;
        int minutes = time / 60;
        if(seconds < 10)
        {
            _timerText.text = minutes + ":0" + seconds;
        }
        else
        {
            _timerText.text = minutes + ":" + seconds;
        }
        if(_timerText.color != Color.red && minutes == 0)
        {
            _timerText.color = Color.red;
        }
    }

    public void UpdateLampAmount(int amount)
    {
        if(amount <= 0)
        {
            _lampImage.enabled = false;
            _lampAmountText.enabled = false;
        }
        else
        {
            _lampImage.enabled = true;
            _lampAmountText.enabled = true;
            _lampAmountText.text = "x" + amount;
        }
    }

    public void ShowInteractableText(string interactionText, bool _isMouseNeeded)
    {
        _interactionText.text = interactionText;
        _mouseImage.enabled =_isMouseNeeded;
        
    }
    
}
