using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private int damageInflicted;    
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private float ammoVelocity;
    [SerializeField] private float ammoActiveTime;

    public bool ActiveSelf => gameObject.activeSelf;
    public Vector3 BulletPosition => gameObject.transform.position;

    private Rigidbody bulletRbody => rigidBody;
    private bool IsKinematic => bulletRbody.isKinematic;



    private void Awake()
    {
        if (rigidBody == null)
            gameObject.TryGetComponent<Rigidbody>(out rigidBody);
    }


    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider is CapsuleCollider)
        {
            //TODO: реализовать нанесение урона противнику
            gameObject.SetActive(false);
        }
    }


    public void SetActive(bool state)
    {
        gameObject.SetActive(state);
    }
    public void SetActive(bool state, Vector3 position, Quaternion rotation)
    {
        if (state)
        {
            gameObject.transform.position = position;
            gameObject.transform.rotation = rotation;
        }
        SetActive(state);
    }

    public void Fire(Vector3 targetPoint, float ammoVelocity, float ammoActiveTime)
    {
        Vector3 bulletVelocity = BulletVelocity(targetPoint, ammoVelocity);
        //TESTING isKinematic
        if (!IsKinematic)
            bulletRbody.velocity = bulletVelocity;
        else
            StartCoroutine(CO_BulletMoveToPosition(bulletVelocity));
        StartCoroutine(CO_DeactivateOnThreshold(ammoActiveTime));

    }
    private Vector3 BulletVelocity(Vector3 velocityVector, float ammoVelocity)
    {
        Vector3 velocity = velocityVector * (ammoVelocity);
        Debug.Log("Velocity vector: " + velocity);
        return velocity;
    }
    private IEnumerator CO_BulletMoveToPosition(Vector3 bulletVelocity)
    {
        Vector3 velocity = bulletVelocity * Time.deltaTime;

        Debug.Log("Velocity vector: " + velocity);
        while (ActiveSelf)
        {
            rigidBody.MovePosition(BulletPosition + velocity);
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }
    private IEnumerator CO_DeactivateOnThreshold(float thresholdSeconds)
    {
        yield return new WaitForSeconds(thresholdSeconds);
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
        yield return null;
    }


}
