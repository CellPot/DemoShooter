using System.Collections.Generic;
using UnityEngine;

namespace DemoShooter.ShootingMechanic
{
    public class AmmoPuller
    {
        public Ammo PullAmmo(List<Ammo> ammoPool, Vector3 startPosition, Quaternion startRotation)
        {
            foreach (Ammo bullet in ammoPool)
            {
                if (bullet.gameObject.activeSelf == false)
                {
                    bullet.PositionSetter.SetActiveState(true,startPosition,startRotation);
                    return bullet;
                }
            }
            Ammo newAmmo = Object.Instantiate(ammoPool[0]);
            ammoPool.Add(newAmmo);
            newAmmo.PositionSetter.SetActiveState(true,startPosition,startRotation);
            return newAmmo;
        }
    }
}