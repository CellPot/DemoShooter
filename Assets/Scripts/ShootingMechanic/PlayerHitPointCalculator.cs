using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace DemoShooter.ShootingMechanic
{
    
    public class PlayerHitPointCalculator : MonoBehaviour, IDestinationCalculator
    {
        [SerializeField] private Camera playerCamera;
        [Tooltip("Zero weapon's sights at specified distance instead of ray's ultimate hit point")]
        [SerializeField] private bool distanceBasedSights = false;
        [Tooltip("Distance to zero sights if option is toggled on")]
        [SerializeField] private float distanceToCenter = 100f;

        private void Awake()
        {
            if (playerCamera == null)
                playerCamera = Camera.main;
        }

        public Vector3 GetHitVector()
        {
            //Creating a ray between initial point and destination, where the last one depends on camera's center
            Ray ray2 = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            Vector3 rayHitPoint = ray2.GetPoint(distanceToCenter);
            if (!distanceBasedSights) 
                return rayHitPoint;
            RaycastHit hit;
            if (Physics.Raycast(ray2, out hit))
                rayHitPoint = hit.point;
            return rayHitPoint;
        }
    }
}