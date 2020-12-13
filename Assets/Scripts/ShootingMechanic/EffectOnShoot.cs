using System;
using DemoShooter.ShootingMechanic;
using UnityEngine;


namespace DemoShooter.ShootingMechanic
{
    [RequireComponent(typeof(Weapon))]
    public class EffectOnShoot : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particleSys;
        private Weapon _weapon;

        private void Awake()
        {
            _weapon = GetComponent<Weapon>();
        }

        private void Start()
        {
            _weapon.OnShotFired += PlayEffect;
        }

        private void OnDestroy()
        {
            _weapon.OnShotFired -= PlayEffect;
        }

        private void PlayEffect()
        {
            particleSys.Play();
        }
    }
}