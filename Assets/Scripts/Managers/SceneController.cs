using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Life playerLife;
    [SerializeField] private Life baseLife;

    private void Start()
    {
        playerLife.onDeath.AddListener(OnPlayerLifeChanged);
        baseLife.onDeath.AddListener(OnPlayerLifeChanged);
        EnemyManager.instance.onChanged.AddListener(CheckWinCondition);
        WaveManager.instance.onChanged.AddListener(CheckWinCondition);

    }
    private void CheckWinCondition()
    {
        WaveManager wm = WaveManager.instance;
        EnemyManager em = EnemyManager.instance;
        if (wm.Waves.Count <= 0)
            if (em.Enemies.Count <= 0)
                SceneManager.LoadScene("WinScene");
    }

    void OnPlayerLifeChanged()
    {
        if (playerLife.LifeAmount <= 0)
            SceneManager.LoadScene("LoseScene");
    }
    void OnBaseLifeChanged()
    {
        if (baseLife.LifeAmount <= 0)
            SceneManager.LoadScene("LoseScene");
    }
    private void OnDestroy()
    {
        playerLife.onDeath.RemoveListener(Start);
        baseLife.onDeath.RemoveListener(Start);
    }
}
