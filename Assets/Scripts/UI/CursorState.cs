using System;
using DemoShooter.GameSystems;
using DemoShooter.Managers;
using UnityEngine;

namespace DemoShooter.UI
{
    public class CursorState : MonoBehaviour
    {
        private void Awake()
        {
            ChangeCursorState(CursorLockMode.Locked,false);
        }

        private void Start()
        {
            PauseManager.instance.OnPauseToggle += ToggleCursorState;
        }

        private void OnDestroy()
        {
            PauseManager.instance.OnPauseToggle -= ToggleCursorState;
        }

        private void ToggleCursorState()
        {
            if (PauseManager.instance.IsPaused)
                ChangeCursorState(CursorLockMode.None,true);
            else
                ChangeCursorState(CursorLockMode.Locked,false);
        }
        private void ChangeCursorState(CursorLockMode lockMode, bool visibility)
        {
            Cursor.lockState = lockMode;
            Cursor.visible = visibility;
        }
    }
}
