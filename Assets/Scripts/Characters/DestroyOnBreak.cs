using System;
using UnityEngine;

namespace DemoShooter.Characters
{
    [RequireComponent(typeof(Health))]
    public class DestroyOnBreak : MonoBehaviour
    {
        private Health _objectHealth;

        private void Awake()
        {
            _objectHealth = GetComponent<Health>();
            _objectHealth.OnZeroHealth += DestroyObject;
        }

        private void OnDestroy()
        {
            _objectHealth.OnZeroHealth -= DestroyObject;
        }

        private void DestroyObject()
        {
            Destroy(gameObject);
        }
    }
}