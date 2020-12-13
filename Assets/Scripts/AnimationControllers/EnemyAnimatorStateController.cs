using System.Collections;
using DemoShooter.Movement;
using DemoShooter.ShootingMechanic;
using UnityEngine;

namespace DemoShooter.AnimationControllers
{
    public class EnemyAnimatorStateController : MonoBehaviour
    {
        [SerializeField] private Weapon weapon;
        [SerializeField] private float shootingStateTime = 0.5f;
        [SerializeField] private bool isShootingStateContinuous = true;
        private Animator _animator;
        private EnemyMovementController _movementController;
        private Coroutine _lastShootingStateRoutine;

        private void Awake()
        {
            _animator = gameObject.GetComponent<Animator>();
            _movementController = gameObject.GetComponent<EnemyMovementController>();
            if (weapon == null)
                weapon = gameObject.GetComponentInChildren<Weapon>();
        }

        private void Start()
        {
            weapon.OnShotFired += SetShootingState;
        }

        private void OnDestroy()
        {
            weapon.OnShotFired -= SetShootingState;
        }

        private void Update()
        {
            SetHorizontalVelocityState();
            // SetShootingState();
        }

        private void SetHorizontalVelocityState()
        {
            Vector3 velocity = _movementController.Velocity;
            Vector3 horizontalVelocity = new Vector3(velocity.x,0,velocity.z);
            _animator.SetFloat("Velocity", horizontalVelocity.magnitude);
        }
        private void SetShootingState()
        {
            if (_lastShootingStateRoutine != null)
                StopCoroutine(_lastShootingStateRoutine);
            _animator.SetBool("Shooting", true);
            //Should use false if want to change Shooting state to false
            _lastShootingStateRoutine = StartCoroutine(SetShootingStateCoroutine(isShootingStateContinuous));
        }
        private IEnumerator SetShootingStateCoroutine(bool state)
        {
            yield return new WaitForSeconds(shootingStateTime);
            if (_animator.GetBool("Shooting") != state)
            {
                _animator.SetBool("Shooting", state);
            }
        }
    
    }
}