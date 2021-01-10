using System;
using System.Collections;
using System.Security.Cryptography;
using DemoShooter.EffectControllers;
using UnityEngine;

namespace DemoShooter.Characters
{
    [RequireComponent(typeof(Health))]
    public class DestroyOnBreak : MonoBehaviour, IEffectOnEvent
    {
        [SerializeField] private float destroyDelay = 0f;
        private Health _objectHealth;

        private void Awake()
        {
            _objectHealth = GetComponent<Health>();
            _objectHealth.OnZeroHealth += ActivateEffect;
        }

        private void OnDestroy()
        {
            _objectHealth.OnZeroHealth -= ActivateEffect;
        }

        public void ActivateEffect()
        {
            StartCoroutine(DestroyCoroutine());
        }

        private IEnumerator DestroyCoroutine()
        {
            yield return new WaitForSeconds(destroyDelay);
            Destroy(gameObject);
            yield return null;
        }
    }
}