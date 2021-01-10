using UnityEngine;

namespace DemoShooter.GameSystems
{

    public class InputSystem : MonoBehaviour
    {
        [SerializeField] private bool isMovementInputBlocked;
        [SerializeField] private bool isShootingInputBlocked;
        public delegate void PlayerMovementHandler(Vector3 movementInputRaw, bool sprintMode);
        public event PlayerMovementHandler OnPlayerMoved;
        public delegate void KeyPressedHandler();
        public event KeyPressedHandler OnPlayerShot; 
        public event KeyPressedHandler OnEscapePressedDown;
        public event KeyPressedHandler OnPlayerAimed;

        private bool _isMoving = false;

        public bool IsMovementBlocked => isMovementInputBlocked;
        public bool IsShootingBlocked => isShootingInputBlocked;

        private void Update()
        {
            if (!isMovementInputBlocked)
                PlayerMovementCheck();
            if (!isShootingInputBlocked)
                PlayerShootingCheck();
            EscapePressedCheck();
            
            if (!_isMoving)
                PlayerAimingCheck();
        }
        private void PlayerMovementCheck()
        {
            Vector3 movementInputRaw = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            if (movementInputRaw != Vector3.zero)
            {
                OnPlayerMoved?.Invoke(movementInputRaw, KeyManager.IsPressed(KeyCode.LeftShift));
                _isMoving = true;
            }
            else
            {
                _isMoving = false;
            }
        }
        private void PlayerShootingCheck()
        {
            if (KeyManager.IsPressed(KeyCode.Mouse0)) 
            {
                OnPlayerShot?.Invoke();
            }
        }

        private void PlayerAimingCheck()
        {
            if (KeyManager.IsPressed(KeyCode.Mouse1))
            {
                OnPlayerAimed?.Invoke();
            }
        }

        private void EscapePressedCheck()
        {
            if (KeyManager.IsPressedDown(KeyCode.Escape))
            {
                OnEscapePressedDown?.Invoke();
            }
        }

        public void BlockControlsInput(bool movementBlock, bool shootingBlock)
        {
            isMovementInputBlocked = movementBlock;
            isShootingInputBlocked = shootingBlock;
        }
    }



}