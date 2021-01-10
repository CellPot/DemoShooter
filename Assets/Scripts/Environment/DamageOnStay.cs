using System;
using DemoShooter.Characters;
using DemoShooter.ShootingMechanic;
using UnityEngine;

namespace DemoShooter.Environment
{
    public class DamageOnStay : MonoBehaviour
    {
        private IDamageOnContact _damageOnContact;
        [SerializeField] private int damageInflicted = 0;
        private void Awake()
        {
            _damageOnContact = gameObject.GetComponent<IDamageOnContact>();
        }

        private void OnTriggerStay(Collider other)
        {
            var otherHealth = other.gameObject.GetComponent<Health>();
            if (otherHealth != null)
            {
                _damageOnContact.Damage(damageInflicted, otherHealth);
            }
        }
    }
}