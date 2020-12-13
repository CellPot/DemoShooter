using System;
using System.Collections;
using System.Collections.Generic;
using DemoShooter.Characters;
using DemoShooter.Managers;
using DemoShooter.Movement;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace DemoShooter.AI
{
    [RequireComponent(typeof(EnemyMovementController))]
    public class EnemyFsm : MonoBehaviour
    {
        [SerializeField] private EnemyState currentState;
        [SerializeField] private SightAI sightSensor;
        [SerializeField] private float baseAttackDistance;
        [SerializeField] private float playerAttackDistance;
        [Tooltip("Value that prevents clipping state between Chasing and Attacking")]
        [SerializeField] private float playerAttackDistanceThreshold = 1.1f;
        [SerializeField] private float attackRotationDamp = 5f;

        private EnemyMovementController _movementController;
        private Enemy _enemy;
        private Transform _baseTransform;
        private EnemyState CurrentState
        {
            get => currentState;
            set => currentState = value;
        }

        private void Awake()
        {
            _movementController = GetComponentInParent<EnemyMovementController>();
            _enemy = GetComponentInParent<Enemy>();
        }

        private void Start()
        {
            _baseTransform = BaseManager.instance.baseTransform;
        }

        private void Update()
        {
            switch (CurrentState)
            {
                case EnemyState.GoToBase:
                    GoToBase();
                    return;
                case EnemyState.AttackBase:
                    AttackBase();
                    return;
                case EnemyState.ChasePlayer:
                    ChasePlayer();
                    return;
                case EnemyState.AttackPlayer:
                    AttackPlayer();
                    return;
                default:
                    return;
            }
        }

        private void AttackPlayer()
        {
            if (sightSensor.detectedObject == null)
            {
                currentState = EnemyState.GoToBase;
                return;
            }
        
            Vector3 detectedObjPosition = sightSensor.detectedObject.transform.position;

            _movementController.StopCharacter();
            _movementController.RotateCharacter(detectedObjPosition,attackRotationDamp);
            _enemy.Attack();
        
            float distanceToPlayer = Vector3.Distance(transform.position, detectedObjPosition);
            if (distanceToPlayer >= playerAttackDistance * playerAttackDistanceThreshold)
            {
                currentState = EnemyState.ChasePlayer;
            }
        }

        private void ChasePlayer()
        {
            _movementController.StopCharacter(false);
            if (sightSensor.detectedObject == null)
            {
                currentState = EnemyState.GoToBase;
                return;
            }

            Vector3 detectedObjPosition = sightSensor.detectedObject.transform.position;
            _movementController.MoveCharacter(detectedObjPosition);

            float distanceToPlayer = Vector3.Distance(transform.position, detectedObjPosition);
            if (distanceToPlayer <= playerAttackDistance)
            {
                currentState = EnemyState.AttackPlayer;
            }
        }

        private void AttackBase()
        {
            _movementController.StopCharacter();
            if (_baseTransform!=null)
                _movementController.RotateCharacter(_baseTransform.position,attackRotationDamp);
            _enemy.Attack();
        }

        private void GoToBase()
        {
            _movementController.StopCharacter(false);
            if (_baseTransform!=null)
                _movementController.MoveCharacter(_baseTransform.position);
        
            if (sightSensor.detectedObject != null)
            {
                CurrentState = EnemyState.ChasePlayer;
            }

            float distanceToBase = Vector3.Distance(transform.position,  BaseManager.instance.baseTransform.position);
            if (distanceToBase <= baseAttackDistance)
                currentState = EnemyState.AttackBase;

        }
    }
    public enum EnemyState
    {
        GoToBase, AttackBase, ChasePlayer, AttackPlayer
    }
}