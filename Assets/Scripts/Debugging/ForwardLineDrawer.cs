using UnityEngine;

namespace DemoShooter.Debugging
{
    public class ForwardLineDrawer : MonoBehaviour
    {
        [SerializeField] private Transform initObject;
        [SerializeField] private bool isDrawing = true;
        [SerializeField] private float drawingDistance = 1000f;
        private void Awake()
        {
            if (initObject == null)
                initObject = gameObject.transform;
        }

        private void Update()
        {
            if (isDrawing)
            {
                Debug.DrawLine(initObject.position,GetHitVector(initObject,drawingDistance));
            }
        }
        private Vector3 GetHitVector(Transform initialObject, float drawDistance)
        {
            Vector3 rayHitPoint = initialObject.forward * drawDistance;
            Ray ray = new Ray(initialObject.position,rayHitPoint);
            if (Physics.Raycast(ray, out var hit))
            {
                rayHitPoint = hit.point;
            }
            return rayHitPoint;
        }
    }
}