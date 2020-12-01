using System;
using DemoShooter.GameSystems;
using DemoShooter.ShootingMechanic;
using UnityEngine;

namespace DemoShooter.Characters
{
    public class Player : Character
    {
        [SerializeField] private InputSystem inputSystem;
        [SerializeField] private RifleWeapon playerWeapon;

        protected override void Awake()
        {
            base.Awake();
            if (playerWeapon == null)
                Debug.LogWarning("Weapon obj isn't attached",this);
        }

        private void Start()
        {
            inputSystem.OnPlayerShot += Attack;
        }

        private void OnDestroy()
        {
            inputSystem.OnPlayerShot -= Attack;
        }

        public override void Attack()
        {
            playerWeapon.FireWeapon();
        }


    



    }
}
