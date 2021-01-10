using System;
using System.Net.Configuration;
using DemoShooter.Movement;
using UnityEngine;
using UnityEngine.Serialization;

namespace DemoShooter.AnimationControllers
{
    [RequireComponent(typeof(Animator))]
    public class PlayerIKController : MonoBehaviour
    {

        [SerializeField] private bool ikActive = false;
        [SerializeField] private Camera playerCamera;
        [SerializeField] private PlayerMovementController movementController;
        

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnAnimatorIK(int layerIndex)
        {
            if (!_animator) return;
            if (ikActive && movementController.CanMove)
            {
                if (playerCamera == null) return;
                Ray camTargetRay = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                Vector3 goalPosition = camTargetRay.GetPoint(100f);
                _animator.SetIKPositionWeight(AvatarIKGoal.RightHand,1);
                _animator.SetIKRotationWeight(AvatarIKGoal.RightHand,1);
                _animator.SetIKPosition(AvatarIKGoal.RightHand,goalPosition);
                Quaternion goalRotation = playerCamera.transform.rotation;
                _animator.SetIKRotation(AvatarIKGoal.RightHand,goalRotation);
            }
            else {          
                _animator.SetIKPositionWeight(AvatarIKGoal.RightHand,0);
                _animator.SetIKRotationWeight(AvatarIKGoal.RightHand,0); 
                _animator.SetLookAtWeight(0);
            }
        }
    }
    
}