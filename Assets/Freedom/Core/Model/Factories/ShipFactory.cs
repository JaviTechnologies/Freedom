using UnityEngine;
using Freedom.Core.View;

namespace Freedom.Core.Model
{
    public class ShipFactory
    {
        public enum ShipType
        {
            A, B, C
        }

        public static IShipModel CreateShip(ShipType type, Vector3 position, IShipView shipView)
        {
            IShipModel shipModel = new ShipModel (type, position, shipView);

            return shipModel;
        }
    }
}