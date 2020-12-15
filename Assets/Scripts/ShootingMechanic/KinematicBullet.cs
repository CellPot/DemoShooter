using System;
using System.Collections;
using DemoShooter.Characters;
using UnityEngine;

namespace DemoShooter.ShootingMechanic
{
    [RequireComponent(typeof(Rigidbody))] 
    public class KinematicBullet : Ammo
    {
        private IDamageOnContact _damageOnContact;
        protected override void Awake()
        {
            base.Awake();
            _damageOnContact = gameObject.GetComponent<IDamageOnContact>();
        }


        public override void FireAmmo(Vector3 targetVector, float secondsUntilInactive, float velocityMod = 1)
        {
            StartCoroutine(MoveAtKinematicCoroutine(targetVector,velocityMod));
            StartCoroutine(deactivation.DeactivationCoroutine(gameObject,secondsUntilInactive));
        }
        private IEnumerator MoveAtKinematicCoroutine(Vector3 targetVector, float velocityMod)
        {
            var modifiedVelocity = AmmoVelocity * velocityMod;
            var positionDeltaVector = targetVector * (modifiedVelocity * Time.deltaTime);
            while (gameObject.activeSelf)
            {
                ammoRigidbody.MovePosition(transform.position + positionDeltaVector);
                yield return new WaitForFixedUpdate();
            }
            yield return null;
        }

        private void OnTriggerEnter(Collider other)
        {
            var otherHealth = other.gameObject.GetComponent<Health>();
            if (otherHealth != null)
            {
                _damageOnContact.Damage(DamageInflicted, otherHealth);
                
            }
            gameObject.SetActive(false);
        }

    }
}