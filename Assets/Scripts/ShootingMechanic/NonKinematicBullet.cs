using System.Collections;
using DemoShooter.Characters;
using UnityEngine;

namespace DemoShooter.ShootingMechanic
{
    [RequireComponent(typeof(Rigidbody))] 
    public class NonKinematicBullet : Ammo
    {
        private IDamageOnContact _damageOnContact;
        protected override void Awake()
        {
            base.Awake();
            _damageOnContact = gameObject.GetComponent<IDamageOnContact>();
        }
        public override void FireAmmo(Vector3 targetVector, float secondsUntilInactive, float velocityMod = 1)
        {
            float modifiedVelocity = AmmoVelocity * velocityMod;
            StartCoroutine(MoveNonKinematicCoroutine(targetVector, modifiedVelocity));
            StartCoroutine(deactivation.DeactivationCoroutine(gameObject,secondsUntilInactive));
        }

        private IEnumerator MoveNonKinematicCoroutine(Vector3 targetVector, float modifiedVelocity)
        {
            ammoRigidbody.velocity = targetVector * (modifiedVelocity);
            // yield return new WaitForFixedUpdate();
            yield return null;
        }
        private void OnCollisionEnter(Collision other)
        {
            Health otherHealth = other.gameObject.GetComponent<Health>();
            if (otherHealth != null)
            {
                _damageOnContact.Damage(DamageInflicted, otherHealth);
                Debug.Log($"{otherHealth.gameObject}'s health: {otherHealth.HealthValue}/{otherHealth.MaxHealthValue}");
            }
            gameObject.SetActive(false);
        }

    }
}