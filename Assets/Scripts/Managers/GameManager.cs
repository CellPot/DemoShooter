using System;
using DemoShooter.Characters;
using DemoShooter.GameSystems;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DemoShooter.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Health playerHealth;
        [SerializeField] private Health playerBaseHealth;
        [SerializeField] private InputSystem inputSystem;

        public delegate void GameFinishHandler();
        public event GameFinishHandler OnGameWon;
        public event GameFinishHandler OnGameLost;

        private bool _isFinished = false;
        
        public static GameManager instance;
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Debug.LogWarning("Duplicated GameManager is ignored", gameObject);
        }

        private void Start()
        {
            playerHealth.OnZeroHealth += OnPlayerZeroHealth;
            playerBaseHealth.OnZeroHealth += OnBaseZeroHealth;
            EnemyManager.instance.OnEnemyListChanged += WinningStateCheck;
            WaveManager.instance.OnWavesListChanged += WinningStateCheck;
        }

        private void Update()
        {
            if (!_isFinished) return;
            inputSystem.BlockControlsInput(true,true);
        }

        private void OnDestroy()
        {
            playerHealth.OnZeroHealth -= OnPlayerZeroHealth;
            playerBaseHealth.OnZeroHealth -= OnBaseZeroHealth;
            EnemyManager.instance.OnEnemyListChanged -= WinningStateCheck;
            WaveManager.instance.OnWavesListChanged -= WinningStateCheck;
        }

        private void OnPlayerZeroHealth()
        {
            if (_isFinished) return;
            OnGameLost?.Invoke();
            Debug.Log("Game's lost: player's health hit 0");
            _isFinished = true;
        }

        private void OnBaseZeroHealth()
        {
            if (_isFinished) return;
            OnGameLost?.Invoke();
            Debug.Log("Game's lost: Base's health hit 0");
            _isFinished = true;
        }
        
        private void WinningStateCheck()
        {
            if (_isFinished) return;
            if (EnemyManager.instance.Enemies.Count > 0 || WaveManager.instance.Waves.Count > 0) return;
            OnGameWon?.Invoke();
            Debug.Log("Game's won: 0 enemies and waves");
            _isFinished = true;
        }
        
    }
}
