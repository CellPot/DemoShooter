using UnityEngine;

namespace DemoShooter.UI
{
    public class CursorState : MonoBehaviour
    {
        private void Update()
        {
            ChangeCursorState(CursorLockMode.Locked, false);
        }
        public void ChangeCursorState(CursorLockMode lockMode, bool visibility)
        {
            Cursor.lockState = lockMode;
            Cursor.visible = visibility;
        }
    }
}
