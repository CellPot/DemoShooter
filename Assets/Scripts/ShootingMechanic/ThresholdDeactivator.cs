using System.Collections;
using UnityEngine;

namespace DemoShooter.ShootingMechanic
{
    public class ThresholdDeactivator : MonoBehaviour, IThresholdDeactivator
    {
        public IEnumerator DeactivationCoroutine(GameObject objToDeactivate,float seconds)
        {
            yield return new WaitForSeconds(seconds);
            if (objToDeactivate.activeSelf)
                objToDeactivate.SetActive(false);
            yield return null;
        }
    }
}