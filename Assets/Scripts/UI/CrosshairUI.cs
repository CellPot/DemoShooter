using System;
using DemoShooter.Debugging;
using DemoShooter.ShootingMechanic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DemoShooter.UI
{
    public class CrosshairUI : MonoBehaviour
    {
        [SerializeField] private Image sightImage;
        [SerializeField] private Camera playerCamera;
        [SerializeField] private Transform playerWeapon;
        [SerializeField] private LayerMask layerMask;
        [Range(0f,100f)]
        [SerializeField] private float size = 6f;
        [Tooltip("Centers sight. Used if weapon's direction in space is ignored")]
        [SerializeField] private bool isCentered = true;
        [Range(Single.Epsilon, 1.0f)] [Tooltip("How fast sight follows new position")]
        [SerializeField] private float interpolationSpeed = 0.3f;

        private void Awake()
        {
            if (playerCamera==null)
                playerCamera = Camera.main;;
        }

        private void Update()
        {
            SetCrosshairPosition();
        }

        private void SetCrosshairPosition()
        {
            if (isCentered)
            {
                sightImage.rectTransform.position = playerCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0));
            }
            else
            {
                Vector3 hitPoint = GetHitVector();
                sightImage.rectTransform.position = Vector3.Lerp(sightImage.rectTransform.position,
                    playerCamera.WorldToScreenPoint(hitPoint), interpolationSpeed);
            }
        }

        private Vector3 GetHitVector()
        {
            Vector3 rayHitPoint = playerWeapon.forward * 800f;
            Ray ray = new Ray(playerWeapon.transform.position,rayHitPoint);
            if (Physics.Raycast(ray, out var hit,200f,layerMask))
            {
                rayHitPoint = hit.point;
            }
            
            Debug.Log(Vector3.Distance(playerWeapon.transform.position,rayHitPoint).ToString());
            return rayHitPoint;
        }
        private void OnValidate()
        {
            sightImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,size);
            sightImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,size);
        }
    }
}
