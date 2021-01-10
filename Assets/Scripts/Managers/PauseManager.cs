using System;
using DemoShooter.GameSystems;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DemoShooter.Managers
{
    public class PauseManager : MonoBehaviour
    {
        public static PauseManager instance;
        [SerializeField] private InputSystem input;
        [SerializeField] private bool isPaused = false;
        [SerializeField][Range(0f,1f)] private float pauseTimeScale = 0;
        [SerializeField] private GameObject pauseMenu;
        // [SerializeField] private GameObject optionsMenu;

        public bool IsPaused { get => isPaused; set => isPaused = value; }

        public delegate void PauseStateHandler(bool state);

        public event PauseStateHandler OnPauseToggle;


        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Debug.LogWarning("Duplicated PauseManager is ignored", gameObject);
            if (pauseMenu != null)
                pauseMenu.SetActive(false);
            // if (optionsMenu != null)
            //     optionsMenu.SetActive(false);
        }

        private void Start()
        {
            input.OnEscapePressedDown += OnPauseChanged;
        }

        private void OnDestroy()
        {
            input.OnEscapePressedDown -= OnPauseChanged;
        }

        public void OnPauseChanged()
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                pauseMenu.SetActive(true);
                input.BlockControlsInput(true,true);
                Time.timeScale = pauseTimeScale;
            }
            else
            {
                pauseMenu.SetActive(false);
                // optionsMenu.SetActive(false);
                input.BlockControlsInput(false,false);
                Time.timeScale = 1;
            }
            OnPauseToggle?.Invoke(isPaused);
        }
    }
}