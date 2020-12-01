using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DemoShooter.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private int scoreAmount;
        public static ScoreManager instance;
        public int ScoreAmount { get => scoreAmount; set => scoreAmount = value;  }

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Debug.LogWarning("Duplicated ScoreManager is ignored", gameObject);
        }
    }
}
