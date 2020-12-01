using System;
using System.Collections.Generic;
using Packages.Rider.Editor.UnitTesting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace DemoShooter.GameSystems
{

    public class InputSystem : MonoBehaviour
    {
        [SerializeField] private bool isMovementBlocked;
        [SerializeField] private bool isShootingBlocked;
        public delegate void PlayerMovementHandler(Vector3 movementInputRaw, bool sprintMode);
        public event PlayerMovementHandler OnPlayerMoved;
        public delegate void PlayerShootingHandler();
        public event PlayerShootingHandler OnPlayerShot; 

        private void Update()
        {
            if (!isMovementBlocked)
                PlayerMoved();
            if (!isShootingBlocked)
                PlayerShot();
        }
        private void PlayerMoved()
        {
            Vector3 movementInputRaw = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            if (movementInputRaw != Vector3.zero)
            {
                OnPlayerMoved?.Invoke(movementInputRaw, KeyManager.IsPressed(KeyCode.LeftShift));
            }
        }
        private void PlayerShot()
        {
            if (KeyManager.IsPressedDown(KeyCode.Mouse0))
            {
                OnPlayerShot?.Invoke();
            }
        }

        public void BlockControls(bool movementBlock, bool shootingBlock)
        {
            isMovementBlocked = movementBlock;
            isShootingBlocked = shootingBlock;
        }
    }



}