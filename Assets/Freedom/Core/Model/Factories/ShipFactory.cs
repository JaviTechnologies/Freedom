using UnityEngine;
using Freedom.Core.View;

namespace Freedom.Core.Model
{
    public class ShipFactory
    {
        public enum ShipType
        {
            A, // player model
            B,
            C
        }

        public static IShipModel CreateShip(ShipType type, Vector3 position)
        {
            IShipModel shipModel = new ShipModel (type, position);

            return shipModel;
        }

        public static IShipModel CreateShip(ShipType type, Vector3 position, Vector3 direction)
        {
            IShipModel shipModel = new ShipModel (type, position, direction);

            return shipModel;
        }

        public static IShipModel CreateRandomEnemyShip (Vector3 position, Vector3 direction)
        {
            // choose model
            ShipType shipType = (ShipType) Random.Range(1, 2);

            return new ShipModel (shipType, position, direction);
        }
    }
}