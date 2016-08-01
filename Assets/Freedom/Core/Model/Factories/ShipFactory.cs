using UnityEngine;
using Freedom.Core.View;
using Freedom.Core.Model.Interfaces;

namespace Freedom.Core.Model.Factories
{
    public class ShipFactory
    {
        public enum ShipType
        {
            A, // player model
            B,
            C
        }

        public static IShipModel CreateShip (ShipType type, Vector3 position)
        {
            IShipModel shipModel = new ShipModel (type, position);

            return shipModel;
        }
    }
}