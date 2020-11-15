using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Life : MonoBehaviour
{
    [SerializeField] private float _lifeAmount;
    public UnityEvent onDeath;

    public float LifeAmount
    {
        get => _lifeAmount;
        set
        {
            _lifeAmount = value;
            if (LifeAmount <= 0)
            {
                onDeath.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
