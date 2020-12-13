using DemoShooter.Managers;
using DemoShooter.ShootingMechanic;
using UnityEngine;

namespace DemoShooter.Characters
{
    public class Enemy : Character
    {
        [SerializeField] private Weapon weapon;

        private void Start()
        {
            EnemyManager.instance.AddEnemy(this);
        }
        public override void Attack()
        {
            weapon.FireWeapon();
            Debug.Log("Attacking",gameObject);
        }
        private void OnDestroy()
        {
            EnemyManager.instance.RemoveEnemy(this);
        }
    }
}
