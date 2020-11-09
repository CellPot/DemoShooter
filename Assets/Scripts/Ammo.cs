using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private int damageInflicted;

    [SerializeField] private Rigidbody rigidBody;

    private void Awake()
    {
        if (rigidBody == null)
            rigidBody = gameObject.GetComponent<Rigidbody>();
    }


    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider is CapsuleCollider)
        {
            //TODO: реализовать нанесение урона противнику
            gameObject.SetActive(false);
        }
    }
    private IEnumerator DeactivateOnThreshold(float thresholdSeconds)
    {
        yield return new WaitForSeconds(thresholdSeconds);
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
        yield return null;
    }



}
