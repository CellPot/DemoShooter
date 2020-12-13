using System;
using UnityEngine;

namespace DemoShooter.ShootingMechanic
{
    
    public class SoundOnShoot : MonoBehaviour
    {
        [SerializeField] private AudioSource shootSource;
        [SerializeField] private Weapon weapon;

        private void Awake()
        {
            if (shootSource==null)
                shootSource = GetComponent<AudioSource>();
            if (weapon == null)
                weapon = GetComponent<Weapon>();
        }

        private void Start()
        {
            weapon.OnShotFired += PlayAudio;
        }

        private void OnDestroy()
        {
            weapon.OnShotFired -= PlayAudio;
        }

        private void PlayAudio()
        {
            shootSource.Play();
        }
    }
}