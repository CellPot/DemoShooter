using System.Collections.Generic;
using UnityEngine;

namespace DemoShooter.ShootingMechanic
{
    public interface IAmmoPoolManager
    {
        List<Ammo> CreateAmmoPool(Ammo ammoPrefab, int poolSize, Transform ammoNestingObject);
        Ammo PullAmmo(List<Ammo> ammoPool, Vector3 startPosition, Quaternion startRotation);
    }
}