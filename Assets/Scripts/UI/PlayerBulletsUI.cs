using System;
using DemoShooter.ShootingMechanic;
using TMPro;
using UnityEngine;

namespace DemoShooter.UI
{
    public class PlayerBulletsUI : MonoBehaviour
    {
        [SerializeField] private RifleWeapon weapon;
        [SerializeField] private TMP_Text bulletsText;

        private void Start()
        {
            ChangeBulletsText();
            weapon.OnAmmoSpent += ChangeBulletsText;
        }

        private void OnDestroy()
        {
            weapon.OnAmmoSpent -= ChangeBulletsText;
        }

        private void ChangeBulletsText()
        {
            bulletsText.text = $"Bullets: {weapon.TotalAmmo.ToString()}";
        }
    }
}