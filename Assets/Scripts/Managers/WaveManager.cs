using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    [SerializeField] private List<WaveSpawner> _waves;
    public IReadOnlyList<WaveSpawner> Waves { get => _waves; }
    public UnityEvent onChanged;

    private void Awake()
    {
        _waves = new List<WaveSpawner>();
        if (instance == null)
            instance = this;
        else
            Debug.LogError("Duplicated WaveManager", gameObject);
    }
    public void AddWave(WaveSpawner wave)
    {
        _waves.Add(wave);
        onChanged.Invoke();
    }
    public void RemoveWave(WaveSpawner wave)
    {
        _waves.Remove(wave);
        onChanged.Invoke();
    }
}
