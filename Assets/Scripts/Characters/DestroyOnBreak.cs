using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

namespace DemoShooter.Characters
{
    [RequireComponent(typeof(Health))]
    public class DestroyOnBreak : MonoBehaviour, IEffectOnBreak
    {
        [SerializeField] private float destroyDelay = 0f;
        private Health _objectHealth;

        private void Awake()
        {
            _objectHealth = GetComponent<Health>();
            _objectHealth.OnZeroHealth += OnObjectBreak;
        }

        private void OnDestroy()
        {
            _objectHealth.OnZeroHealth -= OnObjectBreak;
        }

        public void OnObjectBreak()
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