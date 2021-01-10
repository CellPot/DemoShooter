using DemoShooter.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace DemoShooter.UI
{
    public class OptionsMenuUI : MonoBehaviour
    {
        [SerializeField] private Slider masterSlider;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider sfxSlider;

        private void Awake()
        {
            masterSlider.onValueChanged.AddListener(SetMasterVolume);
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
            sfxSlider.onValueChanged.AddListener(SetSfxVolume);
        }

        private void OnDestroy()
        {
            masterSlider.onValueChanged.RemoveListener(SetMasterVolume);           
            musicSlider.onValueChanged.RemoveListener(SetMusicVolume);
            sfxSlider.onValueChanged.RemoveListener(SetSfxVolume);
        }

        private void SetMasterVolume(float value)
        {
            OptionsManager.instance.AudioMix.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
        }

        private void SetMusicVolume(float value)
        {
            OptionsManager.instance.AudioMix.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
        }
        private void SetSfxVolume(float value)
        {
            OptionsManager.instance.AudioMix.SetFloat("SFXVolume", Mathf.Log10(value) * 20);
        }
    }
}