using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] private int _amount;
    public int Amount { get => _amount; set { _amount = value; Debug.Log($"New Score: {_amount}"); } }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogError("Duplicated ScoreManager", gameObject);
    }
}
