using System;
using UnityEngine;

namespace DemoShooter.Managers
{
    public class BaseManager : MonoBehaviour
    {
        public static BaseManager instance;
        [SerializeField] public Transform baseTransform;
        
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Debug.LogWarning("Duplicated BaseManager is ignored", gameObject);
        }
    }
}