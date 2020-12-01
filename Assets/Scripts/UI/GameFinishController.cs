using System;
using DemoShooter.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace DemoShooter.UI
{
    public class GameFinishController : MonoBehaviour
    {
        [SerializeField] private TMP_Text finishText;
        [SerializeField] private GameObject finishPanel;

        private void Start()
        {
            GameManager.instance.OnGameWon += ActivateWinPanel;
            GameManager.instance.OnGameLost += ActivateLosePanel;
        }

        private void ActivateWinPanel()
        {
            Debug.Log("Panel activation started");
            finishPanel.SetActive(true);
            finishText.SetText("You won");
            Debug.Log("Panel is activated");
        }
        private void ActivateLosePanel()
        {
            finishPanel.SetActive(true);
            finishText.SetText("You lost");
        }

        private void OnDestroy()
        {
            GameManager.instance.OnGameWon -= ActivateWinPanel;
            GameManager.instance.OnGameLost -= ActivateLosePanel;
        }
    }
}