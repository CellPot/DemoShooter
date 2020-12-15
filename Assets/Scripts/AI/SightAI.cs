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

        [SerializeField] private Collider detectedObject;

        public Collider DetectedObject => detectedObject;
        private static Collider[] _detectedObjects = new Collider[100];
        
        private void Update()
        {
            // Collider[] colliders = Physics.OverlapSphere(transform.position, viewDistance, detectLayers);
            int detectedAmount =
                Physics.OverlapSphereNonAlloc(transform.position, viewDistance, _detectedObjects, detectLayers);
            detectedObject = null;
            SetDetectedObject(_detectedObjects,detectedAmount);
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

        private void SetDetectedObject(Collider[] colliders, int detectedCount)
        {
            for (var i = 0; i < detectedCount; i++)
            {
                Collider collidedObject = colliders[i];
                Vector3 directionToCollider = Vector3.Normalize(collidedObject.bounds.center - transform.position);
                float angleToCollider = Vector3.Angle(transform.forward, directionToCollider);
                if (angleToCollider < viewAngle)
                {
                    RaycastHit hit;
                    if (!IsViewBlocked(collidedObject, out hit))
                    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
                        Debug.DrawLine(transform.position, collidedObject.bounds.center, Color.green);
#endif
                        detectedObject = collidedObject;
                        break;
                    }
#if UNITY_EDITOR || DEVELOPMENT_BUILD
                    Debug.DrawLine(transform.position, hit.point, Color.red);
#endif
                }
            }
        }

        private bool IsViewBlocked(Collider collider, out RaycastHit raycastHit)
        {
            return Physics.Linecast(transform.position,collider.bounds.center, out raycastHit, obstacleLayers);
        }
    }
}
