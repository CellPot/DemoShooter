using System;
using System.Collections.Generic;
using DemoShooter.Characters;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace DemoShooter.Managers
{
    public class EnemyManager : MonoBehaviour
    {
        public delegate void EnemyListChangeHandler();
        public event EnemyListChangeHandler OnEnemyListChanged;
        
        public static EnemyManager instance;

        [SerializeField] private List<Enemy> enemies;
        public List<Enemy> Enemies => enemies;
        

        private void Awake()
        {
            enemies = new List<Enemy>();
            if (instance == null)
                instance = this;
            else
                Debug.LogWarning("Duplicated EnemyManager is ignored", gameObject);
        }
        public void AddEnemy(Enemy enemy)
        {
            enemies.Add(enemy);
            OnEnemyListChanged?.Invoke();
        }
        public void RemoveEnemy(Enemy enemy)
        {
            enemies.Remove(enemy);
            OnEnemyListChanged?.Invoke();
        }
    }
}
