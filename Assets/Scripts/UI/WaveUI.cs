using System;
using DemoShooter.Managers;
using TMPro;
using UnityEngine;

namespace DemoShooter.UI
{
    public class WaveUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text waveText;

        private void Start()
        {
            ChangeWaveText();
            WaveManager.instance.OnWavesListChanged += ChangeWaveText;
        }

        private void OnDestroy()
        {
            WaveManager.instance.OnWavesListChanged -= ChangeWaveText;
        }

        private void ChangeWaveText()
        {
            waveText.text = $"Waves: {WaveManager.instance.Waves.Count.ToString()}";
        }
    }
}