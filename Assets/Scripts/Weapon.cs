using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject ammoPrefab;
    [SerializeField] private GameObject ammoStartPoint;
    [SerializeField] private GameObject ammoHolder;
    [SerializeField] private float ammoVelocity = 50.0f;
    [SerializeField] private int ammoPoolSize = 30;
    [SerializeField] private float ammoActiveTime = 3f;
   

    private bool isFiring;
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
        isFiring = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isFiring = true;
            FireAmmo();
        }
        UpdateState();
    }

    private void UpdateState()
    {
        if (isFiring)
        {
            //TODO: изменение стейта аниматора
        }
    }

    private void FireAmmo()
    {
        //Создание луча между исходной точкой снаряда и конечной, которая зависит от направления камеры 
        Ray ray2 = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 targetPoint;
        if (Physics.Raycast(ray2, out hit))
        {
            targetPoint = hit.point;
            Debug.Log(hit.distance.ToString());
        }
        else
            targetPoint = ray2.GetPoint(100);


        GameObject bullet = SpawnAmmo(ammoStartPoint.transform.position, playerCamera.transform.rotation);
        //GameObject bullet = Instantiate(ammoPrefab, ammoStartPoint.transform.position, playerCamera.transform.rotation);

        bullet.GetComponent<Rigidbody>().velocity = (targetPoint - ammoStartPoint.transform.position).normalized * ammoVelocity;
        StartCoroutine(DeactivateOnThreshold(bullet, ammoActiveTime));
    }

    private GameObject SpawnAmmo(Vector3 spawnPosition,Quaternion spawnRotation)
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
    private IEnumerator DeactivateOnThreshold(GameObject activeObject,float thresholdSeconds)
    {
        yield return new WaitForSeconds(thresholdSeconds);
        if (activeObject.activeSelf)
            activeObject.SetActive(false);
    }

    private void OnDestroy()
    {
        ammoPool = null;
    }
}
