using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private int damageInflicted;

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider is CapsuleCollider)
        {
            //TODO: реализовать нанесение урона противнику
            gameObject.SetActive(false);
        }
    }

    


}
