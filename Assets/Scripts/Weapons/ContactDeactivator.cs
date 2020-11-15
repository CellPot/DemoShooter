using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDeactivator : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
        Destroy(collision.gameObject);
        
    }
}
