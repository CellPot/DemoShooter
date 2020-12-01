using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DemoShooter.ShootingMechanic
{
    public class RifleWeapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private Ammo ammoPrefab;
        [SerializeField] private Camera playerCamera;
        [Tooltip("Transform to reference for target vector projecting")]
        [SerializeField] private Transform referencePosition;
        [SerializeField] private Transform ammoInitialPoint;
        [SerializeField] private Transform ammoNestingObject;
        
        [Tooltip("Corrects target hit ray so that it hits into camera's viewport center")]
        [SerializeField] private bool cameraCenterCorrection = true;
        [Tooltip("Corrects hit position according to distance from camera if camera's center correction is enabled.")]
        [SerializeField] private bool takeDistanceIntoAccount = true;
        [SerializeField] private float ammoVelocityModifier = 1;
        [SerializeField] private int ammoPoolSize = 30;
        [SerializeField] private float ammoActiveTime = 3f;
        [SerializeField] private int totalAmmo = 10;
        
        private IDestinationCalculator _destinationCalc;
        private IAmmoPoolManager _ammoPoolManager;
        private static List<Ammo> _ammoPool;
        private bool _isFiring = false;

        public int TotalAmmo => totalAmmo;

        public delegate void AmmoSpendHandler();
        public event AmmoSpendHandler OnAmmoSpent;

        private void Awake()
        {
            if (playerCamera == null)
                playerCamera = Camera.main;
            _ammoPoolManager = gameObject.GetComponent<IAmmoPoolManager>();
            _destinationCalc = gameObject.GetComponent<IDestinationCalculator>();
            _ammoPool = new List<Ammo>();
            _ammoPool = _ammoPoolManager.CreateAmmoPool(ammoPrefab, ammoPoolSize, ammoNestingObject);
        }

        private void Start()
        {
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
            if (totalAmmo > 0)
            {
                _isFiring = true;
            }
        }

        private void ShootAmmo()
        {
            SpendAmmo();
            Ammo ammo = _ammoPoolManager.PullAmmo(_ammoPool, ammoInitialPoint.position, playerCamera.transform.rotation);
            Vector3 targetVector;
            if (cameraCenterCorrection)
                targetVector = (_destinationCalc.GetDestinationVector(playerCamera, takeDistanceIntoAccount)
                                - ammo.AmmoPosition).normalized;
            else
                targetVector = referencePosition.forward;
            ammo.FireAmmo(targetVector, ammoActiveTime, ammoVelocityModifier);
        }

        private void SpendAmmo()
        {
            totalAmmo--;
            OnAmmoSpent?.Invoke();
        }
        

        private void OnDestroy()
        {
            _ammoPool = null;
        }
    }
}