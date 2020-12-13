using System.Collections.Generic;
using UnityEngine;

namespace DemoShooter.ShootingMechanic
{
    public class AmmoPoolCreator
    {
        public List<Ammo> CreateObjectPool(Ammo ammoPrefab, int poolSize, Transform ammoNestingObject)
        {
            List<Ammo> ammoPool = new List<Ammo>();
            for (int i = 0; i < poolSize; i++)
            {
                Ammo ammoObject = Object.Instantiate<Ammo>(ammoPrefab, ammoNestingObject);
                ammoObject.gameObject.SetActive(false);
                ammoPool.Add(ammoObject);
            }
            return ammoPool;
        }
    }
}