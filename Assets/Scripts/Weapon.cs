using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Essential Components")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject ammoPrefab;
    [SerializeField] private GameObject ammoStartPoint;
    [SerializeField] private GameObject ammoHolder;
    [Space]
    [SerializeField] private float ammoVelocity = 50.0f;
    [SerializeField] private int ammoPoolSize = 30;
    [SerializeField] private float ammoActiveTime = 3f;
   

    private bool _isFiring;
    private static List<GameObject> ammoPool;

    private void Awake()
    {
        if (ammoPool == null)
            ammoPool = new List<GameObject>();

        for (int i = 0; i< ammoPoolSize; i++)
        {
            GameObject ammoObject = Instantiate(ammoPrefab, ammoHolder.transform);
            ammoObject.SetActive(false);
            ammoPool.Add(ammoObject);
        }
        if (!playerCamera)
            playerCamera = Camera.main;
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

    private void UpdateState()
    {
        if (_isFiring)
        {
            //TODO: изменение стейта аниматора
        }
    }
    private void FireAmmo()
    {
        Vector3 targetPoint = GetTargetPoint();
        GameObject bullet = SpawnAmmo(ammoStartPoint.transform.position, playerCamera.transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = BulletVelocity(targetPoint);
        StartCoroutine(Co_DeactivateOnThreshold(bullet, ammoActiveTime));        
    }
    private GameObject SpawnAmmo(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        foreach (GameObject bullet in ammoPool)
        {
            if (bullet.activeSelf == false)
            {
                bullet.SetActive(true);
                bullet.transform.position = spawnPosition;
                bullet.transform.rotation = spawnRotation;
                return bullet;
            }
        }
        return null;
    }
    private Vector3 GetTargetPoint()
    {
        //Создание луча между исходной точкой снаряда и конечной, которая зависит от направления камеры 
        Ray ray2 = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray2, out hit))
        {
            Debug.Log(hit.point.ToString());
            return hit.point;
        }
        else
            return ray2.GetPoint(100);
    }
    private Vector3 BulletVelocity(Vector3 targetPoint)
    {
        Vector3 velocity =  (targetPoint - ammoStartPoint.transform.position).normalized * ammoVelocity;
        return velocity;
    }
    private IEnumerator Co_DeactivateOnThreshold(GameObject activeObject,float thresholdSeconds)
    {
        yield return new WaitForSeconds(thresholdSeconds);
        if (activeObject.activeSelf)
            activeObject.SetActive(false);
        yield return null;
    }

    private void OnDestroy()
    {
        ammoPool = null;
    }
}
