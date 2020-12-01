using System.Collections.Generic;
using UnityEngine;

namespace DemoShooter.ShootingMechanic
{
    public class AmmoPoolManager : MonoBehaviour, IAmmoPoolManager
    {
        private AmmoPoolCreator _ammoPoolCreator; 
        private AmmoPuller _ammoPuller;

        private void Awake()
        {
            _ammoPoolCreator = new AmmoPoolCreator();
            _ammoPuller = new AmmoPuller();
        }
        public List<Ammo> CreateAmmoPool(Ammo ammoPrefab, int poolSize, Transform ammoNestingObject)
        {
            return _ammoPoolCreator.CreateObjectPool(ammoPrefab, poolSize,  ammoNestingObject);
        }
        public Ammo PullAmmo(List<Ammo> ammoPool, Vector3 startPosition, Quaternion startRotation)
        {
            return _ammoPuller.PullAmmo(ammoPool, startPosition, startRotation);
        }
        
    }
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