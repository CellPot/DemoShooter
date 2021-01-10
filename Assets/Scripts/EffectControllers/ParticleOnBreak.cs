using DemoShooter.Characters;
using UnityEngine;

namespace DemoShooter.EffectControllers
{
    [RequireComponent(typeof(Health))]
    public class ParticleOnBreak : MonoBehaviour, IEffectOnEvent
    {
        [SerializeField] private GameObject particlePrefab;
        [SerializeField] private Transform effectInitialPoint;
        [SerializeField] private bool didPlay = false;
        private Health _objectHealth;

        private void Awake()
        {
            if (effectInitialPoint == null)
                effectInitialPoint = this.gameObject.transform;
            _objectHealth = GetComponent<Health>();
            _objectHealth.OnZeroHealth += ActivateEffect;
        }
        private void OnDestroy()
        {
            _objectHealth.OnZeroHealth -= ActivateEffect;
        }

        public void ActivateEffect()
        {
            if (!didPlay)
            {
                Instantiate(particlePrefab, effectInitialPoint.position, effectInitialPoint.rotation);
                didPlay = true;
            }
        }
    }
}
