using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace DemoShooter.ShootingMechanic
{
    public class Weapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private Ammo ammoPrefab;
        [Tooltip("Ammo's initial rotation object. Should be camera for player")]
        [SerializeField] private Transform ammoInitialRotationReference;
        [SerializeField] private Transform ammoInitialPositionReference;
        
        [Tooltip("Corrects target hit ray so that it hits into camera's viewport center")]
        [SerializeField] private bool ignoreWeaponsDirection = true;
        [Tooltip("Multiplies ammo's initial velocity")]
        [SerializeField] private float ammoVelocityModifier = 1;
        [SerializeField] private int ammoPoolSize = 30;
        [SerializeField] private float ammoActiveTime = 3f;
        [SerializeField] private bool isAmmoLimited = true;
        [SerializeField] private int totalAmmo = 500;
        [SerializeField] private float fireRate = 0.2f;
        
        
        private IDestinationCalculator _hitVectorCalculator;
        private IAmmoPoolManager _ammoPoolManager;
        private List<Ammo> _ammoPool;
        private bool _isFiring = false;
        private float _lastShotTime;
        public int TotalAmmo => totalAmmo;

        public delegate void ShotFiredHandler();
        public event ShotFiredHandler OnShotFired;

        private void Awake()
        {
            _ammoPool = new List<Ammo>();
        }

        private void Start()
        {
            _ammoPoolManager = gameObject.GetComponent<IAmmoPoolManager>();
            _hitVectorCalculator = gameObject.GetComponent<IDestinationCalculator>();
            _ammoPool = _ammoPoolManager.CreateAmmoPool(ammoPrefab, ammoPoolSize);
        }

        private void FixedUpdate()
        {
            if (_isFiring)
            {
                ShootAmmo();
                _isFiring = false;
            }
        }

        public void FireWeapon()
        {
            if (TotalAmmo > 0 || !isAmmoLimited)
            {
                _isFiring = true;
            }
        }


        private void ShootAmmo()
        {
            var timeSinceLastShot = Time.time - _lastShotTime;
            if (timeSinceLastShot < fireRate)
                return;
            _lastShotTime = Time.time;
            
            OnShotFired?.Invoke();
            SpendAmmo();
            Ammo ammo = _ammoPoolManager.PullAmmo(_ammoPool, ammoInitialPositionReference.position, 
                ammoInitialRotationReference.rotation);
            Vector3 targetVector;
            if (!ignoreWeaponsDirection)
                targetVector = gameObject.transform.forward;
            else
                targetVector = GetVectorDifference(_hitVectorCalculator.GetHitVector(),ammo.AmmoPosition).normalized;
            ammo.FireAmmo(targetVector, ammoActiveTime, ammoVelocityModifier);
            
        }

        private static Vector3 GetVectorDifference(Vector3 a, Vector3 b)
        {
            return a - b;
        }
        private void SpendAmmo()
        {
            totalAmmo--;
        }
        private void OnDestroy()
        {
            foreach (Ammo ammo in _ammoPool)
            {
                if (ammo!=null)
                    Destroy(ammo.gameObject);
            }
            // _ammoPool = null;
        }
    }
}