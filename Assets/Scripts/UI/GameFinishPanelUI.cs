using System;
using DemoShooter.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace DemoShooter.UI
{
    public class GameFinishPanelUI : MonoBehaviour
    {
        [SerializeField] private GameObject finishPanel;
        [SerializeField] private TMP_Text finishText;

        private void Start()
        {
            GameManager.instance.OnGameWon += ActivateWinState;
            GameManager.instance.OnGameLost += ActivateLoseState;
        }

        private void OnDestroy()
        {
            GameManager.instance.OnGameWon -= ActivateWinState;
            GameManager.instance.OnGameLost -= ActivateLoseState;
        }

        private void ActivateLoseState()
        {
            ActivatePanel(true);
            finishText.SetText("You lost");
        }
        private void ActivateWinState()
        {
            ActivatePanel(true);
            finishText.SetText("You won");
        }
        private void ActivatePanel(bool active)
        {
            finishPanel.SetActive(active);
        }
    }
}