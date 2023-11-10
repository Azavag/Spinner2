using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] int moneyCount;
    [SerializeField] TextMeshProUGUI moneyText;

    private void Awake()
    {
        EventManager.EnemyDied += OnEnemyDied;
    }
    void Start()
    {
        UpdateMoneyText();
    }

    public void UpdateMoneyCount(int moneyDiff)
    {
        moneyCount += moneyDiff;
        UpdateMoneyText();
    }
    void UpdateMoneyText()
    {
        moneyText.text = moneyCount.ToString();
    }
    
    public int GetMoneyCount()
    {
        return moneyCount;
    }
 

    void OnEnemyDied()
    {
        UpdateMoneyCount(1);
    }
    private void OnDestroy()
    {
        EventManager.EnemyDied -= OnEnemyDied;
    }
}
