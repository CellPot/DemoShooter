using System;
using UnityEngine;

namespace DemoShooter.Characters
{
    public class Health: MonoBehaviour
    {
        [SerializeField] private float healthValue = 100f;
        [SerializeField] private float maxHealthValue = 100f;

        public delegate void ZeroHealthHandler();
        public event ZeroHealthHandler OnZeroHealth;
        //public UnityEvent onDeath;
        public float HealthValue
        {
            get => healthValue;
            set
            {
                healthValue = value >= 0f ? Mathf.Min(value, maxHealthValue) : 0f;
                ////
                if (healthValue <= 0f)
                {
                    OnZeroHealth?.Invoke();
                }
                ////
            }
        }

        public float MaxHealthValue
        {
            get => maxHealthValue;
            set
            {
                maxHealthValue = Mathf.Max(value, 0f);
                healthValue = Mathf.Min(healthValue, maxHealthValue);
            }
        }
        private void Awake()
        {
            OnValidate();
        }

        public void ChangeHealth(float healthDelta)
        {
            HealthValue += healthDelta;
        }
        public void ChangeHealth(float healthDelta, float maxDelta)
        {
            ChangeMaxHealth(maxDelta);
            ChangeHealth(healthDelta);
        }
        public void ChangeMaxHealth(float maxDelta)
        {
            MaxHealthValue += maxDelta;
        }
        private void OnValidate()
        {
            MaxHealthValue = maxHealthValue;
            HealthValue = healthValue;
            maxHealthValue = MaxHealthValue;
            healthValue = HealthValue;
            // if (_scoreOnDeathGiver != null)
            //     _scoreOnDeathGiver.ScoreOnDestroy = _scoreOnDeath;
        }
    }
}
