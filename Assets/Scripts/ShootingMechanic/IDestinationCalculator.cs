using UnityEngine;

namespace DemoShooter.ShootingMechanic
{
    public interface IDestinationCalculator
    {
        Vector3 GetDestinationVector(Camera playerCamera, bool takeDistanceIntoAccount);
    }
}