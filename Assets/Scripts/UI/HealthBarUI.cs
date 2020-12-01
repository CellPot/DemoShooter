using System;
using DemoShooter.Characters;
using UnityEngine;
using UnityEngine.UI;

namespace DemoShooter.UI
{
    public class HealthBarUI : MonoBehaviour
    {
        [SerializeField] private Health targetHealth;
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private void Start()
        {
            targetHealth.OnHealthChange += ChangeFillAmount;
        }
        private void ChangeFillAmount()
        {
            _image.fillAmount = targetHealth.HealthValue / targetHealth.MaxHealthValue;
        }

        private void OnDestroy()
        {
            targetHealth.OnHealthChange -= ChangeFillAmount;
        }
    }
}