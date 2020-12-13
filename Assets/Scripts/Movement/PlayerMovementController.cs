using System;
using System.Collections;
using DemoShooter.GameSystems;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DemoShooter.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovementController: MonoBehaviour
    {
        [SerializeField] private Transform charCamera;
        [SerializeField] private bool canMove = true;
        [SerializeField] private bool canRun = true;
        [SerializeField] private float movWalkSpeed = 5f;
        [SerializeField] private float movSprintSpeed = 7f;
        [SerializeField] private float movSpeedModifier = 1f;
        [SerializeField] private float turnSmoothTime = 0.1f;
        [SerializeField] private float turnSmoothVelocity;
    
        //////
        [SerializeField] private InputSystem inputSystem;
        /////
        private CharacterController _charController;
        public bool IsMoving
        {
            get => _isMoving;
            private set
            {
                _isMoving = value;
                OnVelocityChanged?.Invoke();;
            }
        }

        private bool _isMoving;

        public delegate void PlayerVelocityChangeHandler();
        public event PlayerVelocityChangeHandler OnVelocityChanged;

        private void Awake()
        {
            _charController = GetComponent<CharacterController>();
            charCamera = charCamera ? charCamera : Camera.main.transform;
        }

        private void Start()
        {
            inputSystem.OnPlayerMoved += MoveCharacter;
        }

        private void OnDestroy()
        {
            inputSystem.OnPlayerMoved -= MoveCharacter;
        }

        private void MoveCharacter(Vector3 movDirection, bool isSprinting)
        {
            if (!canMove) return;
            float smoothedMovementAngle = GetSmoothedTargetAngle(movDirection.normalized);
            _charController.transform.rotation = Quaternion.Euler(0f, smoothedMovementAngle, 0f);
            Vector3 cameraDirecion = Quaternion.Euler(0f, smoothedMovementAngle, 0f) * Vector3.forward;
            StartCoroutine(Walk(cameraDirecion,isSprinting));
            
        }

        private float GetSmoothedTargetAngle(Vector3 normalDirection)
        {
            //Получение угла направления(0:360) по его Tan: Tan d = (направление по x / по y) + угол камеры
            float targetAngle = Mathf.Atan2(normalDirection.x, normalDirection.z) * Mathf.Rad2Deg + charCamera.eulerAngles.y;
            //Сглаживание угла направления
            float smoothedAngle = Mathf.SmoothDampAngle(_charController.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            return smoothedAngle;
        }
        private IEnumerator Walk(Vector3 direction, bool isSprinting)
        {
            if (isSprinting && canRun)
                _charController.Move(direction.normalized * (movSprintSpeed * movSpeedModifier * Time.deltaTime));
            else
                _charController.Move(direction.normalized * (movWalkSpeed * movSpeedModifier * Time.deltaTime));
            IsMoving = true;
            yield return new WaitForSeconds(0.1f);
            IsMoving = false;
        }
    }
}