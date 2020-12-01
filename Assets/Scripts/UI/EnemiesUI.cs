using DemoShooter.Managers;
using TMPro;
using UnityEngine;

namespace DemoShooter.UI
{
    public class EnemiesUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text enemiesText;

        private void Start()
        {
            ChangeEnemiesText();
            EnemyManager.instance.OnEnemyListChanged += ChangeEnemiesText;
        }

        private void OnDestroy()
        {
            EnemyManager.instance.OnEnemyListChanged -= ChangeEnemiesText;
        }

        private void ChangeEnemiesText()
        {
            enemiesText.text = $"Enemies: {EnemyManager.instance.Enemies.Count.ToString()}";
        }
    }
}