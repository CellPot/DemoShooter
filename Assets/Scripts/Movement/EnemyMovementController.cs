using UnityEngine;
using UnityEngine.AI;

namespace DemoShooter.Movement
{
    public class EnemyMovementController : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
    
        [SerializeField] private float movWalkSpeed = 3.5f;
        [SerializeField] private float movSprintSpeed = 7f;
        [SerializeField] private float agentAcceleration = 8f;
        [SerializeField] private float agentAngularSpeed = 120f;
        [SerializeField] private bool canMove = true;
        [SerializeField] private bool canRun = true;
        public Vector3 Velocity => _navMeshAgent.velocity;

        private void Awake()
        {
            _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
            OnValidate();
        }
        private void OnValidate()
        {
            if (_navMeshAgent==null) return;
            _navMeshAgent.acceleration = agentAcceleration;
            _navMeshAgent.angularSpeed = agentAngularSpeed;
            _navMeshAgent.speed = movWalkSpeed;
        
            agentAcceleration = _navMeshAgent.acceleration;
            agentAngularSpeed = _navMeshAgent.angularSpeed;
            movWalkSpeed = _navMeshAgent.speed;
        }

        public void StopCharacter(bool state = true)
        {
            _navMeshAgent.isStopped = state;
        }

        public void RotateCharacter(Vector3 targetPosition, float rotationDamp = 5f)
        {
            Vector3 directionToTarget = Vector3.Normalize(targetPosition- gameObject.transform.position);
            directionToTarget.y = 0;
            var rotation = Quaternion.LookRotation(directionToTarget);
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation,rotation, Time.deltaTime*rotationDamp);
        }

        public void MoveCharacter(Vector3 position, bool isSprinting = false)
        {
            _navMeshAgent.speed = (isSprinting && canRun) ? movSprintSpeed : movWalkSpeed;
            if (canMove)
            {
                _navMeshAgent.SetDestination(position);
            }
        }
    }
}