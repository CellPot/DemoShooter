using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.Internal;

public class WaveSpawner : MonoBehaviour
{
    
    [SerializeField] private GameObject prefab;
    [SerializeField] private float startTime;
    [SerializeField] private float endTime;
    [SerializeField] private float spawnRate;

    private void Start()
    {
        StartCoroutine(Co_Spawn(startTime,prefab,spawnRate,endTime));
    }

    private IEnumerator Co_Spawn(float startDelay, GameObject prefabToSpawn, float spawnDelay, float timeLimit)
    {
        yield return new WaitForSeconds(startDelay);
        float totalTime = Time.time;
        while (totalTime < timeLimit)
        {
            GameObject spawnedObject = Instantiate(prefabToSpawn, transform.position, transform.rotation);
            yield return new WaitForSeconds(spawnDelay);
            totalTime = Time.time;
        }
        yield return null;
    }
}
