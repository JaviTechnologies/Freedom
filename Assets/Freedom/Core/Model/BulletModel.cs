using UnityEngine;
using System;
using Freedom.Core.Model.Interfaces;
using Freedom.Core.View.Interfaces;
using Freedom.Core.Model.Factories;

namespace Freedom.Core.Model
{
    public class BulletModel : IBulletModel
    {
        public enum BulletState
        {
            NONE,
            ALIVE,
            RECYCLED
        }

        protected float speed;
        protected Vector3 position;
        protected Vector3 direction;

        private IBulletView bulletViewHandler;
        private bool shouldWork;

        public BulletModel (BulletFactory.BulletType bulletType, Vector3 position, Vector3 direction)
        {
            Init (bulletType, position, direction);
        }

        #region Implements IBulletModel and Itickable

        public void Init (BulletFactory.BulletType bulletType, Vector3 position, Vector3 direction)
        {
            this.position = position;
            this.direction = direction;
            this.BulletType = bulletType;

            speed = 10f;

            shouldWork = false;
        }

        public BulletState State { get; private set; }

        public BulletFactory.BulletType BulletType { get; private set; }

        public void Tick (float deltaTime)
        {
            if (State != BulletState.ALIVE)
                return;
            
            // move bullet
            if (shouldWork) {
                Move (deltaTime);

                this.bulletViewHandler.UpdateView (position);
            }
        }

        public void SetBulletViewHandler(IBulletView bulletView)
        {
            this.bulletViewHandler = bulletView;

            this.bulletViewHandler.SetRecycleListener (OnRecycleEvent);

            this.State = BulletState.ALIVE;

            shouldWork = true;
        }

        #endregion // Implements IBullet and Itickable

        private void Move (float deltaTime)
        {
            position += (direction * speed * deltaTime);
        }

        private void OnRecycleEvent ()
        {
            this.bulletViewHandler = null;
            this.State = BulletState.RECYCLED;
        }
    }
}

