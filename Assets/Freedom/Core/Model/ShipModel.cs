using UnityEngine;
using Freedom.Core.View;

namespace Freedom.Core.Model {
    public class ShipModel : IShipModel
    {
        protected Vector3 position;
        protected float speed;
        protected Vector3 direction;
        protected IShipView shipViewHandler;
        protected bool moving;

        private ShipFactory.ShipType shipType;

        public ShipModel (ShipFactory.ShipType type, Vector3 position)
        {
            this.position = position;
            this.shipType = type;

            this.moving = false;
            this.speed = 2.5f;
        }

        public ShipModel (ShipFactory.ShipType type, Vector3 position, Vector3 direction) : this(type, position)
        {
            SetDirection(direction);
        }

        public void Setup (IShipView shipView)
        {
            this.shipViewHandler = shipView;

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
    }
}