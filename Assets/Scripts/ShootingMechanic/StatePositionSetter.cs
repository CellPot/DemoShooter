using UnityEngine;

namespace DemoShooter.ShootingMechanic
{
    public class StatePositionSetter
    {
        private readonly GameObject _objectToSet;
        public StatePositionSetter(GameObject gameObject)
        {
            _objectToSet = gameObject;
        }
        public void SetActiveState(bool state,Vector3 position,Quaternion rotation)
        {
            SetActiveState(state);
            _objectToSet.transform.SetPositionAndRotation(position,rotation);
        }
        public void SetActiveState(bool state)
        {
            _objectToSet.SetActive(state);
        }
    }
}