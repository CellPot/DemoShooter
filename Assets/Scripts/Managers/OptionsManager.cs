using System;
using UnityEngine;
using UnityEngine.Audio;

namespace DemoShooter.Managers
{
    public class OptionsManager : MonoBehaviour
    {
        public static OptionsManager instance;
        [SerializeField] private GameObject optionsMenu;
        [SerializeField] private AudioMixer audioMixer;
        public AudioMixer AudioMix => audioMixer;
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Debug.LogWarning("Duplicated PauseManager is ignored", gameObject);
            if (optionsMenu !=null)
                optionsMenu.SetActive(false);
        }

        private void Start()
        {
            PauseManager.instance.OnPauseToggle += ToggleMenuOnPause;
        }

        private void OnDestroy()
        {
            PauseManager.instance.OnPauseToggle -= ToggleMenuOnPause;
        }

        private void ToggleMenuOnPause(bool state)
        {
            if (state == false)
                optionsMenu.SetActive(false);
        }
        
    }
}