using UnityEngine;

namespace DemoShooter.ShootingMechanic
{
    public interface IDestinationCalculator
    {
        Vector3 GetHitVector();
    }
}