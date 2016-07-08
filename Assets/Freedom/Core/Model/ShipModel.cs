using UnityEngine;
using Freedom.Core.View;

namespace Freedom.Core.Model {
    public class ShipModel : IShipModel
    {
        protected Vector3 position;
        protected float speed;
        protected Vector3 direction;
        protected IShipView shipViewHandler;

        public ShipFactory.ShipType ShipType { get; private set; }

        public ShipModel(ShipFactory.ShipType type, Vector3 position, IShipView shipView)
        {
            this.position = position;
            this.ShipType = type;
            this.shipViewHandler = shipView;
            this.direction = new Vector3 (0,1f,0);
        }

        public void Tick(float deltaTime)
        {
            Move (deltaTime);
        }

        protected virtual void Move(float deltaTime)
        {
            position += (direction.normalized * speed * deltaTime);
        }
    }
}