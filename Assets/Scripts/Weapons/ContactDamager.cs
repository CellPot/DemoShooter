using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamager : MonoBehaviour
{
    public float damage;
    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
        Life otherLife = collision.gameObject.GetComponent<Life>();
        if (otherLife != null)
        {
            otherLife.LifeAmount -= damage;
            Debug.Log(otherLife.LifeAmount.ToString());
        }
    }
}
