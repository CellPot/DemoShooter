using System;
using DemoShooter.Managers;
using UnityEngine;

namespace DemoShooter.Characters
{
    [RequireComponent(typeof(Health))]
    public class ScoreOnBreak : MonoBehaviour
    {
        [SerializeField] private int amount;
        public int Amount { get => amount; private set => amount = value; }
        private Health _objectHealth;

        private void Awake()
        {
            _objectHealth = GetComponent<Health>();
            _objectHealth.OnZeroHealth += ChangeScore;
        }

        private void OnDestroy()
        {
            _objectHealth.OnZeroHealth -= ChangeScore;
        }

        private void ChangeScore()
        {
            ScoreManager.instance.ScoreAmount += Amount;
        }
    }
}