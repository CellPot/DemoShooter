using System.Collections;
using DemoShooter.GameSystems;
using DemoShooter.Movement;
using UnityEngine;

namespace DemoShooter.AnimationControllers
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimatorStateController : MonoBehaviour
    {
        [SerializeField] private InputSystem inputSystem;
        [SerializeField] private float shootingStateTime = 0.5f;
        [SerializeField] private bool isShootingStateContinuous = true;
        
        private PlayerMovementController _movementController;
        private CharacterController _characterController;
        private Animator _anim;
        private Coroutine _lastShootingStateRoutine;
        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _movementController = GetComponent<PlayerMovementController>();
            _characterController = GetComponent<CharacterController>();
        }

        private void Start()
        {
            inputSystem.OnPlayerShot += SetShootingState;
            _movementController.OnVelocityChanged += SetHorizontalVelocityState;
        }
        private void OnDestroy()
        {
            inputSystem.OnPlayerShot -= SetShootingState;
            _movementController.OnVelocityChanged -= SetHorizontalVelocityState;
        }

        private void SetHorizontalVelocityState()
        {
            var velocity = _characterController.velocity;
            Vector3 horizontalVelocity = new Vector3(velocity.x,0,velocity.z);
            _anim.SetFloat("Velocity", horizontalVelocity.magnitude);
            // Debug.Log($"Velocity {horizontalVelocity.magnitude }");
        }

    
        private void SetShootingState()
        {
            if (_lastShootingStateRoutine != null)
                StopCoroutine(_lastShootingStateRoutine);
            _anim.SetBool("Shooting", true);
            //Should use false if want to change Shooting state to false
            _lastShootingStateRoutine = StartCoroutine(SetShootingStateCoroutine(isShootingStateContinuous));
        }
        private IEnumerator SetShootingStateCoroutine(bool state)
        {
            yield return new WaitForSeconds(shootingStateTime);
            if (_anim.GetBool("Shooting") != state)
            {
                _anim.SetBool("Shooting", state);
            }
        }
    }
}
