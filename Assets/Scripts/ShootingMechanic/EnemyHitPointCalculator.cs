using DemoShooter.Characters;
using UnityEngine;

namespace DemoShooter.ShootingMechanic
{
    public class EnemyHitPointCalculator : MonoBehaviour, IDestinationCalculator
    {
        public Vector3 GetHitVector()
        {
            var thisObject = gameObject;
            
            Ray ray = new Ray(thisObject.transform.position,thisObject.transform.forward);
            Vector3 rayHitPoint = ray.GetPoint(100f);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
                rayHitPoint = hit.point;
            return rayHitPoint;
        }
    }
}