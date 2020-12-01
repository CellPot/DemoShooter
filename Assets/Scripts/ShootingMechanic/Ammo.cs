using UnityEngine;

namespace DemoShooter.ShootingMechanic
{

    public abstract class Ammo: MonoBehaviour, ICanDamage
    {
        [SerializeField] protected int damageInflicted;
        [SerializeField] protected float ammoVelocity;
        protected Rigidbody ammoRigidbody;
        protected IThresholdDeactivator deactivation;
        public StatePositionSetter PositionSetter { get; private set; }

        protected virtual void Awake()
        {
            GameObject thisObject = this.gameObject;
            ammoRigidbody = thisObject.GetComponent<Rigidbody>();
            deactivation = thisObject.GetComponent<IThresholdDeactivator>();
            PositionSetter = new StatePositionSetter(thisObject);
        }
        protected float AmmoVelocity { get => ammoVelocity; set => ammoVelocity = Mathf.Max(value,0); }
        public int DamageInflicted { get => damageInflicted; set => damageInflicted = Mathf.Max(value,0); }
        public Vector3 AmmoPosition => gameObject.transform.position;
        public abstract void FireAmmo(Vector3 targetVector, float secondsUntilInactive, float velocityMod = 1);

    }
}