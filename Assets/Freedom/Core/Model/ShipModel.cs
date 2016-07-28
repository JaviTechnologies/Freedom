using UnityEngine;
using Freedom.Core.View;

namespace Freedom.Core.Model {
    public class ShipModel : IShipModel
    {
        public enum ShipState
        {
            NONE, ALIVE, DEAD, RECYCLED
        }

        protected Vector3 position;
        protected float speed;
        protected Vector3 direction;
        protected IShipView shipViewHandler;
        protected bool moving;

        private ShipFactory.ShipType shipType;
        public ShipState State { get; private set; }

        public ShipModel (ShipFactory.ShipType type, Vector3 position)
        {
            Init (type, position);
        }

        public void Init (ShipFactory.ShipType type, Vector3 position)
        {
            this.State = ShipState.NONE;
            this.position = position;
            this.shipType = type;

            this.moving = false;
            this.speed = 2.5f;
        }

        public void Setup (IShipView shipView)
        {
            this.shipViewHandler = shipView;

            this.shipViewHandler.SetRecycleListener (OnRecycleEvent);

            this.State = ShipState.ALIVE;

            this.shipViewHandler.UpdateView (position);
        }

        public Vector3 GetPosition ()
        {
            return position;
        }

        public ShipFactory.ShipType GetShipType ()
        {
            return this.shipType;
        }

        public void Tick(float deltaTime)
        {
            if (State != ShipState.ALIVE)
                return;

            if (moving)
            {
                Move (deltaTime);
                this.shipViewHandler.UpdateView (position);
            }
        }

        private void Move (float deltaTime)
        {
            position += (direction * speed * deltaTime);
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

        private void OnRecycleEvent ()
        {
            this.shipViewHandler = null;
            this.State = ShipState.RECYCLED;
        }
    }
}