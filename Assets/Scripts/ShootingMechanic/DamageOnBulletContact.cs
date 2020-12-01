using DemoShooter.Characters;
using UnityEngine;

namespace DemoShooter.ShootingMechanic
{
    public class DamageOnBulletContact: MonoBehaviour, IDamageOnContact
    {
        public void Damage(float damage, Health health)
        {
            health.ChangeHealth(-damage);
        }
    }
    public interface IDamageOnContact
    {
        void Damage(float damage, Health health);
    }
}