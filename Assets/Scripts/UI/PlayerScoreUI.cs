using System;
using DemoShooter.Managers;
using TMPro;
using UnityEngine;

namespace DemoShooter.UI
{
    public class PlayerScoreUI : MonoBehaviour
    {
        
        [SerializeField] private TMP_Text scoreText;

        private void Start()
        {
            ChangeScoreText();
            ScoreManager.instance.OnScoreChanged += ChangeScoreText;
        }

        private void OnDestroy()
        {
            ScoreManager.instance.OnScoreChanged -= ChangeScoreText;
        }

        private void ChangeScoreText()
        {
            scoreText.text = $"Score: {ScoreManager.instance.ScoreAmount.ToString()}";
        }
    }
}