using UnityEngine;
using Freedom.Core.View;
using Freedom.Core.Model.Factories;
using Freedom.Core.Model.Interfaces;
using Freedom.Core.View.Interfaces;

namespace Freedom.Core.Model
{
    public class ShipModel : IShipModel
    {
        public enum ShipState
        {
            NONE,
            ALIVE,
            DEAD,
            RECYCLE
        }

        protected Vector3 position;
        protected float speed;
        protected Vector3 direction;
        protected IShipView shipViewHandler;
        protected bool moving;

        private ShipFactory.ShipType shipType;
        private BulletFactory.BulletType bulletType;

        private System.Action<BulletFactory.BulletType,Vector3,Vector3> bulletShootListener;
        private Vector3 bulletDirection;
        private float timeToShoot;
        private float shootRate;
        private bool shooting;

        public ShipModel (ShipFactory.ShipType type, Vector3 position)
        {
            Setup (type, position);
        }

        #region Implements IShipModel and Itickable

        public void Tick (float deltaTime)
        {
            if (State != ShipState.ALIVE)
                return;

            if (moving) {
                Move (deltaTime);
                this.shipViewHandler.UpdateView (position);
            }

            if (shooting) {
                CheckShoot (deltaTime);
            }
        }

        public void Setup (ShipFactory.ShipType type, Vector3 position)
        {
            this.State = ShipState.NONE;
            this.position = position;
            this.shipType = type;

            this.moving = false;
            this.shooting = false;

            if (type == ShipFactory.ShipType.A) {
                this.speed = 3f;
                this.shootRate = 0.2f;
                bulletDirection = Vector3.forward;
                bulletType = BulletFactory.BulletType.A;
            } else {
                this.speed = 2.5f;
                this.shootRate = 1.5f;
                bulletDirection = -Vector3.forward;
                bulletType = BulletFactory.BulletType.B;
            }

            timeToShoot = shootRate;
        }

        public void SetViewHandler (IShipView shipView)
        {
            this.shipViewHandler = shipView;

            this.shipViewHandler.SetRecycleListener (OnRecycleEvent);
            this.shipViewHandler.SetBulletImpactListener (OnBulletImpact);

            this.State = ShipState.ALIVE;

            this.shipViewHandler.UpdateView (position);

            this.shooting = true;
        }

        public void SetBulletListener(System.Action<BulletFactory.BulletType, Vector3, Vector3> listener)
        {
            bulletShootListener = listener;
        }

        public ShipState State { get; private set; }

        public Vector3 GetPosition ()
        {
            return position;
        }

        public ShipFactory.ShipType GetShipType ()
        {
            return this.shipType;
        }

        public void SetDirection (Vector3 direction)
        {
            this.direction = direction;
            moving = true;
        }

        public void StopMovement ()
        {
            moving = false;
        }

        #endregion // Implements IShipModel and Itickable

        private void Move (float deltaTime)
        {
            position += (direction * speed * deltaTime);
        }

        private void CheckShoot (float deltaTime)
        {
            timeToShoot -= deltaTime;

            if (timeToShoot < 0) {
                // reset time
                timeToShoot += shootRate;

                // shoot
                if (bulletShootListener != null)
                    bulletShootListener (bulletType, this.shipViewHandler.GunPosition, bulletDirection);
            }
        }

        private void OnBulletImpact (float damage)
        {
            if (State == ShipState.ALIVE) {
                this.State = ShipState.DEAD;

                this.shipViewHandler.Die (
                    () => {
                        this.shipViewHandler = null;
                        this.State = ShipState.RECYCLE;
                    }
                );
            }
        }

        private void OnRecycleEvent ()
        {
            this.shipViewHandler = null;
            this.State = ShipState.RECYCLE;
        }
    }
}