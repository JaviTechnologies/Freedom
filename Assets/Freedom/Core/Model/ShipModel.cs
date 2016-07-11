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

        public ShipFactory.ShipType ShipType { get; private set; }

        public ShipModel(ShipFactory.ShipType type, Vector3 position, IShipView shipView)
        {
            this.position = position;
            this.ShipType = type;
            this.shipViewHandler = shipView;
            this.moving = false;

            this.speed = 3f;
            this.direction = new Vector3 (0,0,0);
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