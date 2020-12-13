using DemoShooter.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace DemoShooter.UI
{
    public class PauseMenuUI: MonoBehaviour
    {
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button quitButton;

        private void Awake()
        {
            resumeButton.onClick.AddListener(OnResumeClick);
            quitButton.onClick.AddListener(OnQuitClick);
        }

        private void OnDestroy()
        {
            resumeButton.onClick.RemoveListener(OnResumeClick);
            quitButton.onClick.RemoveListener(OnQuitClick);
        }

        private void OnResumeClick()
        {
            PauseManager.instance.OnPauseChanged();
        }

        private void OnQuitClick()
        {
            Application.Quit();
        }
    }
}