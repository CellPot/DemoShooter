using DemoShooter.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DemoShooter.UI
{
    public class PauseMenuUI: MonoBehaviour
    {
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private Button restartButton;

        private void Awake()
        {
            resumeButton.onClick?.AddListener(OnResumeClick);
            quitButton.onClick?.AddListener(OnQuitClick);
            restartButton.onClick?.AddListener(OnRestartClick);
        }

        private void OnDestroy()
        {
            resumeButton.onClick?.RemoveListener(OnResumeClick);
            quitButton.onClick?.RemoveListener(OnQuitClick);
            restartButton.onClick?.RemoveListener(OnRestartClick);
        }

        private void OnResumeClick()
        {
            PauseManager.instance.OnPauseChanged();
        }

        private void OnQuitClick()
        {
            Application.Quit();
        }

        private void OnRestartClick()
        {
            PauseManager.instance.OnPauseChanged();
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }
}