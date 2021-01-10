using DemoShooter.ShootingMechanic;
using UnityEngine;

namespace DemoShooter.Characters
{
    [RequireComponent(typeof(Health))]
    public abstract class Character : MonoBehaviour, ICanAttack
    {
        private Health _objHealth;
        protected virtual void Awake()
        {
            _objHealth = GetComponent<Health>();
        }

        public abstract void Attack();
    }
}