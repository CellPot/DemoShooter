using System;
using DemoShooter.Extensions;
using UnityEngine;
using UnityEngine.Serialization;

namespace DemoShooter.EffectControllers
{
    public class ParticleOnHit : MonoBehaviour, IEffectOnEvent
    {
        [SerializeField] private LayerMask layersToTrigger;
        [SerializeField] private ParticleSystem particlePrefab;

        private void OnCollisionEnter(Collision other)
        {
            if (!layersToTrigger.Includes(other.collider.gameObject.layer)) return;
            ActivateEffect();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!layersToTrigger.Includes(other.gameObject.layer)) return;
            ActivateEffect();
        }

        public void ActivateEffect()
        {
            Instantiate(particlePrefab, transform.position, transform.rotation);
        }
    }
}
