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
        public List<Ammo> CreateAmmoPool(Ammo ammoPrefab, int poolSize)
        {
            GameObject poolNester = new GameObject();
            // poolNester.name = $"{gameObject.name}'s ammo nester";
            Instantiate(poolNester);
            return _ammoPoolCreator.CreateObjectPool(ammoPrefab, poolSize,  poolNester.transform);
        }
        public Ammo PullAmmo(List<Ammo> ammoPool, Vector3 startPosition, Quaternion startRotation)
        {
            return _ammoPuller.PullAmmo(ammoPool, startPosition, startRotation);
        }
        
    }
}