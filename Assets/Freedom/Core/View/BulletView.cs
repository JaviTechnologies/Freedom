using UnityEngine;
using System;
using Freedom.Core.View.Interfaces;
using Freedom.Core.Model.Factories;

namespace Freedom.Core.View
{
    public class BulletView : MonoBehaviour, IBulletView
    {
        public BulletFactory.BulletType BulletType;

        private System.Action recycleListener;

        public void Setup(float damage)
        {
            Damage = damage;
        }

        public float Damage { get; private set; }

        public void UpdateView (Vector3 position)
        {
            transform.position = position;
        }

        public void SetRecycleListener (System.Action recycleListener)
        {
            this.recycleListener = recycleListener;
        }

        public void Recycle()
        {
            if (recycleListener != null)
                recycleListener ();

            // reset
            this.recycleListener = null;
        }
    }
}

