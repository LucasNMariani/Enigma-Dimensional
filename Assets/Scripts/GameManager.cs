using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] Timer _timer;

    [SerializeField] Objective[] levelObjectives;
    [SerializeField] int _currentLevelObjective;

    bool _hasScrewdriver;
    bool _hasLoupe;
    [SerializeField] bool _useObjetives;

    [SerializeField] int _maxLamps;
    int _currentLamps;
    bool _allLampsCollected;
    GameObject[] _lamps;
    [SerializeField] string _codeLB = "28091921";



    public bool HasScrewdriver => _hasScrewdriver;
    public bool HasLoupe => _hasLoupe; 
    public string CodeLittleBox => _codeLB;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
        EventManager.Subscribe("LoadLevelByCheckPoint", LoadObjective);
    }

    void Start()
    {
        _lamps = new GameObject[_maxLamps];
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        EventManager.Subscribe("SetScrewdriver", SetScrewdriver);
        EventManager.Subscribe("SetLoupe", SetLoupe);
        EventManager.Subscribe("GiveLamp", AddLamp);
        if (_useObjetives)
        {
            EventManager.Subscribe("NextObjective", NextObjective);
            NextObjective();
        }
    }

    public void NextObjective(params object[] parameters)
    {
        if(_currentLevelObjective < levelObjectives.Length)
        {
            _timer.CheckObjective(_currentLevelObjective);
            UIManager.instance.SetNewObjective(levelObjectives[_currentLevelObjective].ObjectiveText);
            _currentLevelObjective++;
            EventManager.Trigger("UpdateLevelObjective", _currentLevelObjective);
        }
        else LevelLoader.instance.LoadNextLevel();
    }

    private void LoadObjective(params object[] parameters)
    {
        _currentLevelObjective = (int)parameters[1] -2;
    }

    private void SetScrewdriver(params object[] parameters)
    {
        _hasScrewdriver = (bool)parameters[0];
    }

    private void SetLoupe(params object[] parameters)
    {
        _hasLoupe = (bool)parameters[0];
    }

    public void AddLamp(params object[] parameters)
    {
        _currentLamps++;
        UIManager.instance.UpdateLampAmount(_currentLamps);
        if(_currentLamps >= _maxLamps)
        {
            if(_useObjetives) NextObjective();
            _allLampsCollected = true;
        }        
    }

    public bool RemoveLamp()
    {
        if (_allLampsCollected)
        {
            _currentLamps--;
            UIManager.instance.UpdateLampAmount(_currentLamps);
            return true;
        }
        else return false;
    }
}
