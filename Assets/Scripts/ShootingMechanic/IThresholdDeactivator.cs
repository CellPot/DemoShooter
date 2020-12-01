using System.Collections;
using UnityEngine;

namespace DemoShooter.ShootingMechanic
{
    public interface IThresholdDeactivator
    {
        IEnumerator DeactivationCoroutine(GameObject objToDeactivate, float seconds);
    }
}