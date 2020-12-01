using UnityEngine;

namespace DemoShooter.ShootingMechanic
{
    public class DestinationCalculator : MonoBehaviour, IDestinationCalculator
    {
        public Vector3 GetDestinationVector(Camera playerCamera, bool takeDistanceIntoAccount)
        {
            Camera _camera = playerCamera as Camera;
            //Creating a ray between initial point and destination, where the last one depends on camera's center
            Ray ray2 = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            Vector3 rayHitPoint = ray2.GetPoint(100);
            if (!takeDistanceIntoAccount) 
                return rayHitPoint;
            RaycastHit hit;
            if (Physics.Raycast(ray2, out hit))
                rayHitPoint = hit.point;
            return rayHitPoint;
        }
    }
}