using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int scoreOnDeathAmount;
    Life life;

    private void Awake()
    {
        life = GetComponent<Life>();
        life.onDeath.AddListener(GivePoints);
    }
    private void Start()
    {
        EnemyManager.instance.AddEnemy(this);
    }
    private void OnDestroy()
    {
        EnemyManager.instance.RemoveEnemy(this);
        life.onDeath.RemoveListener(Awake);
    }
    void GivePoints()
    {
        ScoreManager.instance.Amount += scoreOnDeathAmount;
    }
}
