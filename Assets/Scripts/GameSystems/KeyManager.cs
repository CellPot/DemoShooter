using UnityEngine;

namespace DemoShooter.GameSystems
{
    public static class KeyManager
    {
        public static bool IsPressed(KeyCode key)
        {
            return Input.GetKey(key);
        }
        public static bool IsPressedDown(KeyCode key)
        {
            return Input.GetKeyDown(key);
        }
    }
}