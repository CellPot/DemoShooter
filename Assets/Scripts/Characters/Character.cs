using DemoShooter.ShootingMechanic;
using UnityEngine;

namespace DemoShooter.Characters
{
    [RequireComponent(typeof(Health))]
    public abstract class Character : MonoBehaviour, ICanAttack
    {
        private Health _objHealth;
        // private IScore _scoreOnDeathGiver;
        protected virtual void Awake()
        {
            _objHealth = GetComponent<Health>();
        }
        public virtual void OnDamage(float damageDealt)
        {
            _objHealth.ChangeHealth(-damageDealt);
        }

        public abstract void Attack();
    }
}