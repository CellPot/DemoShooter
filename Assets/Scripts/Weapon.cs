using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Essential Components")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Ammo ammoPrefab;
    [SerializeField] private GameObject ammoStartPoint;
    [SerializeField] private GameObject ammoHolder;
    [Space]
    [Tooltip("Point bullets based on camera's center")]
    [SerializeField] private bool cameraCenterCorrection = true;
    [SerializeField] private float ammoVelocity = 50.0f;
    [SerializeField] private int ammoPoolSize = 30;
    [SerializeField] private float ammoActiveTime = 3f;
   
    
    private bool _isFiring;
    private static List<Ammo> ammoPool;

    public delegate void FireWeapon();
    public event FireWeapon Fired;

    private void Awake()
    {
        if (!playerCamera)
            playerCamera = Camera.main;
        if (ammoPool == null)
            ammoPool = new List<Ammo>();
        ammoPool = CreateAmmoPool(ammoPoolSize);
    }
    private void Start()
    {
        _isFiring = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _isFiring = true;
            FireAmmo();
        }
        UpdateState();
    }

    
    private void FireAmmo()
    {
        Ammo bullet = SpawnAmmo(ammoStartPoint.transform.position, playerCamera.transform.rotation);
        Vector3 velocityVector;
        if (cameraCenterCorrection)
        {
            Vector3 targetPoint = GetTargetPoint();
            velocityVector = (targetPoint - bullet.BulletPosition).normalized;
        }
        else
            velocityVector = playerCamera.transform.forward;
        bullet.Fire(velocityVector, ammoVelocity, ammoActiveTime);
    }

    private Ammo SpawnAmmo(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        foreach (Ammo bullet in ammoPool)
        {
            if (bullet.ActiveSelf == false)
            {
                bullet.SetActive(true, spawnPosition, spawnRotation);
                return bullet;
            }
        }
        Ammo newAmmo = Instantiate(ammoPrefab, ammoHolder.transform);
        ammoPool.Add(newAmmo);
        newAmmo.SetActive(true, spawnPosition, spawnRotation);
        return newAmmo;
    }
    private Vector3 GetTargetPoint(bool takeCameraIntoAccount = true)
    {
        //Создание луча между исходной точкой снаряда и конечной, где последняя зависит от центра камеры 
        Ray ray2 = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 rayHitPoint = ray2.GetPoint(100);
        if (takeCameraIntoAccount)
        {
            RaycastHit hit;
            if (Physics.Raycast(ray2, out hit))
            {
                Debug.Log($"Did hit {rayHitPoint} at distance {hit.distance}" );
                rayHitPoint = hit.point;
            }
        }
        return rayHitPoint;
    }

    private List<Ammo> CreateAmmoPool(int poolSize)
    {
        List<Ammo> ammoList = new List<Ammo>();
        for (int i = 0; i < poolSize; i++)
        {
            Ammo ammoObject = Instantiate<Ammo>(ammoPrefab, ammoHolder.transform);
            ammoObject.SetActive(false);
            ammoList.Add(ammoObject);
        }
        return ammoList;
    }
    private void UpdateState()
    {
        if (_isFiring)
        {
            //TODO: изменение стейта аниматора
        }
    }

    private void OnDestroy()
    {
        ammoPool = null;
    }
}
