using System.Collections;
using DemoShooter.Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace DemoShooter.Characters
{
    public class WaveSpawner : MonoBehaviour
    {
    
        [SerializeField] private GameObject prefabToSpawn;
        [SerializeField] private float startDelay;
        [SerializeField] private float timeLimit;
        [SerializeField] private float spawnRate;
        [SerializeField] private bool canSpawn = true;

        private void Start()
        {
            WaveManager.instance.AddWave(this);
            StartSpawning();
        }

        private void StartSpawning()
        {
            if (canSpawn)
                StartCoroutine(SpawnCoroutine());
        }

        private IEnumerator SpawnCoroutine()
        {
            yield return new WaitForSeconds(startDelay);
            float initialTime = Time.time;
            float totalTime = 0;
            while (!TimeIsUp(totalTime,timeLimit))
            {
                GameObject spawnedObject = Instantiate(prefabToSpawn, transform.position, transform.rotation);
                totalTime = Time.time - initialTime;
                if (TimeIsUp(totalTime,timeLimit))
                    break;
                yield return new WaitForSeconds(spawnRate);
                totalTime = Time.time - initialTime;
            }
            WaveManager.instance.RemoveWave(this);
            yield return null;
        }

        private bool TimeIsUp(float total, float limit)
        {
            return total > limit;
        }
    }
}
