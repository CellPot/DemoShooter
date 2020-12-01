using System.Collections.Generic;
using DemoShooter.Characters;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace DemoShooter.Managers
{
    public class WaveManager : MonoBehaviour
    {
        public delegate void WavesListChangedHandler();
        public event WavesListChangedHandler OnWavesListChanged;
        
        public static WaveManager instance;

        [SerializeField] private List<WaveSpawner> waves;
        public List<WaveSpawner> Waves => waves;

        private void Awake()
        {
            waves = new List<WaveSpawner>();
            if (instance == null)
                instance = this;
            else
                Debug.LogError("Duplicated WaveManager is ignored", gameObject);
        }
        public void AddWave(WaveSpawner wave)
        {
            waves.Add(wave);
            OnWavesListChanged?.Invoke();
        }
        public void RemoveWave(WaveSpawner wave)
        {
            waves.Remove(wave);
            OnWavesListChanged?.Invoke();
        }
    }
}
