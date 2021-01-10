using System;
using DemoShooter.GameSystems;
using UnityEngine;

namespace DemoShooter.Movement
{
    [RequireComponent(typeof(PlayerMovementController))]
    public class PlayerAimingController : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;
        [SerializeField] private float smoothTime = 7f;
        [SerializeField] private float maxDelta = 10f;
        private PlayerMovementController _playerMovementController;

        private void Awake()
        {
            if (playerCamera == null)
                playerCamera = Camera.main;;
            _playerMovementController = GetComponent<PlayerMovementController>();
        }

        private void Update()
        {
            if (_playerMovementController.CanMove)
                TurnCharacter();
        }
        private void TurnCharacter()
        {
            Quaternion playerRotation = gameObject.transform.rotation;
            Quaternion cameraRotation = playerCamera.transform.rotation;
            float deltaAngle = Quaternion.Angle(playerRotation, cameraRotation);
            if (deltaAngle > maxDelta)
            {
                Quaternion smoothedQuat = Quaternion.Lerp(playerRotation,
                    cameraRotation, smoothTime* Time.deltaTime);
                _playerMovementController.RotateCharacter(smoothedQuat.eulerAngles.y);
            }
            
        }

    }
}
