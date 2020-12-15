using UnityEngine;

namespace DemoShooter.Characters
{
    [RequireComponent(typeof(Health))]
    public class ParticleOnBreak : MonoBehaviour, IEffectOnBreak
    {
        [SerializeField] private GameObject particlePrefab;
        [SerializeField] private Transform effectInitialPoint;
        private Health _objectHealth;

        private void Awake()
        {
            if (effectInitialPoint == null)
                effectInitialPoint = this.gameObject.transform;
            _objectHealth = GetComponent<Health>();
            _objectHealth.OnZeroHealth += OnObjectBreak;
        }
        private void OnDestroy()
        {
            _objectHealth.OnZeroHealth -= OnObjectBreak;
        }

        public void OnObjectBreak()
        {
            Instantiate(particlePrefab, effectInitialPoint.position, effectInitialPoint.rotation);
        }
    }
}
