using UnityEngine;
using Freedom.Core.Model;
using Freedom.Core.Model.Factories;
using Freedom.Core.View.Interfaces;
using Freedom.Core.View.Utils;
using System.Collections;

namespace Freedom.Core.View
{
    public class ShipView : MonoBehaviour, IShipView
    {
        public ShipFactory.ShipType shipType;
        public GameObject model;
        public BulletCollisionListener bulletCollisionListener;
        public Renderer modelRenderer;
        public Transform gun;

        public System.Action OnDie;

        private Transform myTransform;
        private System.Action bulletImpactListener;
        private System.Action recycleListener;
        private System.Action<ShipView> destroyedListener;

        private void Awake ()
        {
            myTransform = this.transform;

            bulletCollisionListener.OnCollisionDetected = OnBulletCollision;
        }

        public void UpdateView (Vector3 position)
        {
            myTransform.position = position;
        }

        public Vector3 GetPosition ()
        {
            return myTransform.position;
        }

        public void SetBulletImpactListener (System.Action bulletImpactListener)
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
            // TODO: play destruction animation

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

        public void HandleInvincibleMode (float duration)
        {
            StartCoroutine(InvincibleMode(duration));
        }

        private IEnumerator InvincibleMode (float duration)
        {
            float endTime=Time.time + duration;

            while(Time.time<endTime){
                modelRenderer.enabled = false;
                yield return new WaitForSeconds(0.2f);
                modelRenderer.enabled = true;
                yield return new WaitForSeconds(0.2f);
            }
        }

        private void OnBulletCollision (BulletView bullet)
        {
            this.bulletImpactListener ();
        }

        private void Reset ()
        {
            this.recycleListener = null;
            this.bulletImpactListener = null;
        }
    }
}