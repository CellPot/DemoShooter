using System;
using UnityEngine;

namespace DemoShooter.ShootingMechanic
{
	public class DontGoThroughThings : MonoBehaviour
	{
		// Careful when setting this to true - it might cause double
		// events to be fired - but it won't pass through the trigger
		public bool sendTriggerMessage = false; 	
	 
		public LayerMask layerMask = -1; //make sure we aren't in this layer 
		[Range(0.1f,1.0f)]
		public float skinWidth = 0.1f; //probably doesn't need to be changed 
	 
		private float _minimumExtent; 
		private float _partialExtent; 
		private float _sqrMinimumExtent; 
		private Vector3 _previousPosition; 
		private Rigidbody _myRigidbody;
		private Collider _myCollider;
	 
		//initialize values 
		private void Start() 
		{ 
		   _myRigidbody = GetComponent<Rigidbody>();
		   _myCollider = GetComponent<Collider>();
		   _previousPosition = _myRigidbody.position;
		   var colBounds = _myCollider.bounds;
		   _minimumExtent = Mathf.Min(Mathf.Min(colBounds.extents.x, colBounds.extents.y), colBounds.extents.z); 
		   _partialExtent = _minimumExtent * (1.0f - skinWidth); 
		   _sqrMinimumExtent = _minimumExtent * _minimumExtent; 
		}

		private void OnDisable()
		{
			_previousPosition = Vector3.zero;
		}

		private void FixedUpdate()
		{
			CheckCollisions();
		}

		private void CheckCollisions()
		{
			if (_previousPosition == Vector3.zero)
				_previousPosition = _myRigidbody.position;
			
			//have we moved more than our minimum extent? 
			Vector3 movementThisStep = _myRigidbody.position - _previousPosition;
			float movementSqrMagnitude = movementThisStep.sqrMagnitude;

			if (movementSqrMagnitude > _sqrMinimumExtent)
			{
				float movementMagnitude = Mathf.Sqrt(movementSqrMagnitude);
				RaycastHit hitInfo;

				//check for obstructions we might have missed 
				if (Physics.Raycast(_previousPosition, movementThisStep, out hitInfo, movementMagnitude, layerMask.value))
				{
					if (!hitInfo.collider)
						return;

					if (sendTriggerMessage && hitInfo.collider.isTrigger)
					{
						hitInfo.collider.SendMessage("OnTriggerEnter", _myCollider);
					}

					if (!hitInfo.collider.isTrigger)
						_myRigidbody.position = hitInfo.point - (movementThisStep / movementMagnitude) * _partialExtent;
				}
			}

			_previousPosition = _myRigidbody.position;
		}
	}
}