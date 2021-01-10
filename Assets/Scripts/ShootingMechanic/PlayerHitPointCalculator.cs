using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

namespace DemoShooter.ShootingMechanic
{
    
    public class PlayerHitPointCalculator : MonoBehaviour, IDestinationCalculator
    {
        [SerializeField] private Camera playerCamera;
        [FormerlySerializedAs("distanceBasedSights")]
        [Tooltip("Zero weapon's sights at specified distance instead of ray's closest hit point")]
        [SerializeField] private bool isDistanceBased = false;
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
            Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            Vector3 rayHitPoint = ray.GetPoint(distanceToCenter);
            if (isDistanceBased) 
                return rayHitPoint;
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
                rayHitPoint = hit.point;
            return rayHitPoint;
        }
    }
}