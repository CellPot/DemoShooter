using UnityEngine;

namespace DemoShooter.GameSystems
{

    public class InputSystem : MonoBehaviour
    {
        [SerializeField] private bool isMovementInputBlocked;
        [SerializeField] private bool isShootingInputBlocked;
        public delegate void PlayerMovementHandler(Vector3 movementInputRaw, bool sprintMode);
        public event PlayerMovementHandler OnPlayerMoved;
        public delegate void PlayerShootingHandler();
        public event PlayerShootingHandler OnPlayerShot; 
        public delegate void KeyPressedHandler();
        public event KeyPressedHandler OnEscapePressedDown;

        private void Update()
        {
            if (!isMovementInputBlocked)
                PlayerMovementCheck();
            if (!isShootingInputBlocked)
                PlayerShootingCheck();
            EscapePressedCheck();
        }
        private void PlayerMovementCheck()
        {
            Vector3 movementInputRaw = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            if (movementInputRaw != Vector3.zero)
            {
                OnPlayerMoved?.Invoke(movementInputRaw, KeyManager.IsPressed(KeyCode.LeftShift));
            }
        }
        private void PlayerShootingCheck()
        {
            if (KeyManager.IsPressed(KeyCode.Mouse0)) 
            {
                OnPlayerShot?.Invoke();
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