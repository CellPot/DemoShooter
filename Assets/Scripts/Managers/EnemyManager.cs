using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [SerializeField] private List<Enemy> _enemies;
    public List<Enemy> Enemies { get => _enemies; }
    public UnityEvent onChanged;

    private void Awake()
    {
        _enemies = new List<Enemy>();
        if (instance == null)
            instance = this;
        else
            Debug.LogError("Duplicated EnemyManager", gameObject);
    }
    public void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
        onChanged.Invoke();
    }
    public void RemoveEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
        onChanged.Invoke();
    }
}
