using UnityEngine;
using Freedom.Core.Model;
using Freedom.Core.Model.Factories;
using Freedom.Core.View.Interfaces;
using Freedom.Core.View.Utils;

namespace Freedom.Core.View
{
    public class ShipView : MonoBehaviour, IShipView
    {
        public ShipFactory.ShipType shipType;
        public GameObject model;
        public Transform gun;

        public System.Action OnDie;

        private Transform myTransform;
        private System.Action<float> bulletImpactListener;
        private System.Action recycleListener;
        private System.Action<ShipView> destroyedListener;

        private void Awake ()
        {
            myTransform = this.transform;

            (model.GetComponent<BulletCollisionListener> ()).OnCollisionDetected = OnBulletCollision;
        }

        public void UpdateView (Vector3 position)
        {
            myTransform.position = position;
        }

        public Vector3 GetPosition ()
        {
            return myTransform.position;
        }

        public void SetBulletImpactListener (System.Action<float> bulletImpactListener)
        {
            this.bulletImpactListener = bulletImpactListener;
        }

        public void SetRecycleListener (System.Action recycleListener)
        {
            this.recycleListener = recycleListener;
        }

        public void SetDestroyedListener (System.Action<ShipView> destroyedListener)
        {
            this.destroyedListener = destroyedListener;
        }

        public void Die (System.Action callback)
        {
            // play destruction animation

            if (destroyedListener != null) {
                destroyedListener (this);
            }

            callback ();
        }

        public string ShipType { get { return shipType.ToString (); } }

        public Vector3 GunPosition { get { return gun.position; } }

        public void Recycle ()
        {
            if (recycleListener != null)
                recycleListener ();
            
            Reset ();
        }

        private void OnBulletCollision (BulletView bullet)
        {
            this.bulletImpactListener (bullet.Damage);
        }

        private void Reset ()
        {
            this.recycleListener = null;
            this.bulletImpactListener = null;
        }
    }
}