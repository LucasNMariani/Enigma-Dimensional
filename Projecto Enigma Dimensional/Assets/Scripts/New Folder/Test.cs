using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Dictionary<string, int> enemiesDeadCount = new Dictionary<string, int>();
    //public List<string> enemiesNames = new List<string>();
    public static Test instance;
    public TextMeshProUGUI monsterDogCountText;
    public TextMeshProUGUI niñaCountText;

    void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;
    }
    
    // (string) => ()
    public void UpdateEnemyDead(string enemyName)
    {
        if (!enemiesDeadCount.ContainsKey(enemyName))
        {
            enemiesDeadCount.Add(enemyName, 1);
            //enemiesNames.Add(enemyName);
        }
        else enemiesDeadCount[enemyName]++;
        UpdateUIEnemiesText();
    }

    public void UpdateUIEnemiesText()
    {
        monsterDogCountText.text = enemiesDeadCount["MonsterDog"].ToString();
        niñaCountText.text = $"Enemigo Niña: {enemiesDeadCount["Niña"]} asesinados";
    }
}

public abstract class Enemies : MonoBehaviour
{
    [SerializeField] protected string name;
    protected virtual void Dead()
    {
        Test.instance.UpdateEnemyDead(name);
    }
}

public class Monster : Enemies
{
    void Start()
    {
        name = "MonsterDead";
    }
    
}

public class Nina : Enemies
{
    void Start()
    {
        name = "Niña";
    }
}

