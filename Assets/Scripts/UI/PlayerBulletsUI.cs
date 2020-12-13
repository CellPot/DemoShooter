using System;
using DemoShooter.ShootingMechanic;
using TMPro;
using UnityEngine;

namespace DemoShooter.UI
{
    public class PlayerBulletsUI : MonoBehaviour
    {
        [SerializeField] private Weapon weapon;
        [SerializeField] private TMP_Text bulletsText;

        private void Start()
        {
            ChangeBulletsText();
            weapon.OnShotFired += ChangeBulletsText;
        }

        private void OnDestroy()
        {
            weapon.OnShotFired -= ChangeBulletsText;
        }

        private void ChangeBulletsText()
        {
            bulletsText.text = $"Bullets: {weapon.TotalAmmo.ToString()}";
        }
    }
}