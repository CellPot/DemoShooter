using System;
using UnityEngine;

namespace DemoShooter.AI
{
    public class SightAI : MonoBehaviour
    {
        [SerializeField] private float viewDistance;
        [Tooltip("Angle to detect to the left and right from object's forward view")]
        [SerializeField] private float viewAngle;
        [SerializeField] private LayerMask detectLayers;
        [SerializeField] private LayerMask obstacleLayers;

        public Collider detectedObject;
        private void Update()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, viewDistance, detectLayers);
            detectedObject = null;
            SetDetectedObject(colliders);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position,viewDistance);
            Vector3 rightViewAngleBorder = Quaternion.Euler(0, viewAngle, 0) * transform.forward;
            Vector3 leftViewAngleBorder = Quaternion.Euler(0, -viewAngle, 0) * transform.forward;
            Gizmos.color = Color.magenta;
            Gizmos.DrawRay(transform.position,rightViewAngleBorder*viewDistance);
            Gizmos.DrawRay(transform.position,leftViewAngleBorder*viewDistance);
        }

        private void SetDetectedObject(Collider[] colliders)
        {
            foreach (Collider collidedObject in colliders)
            {
                Vector3 directionToCollider = Vector3.Normalize(collidedObject.bounds.center - transform.position);
                float angleToCollider = Vector3.Angle(transform.forward, directionToCollider);
                if (angleToCollider < viewAngle)
                {
                    RaycastHit hit;
                    if (!IsViewBlocked(collidedObject,out hit))
                    {
                        Debug.DrawLine(transform.position,collidedObject.bounds.center,Color.green);
                        detectedObject = collidedObject;
                        break;
                    }
                    else
                    {
                        Debug.DrawLine(transform.position,hit.point,Color.red);
                    }
                }
            }
        }

        private bool IsViewBlocked(Collider collider, out RaycastHit raycastHit)
        {
            return Physics.Linecast(transform.position,collider.bounds.center, out raycastHit, obstacleLayers);
        }
    }
}
